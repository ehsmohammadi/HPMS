using System;
using System.Collections.Generic;
using MITD.Core;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.JobPositions;
using MITD.PMS.Domain.Model.Periods;

namespace MITD.PMS.Domain.Service
{
    public class BasicDataCopyingProgress
    {
        //private long totalUnits = 0;
        //private long totalJobs = 0;
        //private long totalJobIndices = 0;
        //private long totalJobPositions = 0;
        //private long unitsCopyCompleteNo = 0;
        //private long JobsCopyCompleteNo = 0;
        //private long JobIndicesCopyCompleteNo = 0;
        //private long JobPositionsCopyCompleteNo = 0;

        //public long TotalJobPosition { get { return totalUnits; } }
        //public long TotalJobs { get { return totalJobs; } }
        //public long TotalJobIndices { get { return totalJobIndices; } }
        //public long TotalJobPositions { get { return totalJobPositions; } } 

        private long totalItemsNo = 0;
        private long itemsCompletedNo = 0;
        private readonly List<string> messages = new List<string>();

        public PeriodState State { get; set; }
        public List<string> Messages { get { return messages; } }
        public int Percent
        {
            get { return totalItemsNo == 0 ? 0 : Convert.ToInt32((itemsCompletedNo * 100) / totalItemsNo); }
        }
        public void SetProgress(long totalItemsNo, long itemsCompletedNo)
        {
            this.totalItemsNo = totalItemsNo;
            this.itemsCompletedNo = itemsCompletedNo;
        }
        public void SetMessage(string message)
        {
            messages.Add(message);
        }
    }

    public interface IPeriodBasicDataCopierService
    {
        void CopyBasicData(Period currentPeriod, Period sourcePeriod, IEventPublisher publisher);
        bool IsCopying { get; }
        BasicDataCopyingProgress BasicDataCopyingProgress { get; }
        KeyValuePair<PeriodId, List<string>> LastPeriod { get; }
    }
}
