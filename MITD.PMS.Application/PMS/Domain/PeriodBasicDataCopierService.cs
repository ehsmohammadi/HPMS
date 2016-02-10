using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using MITD.Core;
using MITD.PMS.Application.Contracts;
using MITD.PMS.Domain.Model.JobIndices;
using MITD.PMS.Domain.Model.JobPositions;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Model.Units;
using MITD.PMS.Domain.Service;
using MITD.PMS.Domain.Model.Jobs;
using System;
using MITD.PMSSecurity.Domain.Logs;
using System.Threading;

namespace MITD.PMS.Application
{
    public class PeriodBasicDataCopierService : IPeriodBasicDataCopierService
    {
        private readonly IJobPositionServiceFactory jobPositionServiceFactory;
        private readonly IUnitServiceFactory unitServiceFactory;
        private readonly IJobServiceFactory jobServiceFactory;
        private readonly IJobIndexServiceFactory jobIndexServiceFactory;
        private BasicDataCopyingProgress basicDataCopyingProgress = new BasicDataCopyingProgress();
        private readonly IPeriodServiceFactory periodServiceFactory;
        private bool isCopying = false;
        private readonly object lockFlag = new object();
        private readonly object lockLastPeriod = new object();
        private KeyValuePair<PeriodId, List<string>> lastPeriod;
        private DelegateHandler<CopyBasicDataCompleted> copyCompletedSub;

        public KeyValuePair<PeriodId, List<string>> LastPeriod
        {
            get
            {
                lock (lockLastPeriod)
                {
                    return lastPeriod;
                }
            }
        }

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

        public BasicDataCopyingProgress BasicDataCopyingProgress
        {
            get
            {
                lock (lockFlag) { return basicDataCopyingProgress; }
            }
        }

        public PeriodBasicDataCopierService(IJobPositionServiceFactory jobPositionServiceFactory, IUnitServiceFactory unitServiceFactory,
            IJobServiceFactory jobServiceFactory, IJobIndexServiceFactory jobIndexServiceFactory, IPeriodServiceFactory periodServiceFactory)
        {
            this.jobPositionServiceFactory = jobPositionServiceFactory;
            this.unitServiceFactory = unitServiceFactory;
            this.jobServiceFactory = jobServiceFactory;
            this.jobIndexServiceFactory = jobIndexServiceFactory;
            this.periodServiceFactory = periodServiceFactory;
        }

