using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MITD.Core;
using MITD.PMS.Application.Contracts;
using MITD.PMS.Domain.Model.JobPositions;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Service;
using System;


namespace MITD.PMS.Application
{
    public class InquiryConfiguratorService : IInquiryConfiguratorService
    {
        private readonly IJobPositionServiceFactory jobPositionServiceFactory;
        private readonly IInquiryServiceFactory inquiryServiceFactory;
        private readonly IPeriodServiceFactory periodServiceFactory;
        private InquiryInitializingProgress inquiryInitializingProgress = new InquiryInitializingProgress();
        private readonly object runningLockObject = new object();
        private readonly object progressLockObject = new object();

        private bool isRunning = false;

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

        public InquiryConfiguratorService(IJobPositionServiceFactory jobPositionServiceFactory,
                                          IInquiryServiceFactory inquiryServiceFactory,
                                          IPeriodServiceFactory periodServiceFactory)
        {
            this.jobPositionServiceFactory = jobPositionServiceFactory;
            this.inquiryServiceFactory = inquiryServiceFactory;
            this.periodServiceFactory = periodServiceFactory;

        }

        public void Configure(Period period, IEventPublisher publisher)
        {
            if (!IsRunning)
            {

                isRunning = true;
                inquiryInitializingProgress.State = new PeriodInitializingForInquiryState();
                inquiryInitializingProgress.Messages.Add("شروع آماده سازی دوره");
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
                var totalCount = jobPositionIds.Count();
                inquiryInitializingProgress.Messages.Add("تعداد " + totalCount + " پست برای پیکر بندی نظر سنجی آماده می باشد");
                Task.Factory.StartNew(() =>
                {

                    //var sub = new DelegateHandler<InitializeInquiryCompleted>(e =>
                    //{


                    //});

                    //publisher.RegisterHandler(sub);
                    try
                    {
                        long index = 0;
                        foreach (var jobPositionId in jobPositionIds)
                        {
                            JobPosition jobPosition;
                            var srvManagerJobPosition = jobPositionServiceFactory.Create();
                            try
                            {
                                var jps = srvManagerJobPosition.GetService();
                                jobPosition = jps.ConfigureInquiry(jobPositionId);
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
                            index++;
                            inquiryInitializingProgress.SetProgress(totalCount, index);

                        }
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
                        //publisher.Publish(new InitializeInquiryCompleted(period));
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

    }
}
