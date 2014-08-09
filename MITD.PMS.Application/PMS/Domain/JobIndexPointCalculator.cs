using MITD.PMS.Domain.Model.Calculations;
using MITD.PMS.Domain.Model.Policies;
using MITD.PMS.Domain.Service;
using MITD.PMS.Application.Contracts;
using MITD.Core;
using System.Threading.Tasks;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.Periods;
using System.Linq;
using System.Collections.Generic;
using System;
using MITD.PMS.Domain.Model.JobIndexPoints;
using System.Threading;
using MITD.PMS.Exceptions;
using MITD.PMSSecurity.Domain.Logs;

namespace MITD.PMS.Application
{
    public enum CalculatorState { New, Running, Completed };
    public class JobIndexPointCalculator : IJobIndexPointCalculator
    {
        private IEventPublisher publisher;
        private IJobIndexPointPersister jobIndexPointPersister;
        private ICalculationExceptionPersister calculationExceptionPersister;
        private ICalculatorEngineFactory calculatorEngineFactory;
        private IEmployeeServiceFactory employeeServiceFactory;

        private AutoResetEvent pathPersisterTrigger = new AutoResetEvent(false);
        private AutoResetEvent startTrigger = new AutoResetEvent(false);
        private AutoResetEvent pauseTrigger = new AutoResetEvent(false);

        private object lockObj = new object();
        private object lockObj2 = new object();
        private object lockObj3 = new object();
        private object lockObj4 = new object();
        private object operationLock = new object();

        private List<string> messages = new List<string>();
        private CalculatorState state = CalculatorState.New;
        private CalculatorSession calculatorSession = new CalculatorSession { PathNo = 1 };
        private CalculationProgress progress = new CalculationProgress();
        private bool doPause = false;
        private bool doStop = false;
        private DelegateHandler<CalculationCompleted> CalculationCompletedSub;
        private DelegateHandler<RulesCompiled> rulsCompiledSub;

        public CalculatorState State
        {
            get
            {
                lock (lockObj)
                {
                    return state;
                }
            }
        }

        public CalculatorSession CalculatorSession
        {
            get
            {
                lock (lockObj3)
                {
                    return calculatorSession;
                }
            }
        }

        public IReadOnlyList<string> Messages
        {
            get
            {
                lock (lockObj2)
                {
                    return messages.ToList();
                }
            }
        }

        public CalculationProgress Progress
        {
            get 
            {
                lock (lockObj4)
                {
                    return progress;
                }
            }
        }

        public JobIndexPointCalculator(IEventPublisher publisher,
            ICalculatorEngineFactory calculatorEngineFactory, IEmployeeServiceFactory employeeServiceFactory)
        {
            this.calculatorEngineFactory = calculatorEngineFactory;
            this.publisher = publisher;
            this.employeeServiceFactory = employeeServiceFactory;
        }