        private void start(Period currentPeriod, Period sourcePeriod, IEventPublisher publisher)
        {
            basicDataCopyingProgress.State = new PeriodBasicDataCopying();
            basicDataCopyingProgress.Messages.Add("شروع کپی دوره");
            var preState = currentPeriod.State;
            publisher.OnHandlerError(exp => { basicDataCopyingProgress.Messages.Add(exp.Message); });
            copyCompletedSub = new DelegateHandler<CopyBasicDataCompleted>(
                p =>
                {
                    var srvManagerPeriod = periodServiceFactory.Create();
                    try
                    {
                        var ps = srvManagerPeriod.GetService();
                        ps.CompleteCopyingBasicData(currentPeriod.Id, preState);
                    }
                    finally
                    {
                        IsCopying = false;
                        basicDataCopyingProgress = new BasicDataCopyingProgress();
                        periodServiceFactory.Release(srvManagerPeriod);
                    }
                });
            publisher.RegisterHandler(copyCompletedSub);

            try
            {
                BasicDataCopyingProgress.SetProgress(100, 5);
                deleteAllPeriodData(currentPeriod);
                basicDataCopyingProgress.Messages.Add("اطلاعات پایه پیشین پاک شد.");

                basicDataCopyingProgress.SetProgress(100, 25);
                copyUnits(sourcePeriod, currentPeriod);
                basicDataCopyingProgress.Messages.Add("کپی واحد های سازمانی انجام شد.");


                basicDataCopyingProgress.SetProgress(100, 35);
                copyPeriodJobIndices(sourcePeriod, currentPeriod);
                basicDataCopyingProgress.Messages.Add("کپی شاخص ها انجام شد.");

                basicDataCopyingProgress.SetProgress(100, 45);
                copyJobs(sourcePeriod, currentPeriod);
                basicDataCopyingProgress.Messages.Add("کپی شغل ها انجام شد.");
                basicDataCopyingProgress.SetProgress(100, 65);

                copyJobPositions(sourcePeriod, currentPeriod);
                basicDataCopyingProgress.Messages.Add("کپی پست ها انجام شد.");
                basicDataCopyingProgress.SetProgress(100, 100);
                basicDataCopyingProgress.Messages.Add("اتمام عملیات کپی دوره.");
                lastPeriod=new KeyValuePair<PeriodId,List<string>>(currentPeriod.Id, basicDataCopyingProgress.Messages);
                publisher.Publish(new CopyBasicDataCompleted(currentPeriod,preState));
            }
            catch (Exception ex)
            {
                basicDataCopyingProgress.Messages.Add("خطا در کپی اطلاعات " + ex.Message);
                var logServiceMngt = LogServiceFactory.Create();
                try
                {
                    var logService = logServiceMngt.GetService();
                    logService.AddEventLog("Copy Exception",
                        LogLevel.Error,
                        null, this.GetType().Name, "start", ex.Message, ex.StackTrace);
                    logService.AddExceptionLog(ex);
                }
                finally
                {
                    LogServiceFactory.Release(logServiceMngt);
                }
                IsCopying = false;
                basicDataCopyingProgress = new BasicDataCopyingProgress();
            }
        }

        public void CopyBasicData(Period currentPeriod, Period sourcePeriod, IEventPublisher publisher)
        {
            if (!IsCopying)
            {
                IsCopying = true;
                Task.Factory.StartNew(() =>
                {

                    start(currentPeriod, sourcePeriod, publisher);  

                });

            }

        }

        private void copyUnit(Period currentPeriod, UnitId sourceUnitId, SharedUnitId parentId)
        {

            List<Unit> childs = new List<Unit>();
            var srvManagerUnit = unitServiceFactory.Create();
            Unit sourceUnit;
            try
            {
                var unitService = srvManagerUnit.GetService();
                sourceUnit = unitService.GetUnitBy(sourceUnitId);
                unitService.AssignUnit(currentPeriod.Id, sourceUnit.SharedUnit.Id, parentId);
                childs = unitService.GetAllUnitByParentId(sourceUnit.Id);
            }
            finally
            {
                unitServiceFactory.Release(srvManagerUnit);
            }

            foreach (Unit u in childs)
                copyUnit(currentPeriod, u.Id, sourceUnit.SharedUnit.Id);

        }

        private void copyUnits(Period sourcePeriod, Period currentPeriod)
        {
            List<Unit> parentUnitList = new List<Unit>();
            var srvManagerUnit = unitServiceFactory.Create();
            try
            {
                var unitService = srvManagerUnit.GetService();
                parentUnitList = unitService.GetAllParentUnits(sourcePeriod);
            }
            finally
            {
                unitServiceFactory.Release(srvManagerUnit);
            }
            foreach (Unit u in parentUnitList)
                copyUnit(currentPeriod, u.Id, null);
        }

        private void copyJob(Period currentPeriod, JobId jobId)
        {
            var srvManagerJob = jobServiceFactory.Create();
            try
            {
                var jobService = srvManagerJob.GetService();
                var sourcejob = jobService.GetJobById(jobId);
                //todo :(LOW) Copy from one period to another mist be check and debuge ( here must  
                var newJobJobIndices = new List<JobIndexForJob>();
                foreach (var jobJobIndex in sourcejob.JobIndexList)
                {
                    var newJobIndexId = getJobIndexIdForCopy(currentPeriod, jobJobIndex.JobIndexId);
                    newJobJobIndices.Add(new JobIndexForJob(newJobIndexId, jobJobIndex.ShowforTopLevel, jobJobIndex.ShowforSameLevel, jobJobIndex.ShowforLowLevel)); 
                }
                //List<AbstractJobIndexId> newJobIndexIds = getJobIndexIdsForCopy(currentPeriod, sourcejob.JobIndexList);
                jobService.AssignJob(new JobId(currentPeriod.Id, sourcejob.SharedJob.Id),
                    sourcejob.CustomFields.Select(c => c.SharedJobCustomField.Id).ToList(), newJobJobIndices);

            }
            finally
            {
                jobServiceFactory.Release(srvManagerJob);
            }

        }



