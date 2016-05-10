using System;
using System.Collections.Generic;
using MITD.Core;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Service;

namespace MITD.PMS.Application
{
    public class EmployeePointCopierService : IEmployeePointCopierService
    {
        #region Fields

        private readonly IEmployeeServiceFactory employeeServiceFactory;
        //private readonly IJobPositionServiceFactory jobPositionServiceFactory;
        //private readonly IUnitServiceFactory unitServiceFactory;
        //private readonly IJobServiceFactory jobServiceFactory;
        //private readonly IJobIndexServiceFactory jobIndexServiceFactory;

        //private readonly IPeriodServiceFactory periodServiceFactory;

        private readonly object lockFlag = new object();
        //private readonly object lockLastPeriod = new object();
        //private KeyValuePair<PeriodId, List<string>> lastPeriod;
        //private DelegateHandler<CopyBasicDataCompleted> copyCompletedSub;

        #endregion

        #region Properties

        private bool isCopying = false;
        public bool IsCopying
        {
            get
            {
                lock (lockFlag)
                {
                    return isCopying;
                }
            }
            private set
            {
                lock (lockFlag)
                {
                    isCopying = value;
                }
            }
        }

        private EmployeePointCopyingProgress employeePointCopyingProgress = new EmployeePointCopyingProgress();

        public EmployeePointCopyingProgress EmployeePointCopyingProgress
        {
            get
            {
                lock (lockFlag) { return employeePointCopyingProgress; }
            }
        }

        #endregion

        #region Constructors

        public EmployeePointCopierService( IEmployeeServiceFactory employeeServiceFactory)
        {
            this.employeeServiceFactory = employeeServiceFactory;
        }

        #endregion
        
        #region Public Methods
        public void CopyEmployeePoint(Period period, IEventPublisher publisher)
        {
            if (!IsCopying)
            {
                IsCopying = true;
                start(period, publisher);
                //Task.Factory.StartNew(() =>
                //{

                //    start(currentPeriod, sourcePeriod, publisher);

                //});

            }
        } 
        #endregion

        #region private methods

        private void start(Period period, IEventPublisher publisher)
        {
            try
            {
                employeePointCopyingProgress.State = new PeriodConfirmationState();
                employeePointCopyingProgress.Messages.Add("شروع کپی نمرات");

                var employeeIdList = getAllEmployeeNo(period);//employeeRepository.GetAllEmployeeNo(a => true);
                foreach (var employeeNo in employeeIdList)
                {
                    var srvManagerEmployeeService = employeeServiceFactory.Create();
                    try
                    {
                        var empService = srvManagerEmployeeService.GetService();
                        empService.UpdateFinalPoint(new EmployeeId(employeeNo, period.Id));
                    }
                    finally
                    {
                        employeeServiceFactory.Release(srvManagerEmployeeService);
                    }

                }

                IsCopying = false;
                employeePointCopyingProgress = new EmployeePointCopyingProgress();
            }

            catch (Exception)
            {

                IsCopying = false;
                employeePointCopyingProgress = new EmployeePointCopyingProgress();
            }

        }

        private IEnumerable<string> getAllEmployeeNo(Period period)
        {
            var srvManagerEmployeeService = employeeServiceFactory.Create();
            try
            {
                var empService = srvManagerEmployeeService.GetService();
                return empService.GetAllEmployeeNo(period.Id);
            }
            finally
            {
                employeeServiceFactory.Release(srvManagerEmployeeService);
            }

        } 
        #endregion

        //private void start(Period currentPeriod, Period sourcePeriod, IEventPublisher publisher)
        //{
        //    //basicDataCopyingProgress.State = new PeriodBasicDataCopying();
        //    //basicDataCopyingProgress.Messages.Add("شروع کپی دوره");
        //    //var preState = currentPeriod.State;
        //    //publisher.OnHandlerError(exp => { basicDataCopyingProgress.Messages.Add(exp.Message); });
        //    //copyCompletedSub = new DelegateHandler<CopyBasicDataCompleted>(
        //    //    p =>
        //    //    {
        //    //        var srvManagerPeriod = periodServiceFactory.Create();
        //    //        try
        //    //        {
        //    //            var ps = srvManagerPeriod.GetService();
        //    //            ps.CompleteCopyingBasicData(currentPeriod.Id, preState);
        //    //        }
        //    //        finally
        //    //        {
        //    //            IsCopying = false;
        //    //            basicDataCopyingProgress = new BasicDataCopyingProgress();
        //    //            periodServiceFactory.Release(srvManagerPeriod);
        //    //        }
        //    //    });
        //    //publisher.RegisterHandler(copyCompletedSub);

        //    //try
        //    //{
        //    //    BasicDataCopyingProgress.SetProgress(100, 5);
        //    //    deleteAllPeriodData(currentPeriod);
        //    //    basicDataCopyingProgress.Messages.Add("اطلاعات پایه پیشین پاک شد.");

        //    //    basicDataCopyingProgress.SetProgress(100, 25);
        //    //    copyUnits(sourcePeriod, currentPeriod);
        //    //    basicDataCopyingProgress.Messages.Add("کپی واحد های سازمانی انجام شد.");


        //    //    basicDataCopyingProgress.SetProgress(100, 35);
        //    //    copyPeriodJobIndices(sourcePeriod, currentPeriod);
        //    //    basicDataCopyingProgress.Messages.Add("کپی شاخص ها انجام شد.");

        //    //    basicDataCopyingProgress.SetProgress(100, 45);
        //    //    copyJobs(sourcePeriod, currentPeriod);
        //    //    basicDataCopyingProgress.Messages.Add("کپی شغل ها انجام شد.");
        //    //    basicDataCopyingProgress.SetProgress(100, 65);

        //    //    copyJobPositions(sourcePeriod, currentPeriod);
        //    //    basicDataCopyingProgress.Messages.Add("کپی پست ها انجام شد.");
        //    //    basicDataCopyingProgress.SetProgress(100, 100);
        //    //    basicDataCopyingProgress.Messages.Add("اتمام عملیات کپی دوره.");
        //    //    lastPeriod=new KeyValuePair<PeriodId,List<string>>(currentPeriod.Id, basicDataCopyingProgress.Messages);
        //    //    publisher.Publish(new CopyBasicDataCompleted(currentPeriod,preState));
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    basicDataCopyingProgress.Messages.Add("خطا در کپی اطلاعات " + ex.Message);
        //    //    var logServiceMngt = LogServiceFactory.Create();
        //    //    try
        //    //    {
        //    //        var logService = logServiceMngt.GetService();
        //    //        logService.AddEventLog("Copy Exception",
        //    //            LogLevel.Error,
        //    //            null, this.GetType().Name, "start", ex.Message, ex.StackTrace);
        //    //        logService.AddExceptionLog(ex);
        //    //    }
        //    //    finally
        //    //    {
        //    //        LogServiceFactory.Release(logServiceMngt);
        //    //    }
        //    //    IsCopying = false;
        //    //    basicDataCopyingProgress = new BasicDataCopyingProgress();
        //    //}
        //}
    }
}