        private void start(Calculation calculation, bool doResume)
        {
            long totalCalcOperationCount = 0;
            long preCalcOperationDoneCount = 0;
            calculatorSession = new CalculatorSession { PathNo = 1 };
            if (doResume)
            {
                var calcResult = calculation.CalculationResult;
                foreach (string s in calcResult.Messages)
                    messages.Add(s);
                messages.Add("=====================================================");
                totalCalcOperationCount = calcResult.TotalEmployeesCount;
                preCalcOperationDoneCount = calcResult.EmployeesCalculatedCount;
                progress.SetProgress(totalCalcOperationCount, preCalcOperationDoneCount);
                if (calcResult.LastCalculatedPath.HasValue)
                {
                    progress.SetLastCalculatedEmployee(calcResult.LastCalculatedEmployeeId, calcResult.LastCalculatedPath.Value);
                    calculatorSession.PathNo = calcResult.LastCalculatedPath.Value;
                }
                var engine = calculatorEngineFactory.Create();
                try
                {
                    calculatorSession.AddCalculationPoints(engine.GetCalculationPiontBy(calculation.Id));
                }
                finally
                {
                    calculatorEngineFactory.Release(engine);
                }

            }

            Policy policy = null;
            messages.Add(DateTime.Now + "شروع محاسبه  ");
            try
            {
                startTrigger.Set();
                CalculationCompletedSub = new DelegateHandler<CalculationCompleted>(e =>
                {
                    var engine = calculatorEngineFactory.Create();
                    try
                    {
                        engine.AddUpdateCalculationPoint(calculatorSession.CalculationPoints);
                        engine.UpdateCalculationResult(calculation, progress, this, messages);

                    }
                    finally
                    {
                        calculatorEngineFactory.Release(engine);
                    }
                });
                publisher.RegisterHandler(CalculationCompletedSub);

                rulsCompiledSub = new DelegateHandler<RulesCompiled>(e =>
                {
                    if (!doResume)
                    {
                        Task.Factory.StartNew(() =>
                            {
                                var engine = calculatorEngineFactory.Create();
                                try
                                {
                                    engine.UpdateCompileResult(calculation.Id, e.CompileResult.LibraryText, e.Rules);
                                }
                                finally
                                {
                                    calculatorEngineFactory.Release(engine);

                                }

                                messages.Add(DateTime.Now + "  قوانین کامپایل شدند");
                            });
                    }
                });
                publisher.RegisterHandler(rulsCompiledSub);

                publisher.RegisterHandler<JobIndexPointsReady>(jobIndexPointPersister);
                publisher.RegisterHandler<CalculationExceptionReady>(calculationExceptionPersister);
                publisher.RegisterHandler<CalculationCompleted>(jobIndexPointPersister);
                publisher.RegisterHandler<CalculationCompleted>(calculationExceptionPersister);
                publisher.RegisterHandler<CalculationForPathCompleted>(jobIndexPointPersister);
                publisher.RegisterHandler<PathPersisteCompleted>(this);

                Dictionary<int, IList<Employee>> employeesWithPath;
                Period period;
                fetchPolicyAndEmployees(calculation, doResume, calculatorSession.PathNo, out policy, out employeesWithPath, out period);
                deleteCalculationException(calculation);

                long currentCalcOperationCount = 0;
                foreach (var employees in employeesWithPath)
                {
                    currentCalcOperationCount = currentCalcOperationCount + employees.Value.Count();
                }

                if (!doResume)
                {
                    totalCalcOperationCount = currentCalcOperationCount;
                    progress.SetProgress(totalCalcOperationCount, 0);
                }

                messages.Add(DateTime.Now + "  تعداد " + currentCalcOperationCount + " عملیات محاسبه برای  " + calculation.EmployeeCount + " کارمند آماده می باشد");

                var currentCalcOperationDoneCount = preCalcOperationDoneCount;
                var pathCount = employeesWithPath.Count();

                foreach (var emlpoyees in employeesWithPath)
                {
                    calculatorSession.PathNo = emlpoyees.Key;

                    foreach (var employee in emlpoyees.Value)
                    {
                        try
                        {
                            if (doStop || doPause) break;

                            var pointsHolder = calculateIndices(calculation, policy, period, employee, calculatorSession);
                            currentCalcOperationDoneCount++;
                            progress.SetProgress(totalCalcOperationCount, currentCalcOperationDoneCount);
                            addOrUpdateCalculationPoints(pointsHolder.CalculationPoints);
                            publisher.Publish(new JobIndexPointsReady(pointsHolder, calculation.Id, employee.Id, calculatorSession.PathNo));
                            progress.SetLastCalculatedEmployee(employee.Id, calculatorSession.PathNo);
                        }
                        catch (Exception ex)
                        {
                            publisher.Publish(new CalculationExceptionReady(calculation.Id, employee.Id, calculatorSession.PathNo, ex));
                            calculatorSession.HasEmployeeCalculationFailed = true;
                            messages.Add("*** خطا در محاسبه شاخص های کارمند " + employee.Id.EmployeeNo + " **** ");
                            var logServiceMngt = LogServiceFactory.Create();
                            try
                            {
                                var logService = logServiceMngt.GetService();
                                logService.AddEventLog("JobIndexPointCalculator_EmpId:" + employee.Id.EmployeeNo,
                                    LogLevel.Error,
                                    null, this.GetType().Name, "start", ex.Message, ex.StackTrace);
                                logService.AddExceptionLog(ex);
                            }
                            finally
                            {
                                LogServiceFactory.Release(logServiceMngt);
                            }

                        }
                    }

                    if (pathCount > calculatorSession.PathNo)
                    {
                        publisher.Publish(new CalculationForPathCompleted());
                        pathPersisterTrigger.WaitOne();
                    }

                    if (calculatorSession.HasEmployeeCalculationFailed)
                        break;

                }

                messages.Add(DateTime.Now + "  تعداد " + currentCalcOperationDoneCount + " عملیات محاسبه انجام شد");
                if (!(doStop || doPause))
                {
                   publisher.Publish(new CalculationCompleted());
                }
                else if (doStop)
                {
                    messages.Add(DateTime.Now + "  لغو محاسبه.");
                }
                else if (doPause)
                {
                    messages.Add(DateTime.Now + "  وقفه در محاسبه.");
                }
                if (doPause || doStop)
                {
                    var engine = calculatorEngineFactory.Create();
                    try
                    {
                        engine.AddUpdateCalculationPoint(calculatorSession.CalculationPoints);

                    }
                    finally
                    {
                        calculatorEngineFactory.Release(engine);
                    }
                }
                pauseTrigger.Set();
            }
            catch (Exception e)
            {
                startTrigger.Set();
                calculatorSession.HasEmployeeCalculationFailed = true;
                messages.Add(DateTime.Now + "در آماده سازی محاسبه با مشکل مواجه شده است");
                publisher.Publish(new CalculationCompleted());

            }
            finally
            {
                state = CalculatorState.Completed;
                if (policy != null)
                    policy.Dispose();
            }
        }