        private void copyJobs(Period sourcePeriod, Period currentPeriod)
        {
            List<JobId> jobIdList = new List<JobId>();
            var srvManagerJob = jobServiceFactory.Create();
            try
            {
                var jobService = srvManagerJob.GetService();
                jobIdList = jobService.GetAllJobIdList(sourcePeriod.Id);
            }
            finally
            {
                jobServiceFactory.Release(srvManagerJob);
            }
            foreach (JobId jobId in jobIdList)
                copyJob(currentPeriod, jobId);
        }

        //private List<AbstractJobIndexId> getJobIndexIdsForCopy(Period currentPeriod, IReadOnlyList<AbstractJobIndexId> jobIndexIdList)
        //{
        //    var srvManagerJobIndex = jobIndexServiceFactory.Create();
        //    try
        //    {
        //        var jobIndexService = srvManagerJobIndex.GetService();
        //        List<SharedJobIndexId> sharedJobIndexIds = jobIndexService.GetSharedJobIndexIdBy(jobIndexIdList.ToList());
        //        List<AbstractJobIndexId> res = jobIndexService.GetJobIndexIdsBy(currentPeriod, sharedJobIndexIds);
        //        return res;
        //    }
        //    finally
        //    {
        //        jobIndexServiceFactory.Release(srvManagerJobIndex);
        //    }
        //}

        private AbstractJobIndexId getJobIndexIdForCopy(Period currentPeriod, AbstractJobIndexId jobIndexId)
        {
            var srvManagerJobIndex = jobIndexServiceFactory.Create();
            try
            {
                var jobIndexService = srvManagerJobIndex.GetService();
                var sharedJobIndexId = jobIndexService.GetSharedJobIndexIdBy(jobIndexId);
                AbstractJobIndexId res = jobIndexService.GetJobIndexIdBy(currentPeriod, sharedJobIndexId);
                return res;
            }
            finally
            {
                jobIndexServiceFactory.Release(srvManagerJobIndex);
            }
        }

        private void copyAbstractJobIndex(Period currentPeriod, AbstractJobIndexId sourceAbstractJobIndexId, AbstractJobIndexId parentId)
        {
            List<AbstractJobIndex> childs = new List<AbstractJobIndex>();
            JobIndexGroup newJobIndexGroup = null;
            var srvManagerJobIndex = jobIndexServiceFactory.Create();
            try
            {
                var jobIndexService = srvManagerJobIndex.GetService();
                var sourceAbstractJobIndex = jobIndexService.GetJobIndexById(sourceAbstractJobIndexId);
                if (sourceAbstractJobIndex is JobIndexGroup)
                {
                    var sourceGroup = sourceAbstractJobIndex as JobIndexGroup;
                    newJobIndexGroup = jobIndexService.AddJobIndexGroup(currentPeriod.Id, parentId, sourceGroup.Name, sourceGroup.DictionaryName);
                    childs = jobIndexService.GetAllAbstractJobIndexByParentId(sourceGroup.Id);
                }
                else if (sourceAbstractJobIndex is JobIndex)
                {
                    var sourceJobIndex = sourceAbstractJobIndex as JobIndex;
                    jobIndexService.AddJobIndex(currentPeriod.Id, parentId, sourceJobIndex.SharedJobIndexId,
                        sourceJobIndex.CustomFieldValues.ToDictionary(c => c.Key, c => c.Value), sourceJobIndex.IsInquireable, sourceJobIndex.CalculationOrder, sourceJobIndex.CalculationLevel);
                }
            }
            finally
            {
                jobIndexServiceFactory.Release(srvManagerJobIndex);
            }

            foreach (AbstractJobIndex abstactIndex in childs)
                copyAbstractJobIndex(currentPeriod, abstactIndex.Id, newJobIndexGroup.Id);

        }
        private void copyPeriodJobIndices(Period sourcePeriod, Period currentPeriod)
        {
            List<JobIndexGroup> parentJobIndexGroupList = new List<JobIndexGroup>();
            var srvManagerJobIndex = jobIndexServiceFactory.Create();
            try
            {
                var jobIndexService = srvManagerJobIndex.GetService();
                parentJobIndexGroupList = jobIndexService.GetAllParentJobIndexGroup(sourcePeriod);
            }
            finally
            {
                jobIndexServiceFactory.Release(srvManagerJobIndex);
            }
            foreach (JobIndexGroup group in parentJobIndexGroupList)
                copyAbstractJobIndex(currentPeriod, group.Id, null);
        }

