using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MITD.Core;
using MITD.PMS.Domain.Model.JobPositions;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Model.Units;
using MITD.PMS.Domain.Service;

namespace MITD.PMS.Application
{
    public class InquiryConfiguratorService : IInquiryConfiguratorService
    {
        #region Fields

        private readonly IJobPositionServiceFactory jobPositionServiceFactory;
        private readonly IInquiryServiceFactory inquiryServiceFactory;
        private readonly IUnitServiceFactory unitServiceFactory;
        private readonly IUnitInquiryServiceFactory unitInquiryServiceFactory;
        private readonly IPeriodServiceFactory periodServiceFactory;

        private readonly object runningLockObject = new object();
        private readonly object progressLockObject = new object();

        #endregion

        #region Properties & BackFields

        private InquiryInitializingProgress inquiryInitializingProgress = new InquiryInitializingProgress();
        public InquiryInitializingProgress InquiryInitializingProgress
        {
            get
            {
                lock (progressLockObject)
                {
                    return inquiryInitializingProgress;
                }
            }
        }

        private bool isRunning = false;
        public bool IsRunning
        {
            get
            {
                lock (runningLockObject)
                {
                    return isRunning;
                }

            }
            private set
            {
                lock (runningLockObject)
                {
                    isRunning = value;
                }
            }
        }
        #endregion

        #region Constructors

        public InquiryConfiguratorService(IJobPositionServiceFactory jobPositionServiceFactory,
            IInquiryServiceFactory inquiryServiceFactory, IUnitServiceFactory unitServiceFactory,
            IUnitInquiryServiceFactory unitInquiryServiceFactory,
            IPeriodServiceFactory periodServiceFactory)
        {
            this.jobPositionServiceFactory = jobPositionServiceFactory;
            this.inquiryServiceFactory = inquiryServiceFactory;
            this.unitServiceFactory = unitServiceFactory;
            this.unitInquiryServiceFactory = unitInquiryServiceFactory;
            this.periodServiceFactory = periodServiceFactory;

        }

        #endregion

        #region Methods

        public long GetNumberOfConfiguredJobPosition(Period period)
        {
            var jobPositionIds = new List<JobPositionId>();
            var serviceManager = jobPositionServiceFactory.Create();
            try
            {
                var jpService = serviceManager.GetService();
                jobPositionIds = jpService.GetAllJobPositionId(period);
            }
            finally
            {
                jobPositionServiceFactory.Release(serviceManager);
            }
            return jobPositionIds.Count();
        }

        public long GetNumberOfConfiguredUnit(Period period)
        {
            var unitIds = new List<UnitId>();
            var serviceManager = unitServiceFactory.Create();
            try
            {
                var uService = serviceManager.GetService();
                unitIds = uService.GetAllUnitId(period);
            }
            finally
            {
                unitServiceFactory.Release(serviceManager);
            }
            return unitIds.Count();
        }