        private void deleteCalculationException(Calculation calculation)
        {
            var engine = calculatorEngineFactory.Create();
            try
            {
                engine.DeleteCalculationException(calculation);

            }
            finally
            {
                calculatorEngineFactory.Release(engine);
            }
        }

        private void addOrUpdateCalculationPoints(IEnumerable<CalculationPoint> points)
        {
            calculatorSession.AddCalculationPoints(points);
        }

        private CalculationPointPersistanceHolder calculateIndices(Calculation calculation, Policy policy, Period period, Employee employee, CalculatorSession calculationSession)
        {
            CalculationPointPersistanceHolder pointsHolder;
            var engine = calculatorEngineFactory.Create();
            try
            {
                pointsHolder = engine.CalculateIndices(calculation, policy, period, employee, publisher, calculationSession);
            }
            finally
            { calculatorEngineFactory.Release(engine); }
            return pointsHolder;
        }

        private void fetchPolicyAndEmployees(Calculation calculation, bool doResume, int pathNo, out Policy policy, out Dictionary<int, IList<Employee>> employees, out Period period)
        {
            messages.Add(DateTime.Now + "  بارگزاری کارمندان...");
            var engine = calculatorEngineFactory.Create();
            try
            {
                engine.fetchPolicyAndEmployees(calculation, doResume, pathNo, out policy, out employees, out period);
            }
            finally
            { calculatorEngineFactory.Release(engine); }
        }

        public void Start(Calculation calculation)
        {
            setRunningState();
            this.jobIndexPointPersister = new JobIndexPointPersister(calculatorEngineFactory, publisher);
            this.calculationExceptionPersister = new CalculationExceptionPersister(calculatorEngineFactory, publisher);
            Task.Factory.StartNew(() => start(calculation, false));
            startTrigger.WaitOne();
        }

        public void Pause(Calculation calculation)
        {
            lock (operationLock)
            {

                if (string.IsNullOrWhiteSpace(calculation.LibraryText))
                    throw new CalculationInvalidStateOperationException("Calculation","Compiling", "Pause"); 
                doPause = true;
                pauseTrigger.WaitOne();

                var employeeManagerService = employeeServiceFactory.Create();
                try
                {
                    var empService = employeeManagerService.GetService();
                    if (progress.LastCalculatedEmployeeId != null)
                        calculation.UpdateCalculationResult(progress.TotalItem, progress.ItemsCalculated,
                                                            messages, progress.LastCalculatedPath.Value, empService.GetBy(progress.LastCalculatedEmployeeId));
                    else
                        calculation.UpdateCalculationResult(progress.TotalItem, progress.ItemsCalculated, messages);
                }
                finally
                {
                    employeeServiceFactory.Release(employeeManagerService);
                }
            }
        }

        public void Stop(Calculation calculation)
        {
            lock (operationLock)
            {
                doStop = true;
                pauseTrigger.WaitOne();
                var employeeManagerService = employeeServiceFactory.Create();
                try
                {
                    var empService = employeeManagerService.GetService();
                    if (progress.LastCalculatedEmployeeId != null)
                        calculation.UpdateCalculationResult(progress.TotalItem, progress.ItemsCalculated,
                                                            messages, progress.LastCalculatedPath.Value, empService.GetBy(progress.LastCalculatedEmployeeId));
                    else
                        calculation.UpdateCalculationResult(progress.TotalItem, progress.ItemsCalculated, messages);
                }
                finally
                {
                    employeeServiceFactory.Release(employeeManagerService);
                }
            }
        }

        public void Resume(Calculation calculation)
        {
            setRunningState();
            this.jobIndexPointPersister = new JobIndexPointPersister(calculatorEngineFactory, publisher);
            this.calculationExceptionPersister = new CalculationExceptionPersister(calculatorEngineFactory, publisher);
            Task.Factory.StartNew(() => start(calculation, true));
            startTrigger.WaitOne();
        }

        private void setRunningState()
        {
            lock (operationLock)
            {
                if (state == CalculatorState.Running)
                    throw new CalculationInvalidStateOperationException("Calculation","Running","Run");
                state = CalculatorState.Running;
            }
        }

        public void Handle(PathPersisteCompleted eventData)
        {
            if (eventData.HasExceptionInPersist)
                calculatorSession.HasEmployeeCalculationFailed = true;
            pathPersisterTrigger.Set();
        }
    }
}
