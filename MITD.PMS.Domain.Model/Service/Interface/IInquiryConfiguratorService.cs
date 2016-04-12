using System;
using System.Collections.Generic;
using MITD.Core;
using MITD.PMS.Domain.Model.JobPositions;
using MITD.PMS.Domain.Model.Periods;

namespace MITD.PMS.Domain.Service
{
    public class InquiryInitializingProgress
    {
        private long totalJobPosition = 0;
        private long jobPositionsConfigured = 0;
        private readonly List<string> messages = new List<string>();

        public List<string> Messages { get { return messages; } }
        public long TotalJobPosition { get { return totalJobPosition; } }
        public long JobPositionsConfigured { get { return jobPositionsConfigured; } }
        public int Percent
        {
            get { return totalJobPosition == 0 ? 0 : Convert.ToInt32((jobPositionsConfigured * 100) / totalJobPosition); }
        }

        public PeriodState State { get; set; }
        public void SetProgress(long totalJobPosition, long jobPositionsConfigured)
        {
            this.totalJobPosition = totalJobPosition;
            this.jobPositionsConfigured = jobPositionsConfigured;
        }
        public void SetMessage(string message)
        {
            messages.Add(message);
        }
    }

    public interface IInquiryConfiguratorService
    {
        void Configure(Period period, IEventPublisher publisher);
        InquiryInitializingProgress InquiryInitializingProgress { get; }
        bool IsRunning { get; }
        long GetNumberOfConfiguredJobPosition(Period period);
        long GetNumberOfConfiguredUnit(Period period);
    }
}