        private void copyJobPosition(Period currentPeriod, JobPositionId sourceJobPositionId, SharedJobPositionId parentId)
        {
            var childs = new List<JobPosition>();
            var serviceManager = jobPositionServiceFactory.Create();
            JobPosition sourceJobPosition;
            try
            {
                var jobPositionService = serviceManager.GetService();
                sourceJobPosition = jobPositionService.GetBy(sourceJobPositionId);
                var newUnitId = getUnitIdForCopy(currentPeriod, sourceJobPosition.UnitId);
                var newJobId = getJobIdForCopy(currentPeriod, sourceJobPosition.JobId);
                jobPositionService.AssignJobPosition(currentPeriod.Id, sourceJobPosition.SharedJobPosition.Id, parentId, newJobId, newUnitId);
                childs = jobPositionService.GetAllJobPositionByParentId(sourceJobPosition.Id);
            }
            finally
            {
                jobPositionServiceFactory.Release(serviceManager);
            }

            foreach (JobPosition u in childs)
                copyJobPosition(currentPeriod, u.Id, sourceJobPosition.SharedJobPosition.Id);

        }
        private JobId getJobIdForCopy(Period currentPeriod, JobId jobId)
        {
            var srvManager = jobServiceFactory.Create();
            try
            {
                var jobService = srvManager.GetService();
                var res = jobService.GetJobIdBy(currentPeriod, jobId.SharedJobId);
                return res;
            }
            finally
            {
                jobServiceFactory.Release(srvManager);
            }
        }
        private UnitId getUnitIdForCopy(Period currentPeriod, UnitId unitId)
        {
            var srvManager = unitServiceFactory.Create();
            try
            {
                var unitService = srvManager.GetService();
                var res = unitService.GetUnitIdBy(currentPeriod, unitId.SharedUnitId);
                return res;
            }
            finally
            {
                unitServiceFactory.Release(srvManager);
            }

        }
        private void copyJobPositions(Period sourcePeriod, Period currentPeriod)
        {
            var parentJobPositionList = new List<JobPosition>();
            var srvManager = jobPositionServiceFactory.Create();
            try
            {
                var jobPositionService = srvManager.GetService();
                parentJobPositionList = jobPositionService.GetAllParentJobPositions(sourcePeriod);
            }
            finally
            {
                jobPositionServiceFactory.Release(srvManager);
            }
            foreach (JobPosition u in parentJobPositionList)
                copyJobPosition(currentPeriod, u.Id, null);
        }

        private void deleteAllPeriodData(Period currentPeriod)
        {
            var srvManager = periodServiceFactory.Create();
            try
            {
                var periodService = srvManager.GetService();
                periodService.RollBack(currentPeriod.Id);
            }
            finally
            {
                periodServiceFactory.Release(srvManager);
            }
        }
    }
}
