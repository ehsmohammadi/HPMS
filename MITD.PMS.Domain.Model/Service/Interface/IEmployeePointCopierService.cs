using System;
using System.Collections.Generic;
using MITD.Core;
using MITD.PMS.Domain.Model.Periods;

namespace MITD.PMS.Domain.Service
{
    public class EmployeePointCopyingProgress
    {

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

    public interface IEmployeePointCopierService : IService
    {
        void CopyEmployeePoint(Period period,IEventPublisher publisher);
        bool IsCopying { get; }
        EmployeePointCopyingProgress EmployeePointCopyingProgress { get; }
    }
}