        public void Configure(Period period, IEventPublisher publisher)
        {
            if (!IsRunning)
            {
                isRunning = true;
                inquiryInitializingProgress.State = new PeriodInitializingForInquiryState();
                inquiryInitializingProgress.Messages.Add("شروع آماده سازی دوره");

                #region JobPosition

                var jobPositionIds = new List<JobPositionId>();
                var srvManagerJob = jobPositionServiceFactory.Create();
                try
                {
                    var jpService = srvManagerJob.GetService();
                    jobPositionIds = jpService.GetAllJobPositionId(period);
                }
                catch (Exception exp)
                {
                    isRunning = false;
                    inquiryInitializingProgress.State = new PeriodInitializingForInquiryState();
                    throw exp;
                }
                finally
                {
                    jobPositionServiceFactory.Release(srvManagerJob);
                }

                var jobPositionTotalCount = jobPositionIds.Count();

                #endregion

                #region Unit

                var unitIds = new List<UnitId>();
                var srvManagerUnit = unitServiceFactory.Create();
                try
                {
                    var uService = srvManagerUnit.GetService();
                    unitIds = uService.GetAllUnitId(period);
                }
                catch (Exception exp)
                {
                    isRunning = false;
                    inquiryInitializingProgress.State = new PeriodInitializingForInquiryState();
                    throw exp;
                }
                finally
                {
                    unitServiceFactory.Release(srvManagerUnit);
                }

                var unitTotalCount = unitIds.Count();

                #endregion

                inquiryInitializingProgress.Messages.Add("تعداد " + jobPositionTotalCount + " پست برای پیکر بندی نظر سنجی آماده می باشد");
                inquiryInitializingProgress.Messages.Add("تعداد " + unitTotalCount + " واحد برای پیکر بندی نظر سنجی آماده می باشد");

                var totalCount = jobPositionTotalCount + unitTotalCount;
                
                Task.Factory.StartNew(() =>
                {
                    try
                    {
                        #region JobPosition

                        long jobPositionIndex = 0;
                        foreach (var jobPositionId in jobPositionIds)
                        {
                            JobPosition jobPosition;
                            var srvManagerJobPosition = jobPositionServiceFactory.Create();
                            try
                            {
                                var jps = srvManagerJobPosition.GetService();
                                jobPosition = jps.ConfigureInquiry(jobPositionId,true);
                                var dummy = jobPosition.ConfigurationItemList;
                            }
                            finally
                            {
                                jobPositionServiceFactory.Release(srvManagerJobPosition);
                            }


                            foreach (var itm in jobPosition.ConfigurationItemList)
                            {
                                var srvManagerInquiry = inquiryServiceFactory.Create();

                                try
                                {
                                    var iqs = srvManagerInquiry.GetService();
                                    iqs.CreateAllInquiryJobIndexPoint(itm);
                                }
                                finally
                                {
                                    inquiryServiceFactory.Release(srvManagerInquiry);
                                }
                            }
                            jobPositionIndex++;
                            inquiryInitializingProgress.SetProgress(totalCount, jobPositionIndex);

                        } 

                        #endregion

                        #region Unit

                        long unitIndex = 0;
                        foreach (var unitId in unitIds)
                        {
                            Unit unit;
                            var srvManagerUnitInquiry = unitServiceFactory.Create();
                            try
                            {
                                var us = srvManagerUnitInquiry.GetService();
                                unit = us.GetUnitBy(unitId);
                                var dummy = unit.ConfigurationItemList;
                            }
                            finally
                            {
                                unitServiceFactory.Release(srvManagerUnitInquiry);
                            }


                            foreach (var itm in unit.ConfigurationItemList)
                            {
                                var srvManagerUnitInquiry2 = unitInquiryServiceFactory.Create();

                                try
                                {
                                    var iqs = srvManagerUnitInquiry2.GetService();
                                    iqs.CreateAllInquiryUnitIndexPoint(itm);
                                }
                                finally
                                {
                                    unitInquiryServiceFactory.Release(srvManagerUnitInquiry2);
                                }
                            }
                            unitIndex++;
                            inquiryInitializingProgress.SetProgress(totalCount, unitIndex);

                        }

                        #endregion

                        inquiryInitializingProgress.Messages.Add("اتمام آماده سازی دوره برای نظر سنجی");

                        var serviceManager = periodServiceFactory.Create();
                        try
                        {
                            var ps = serviceManager.GetService();
                            ps.CompleteInitializeInquiry(period.Id);
                        }
                        catch (Exception exp)
                        {
                            throw exp;
                        }
                        finally
                        {
                            isRunning = false;
                            inquiryInitializingProgress = new InquiryInitializingProgress();
                            periodServiceFactory.Release(serviceManager);
                        }
                        
                    }
                    catch (Exception exp)
                    {
                        isRunning = false;
                        inquiryInitializingProgress.State = new PeriodInitializingForInquiryState();
                        throw exp;
                    }
                });

            }
        }

        #endregion

    }
}
