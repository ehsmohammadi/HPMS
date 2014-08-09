using System;
using MITD.Core;
#if SILVERLIGHT 

namespace MITD.PMS.Presentation.Contracts
{
    public class PeriodStateEnum : Enumeration
    {

#else
using MITD.Domain.Model;

namespace MITD.PMS.Domain.Model.Periods
{
    public class PeriodStateEnum : Enumeration
    {
#endif
        public static readonly PeriodStateEnum Init = new PeriodStateEnum("1", "PeriodInitState", "جدید");
        public static readonly PeriodStateEnum BasicDataCopying = new PeriodStateEnum("2", "PeriodBasicDataCopying", "در حال کپی");
        public static readonly PeriodStateEnum InitializingForInquiry = new PeriodStateEnum("3", "PeriodInitializingForInquiryState", "در حال آماده سازی برای نظرسنجی");
        public static readonly PeriodStateEnum InitializingForInquiryCompleted = new PeriodStateEnum("4", "PeriodInitializeInquiryCompletedState", " آماده سازی برای نظر سنجی پایان یافته");
        public static readonly PeriodStateEnum InquiryStarted = new PeriodStateEnum("5", "PeriodInquiryStartedState", "نظر سنجی آغاز شده");
        public static readonly PeriodStateEnum InquiryCompleted = new PeriodStateEnum("6", "PeriodInquiryCompletedState", "نظر سنجی پایان یافته");
        public static readonly PeriodStateEnum ClaimingStarted = new PeriodStateEnum("7", "PeriodClaimingStartedState", "زمان اعتراضات آغاز شده");
        public static readonly PeriodStateEnum ClaimingFinished = new PeriodStateEnum("8", "PeriodClaimingFinishedState", "زمان اعتراضات پایان یافته");
        public static readonly PeriodStateEnum Closed = new PeriodStateEnum("9", "PeriodClosedState", "بسته");
        
        private readonly string description;
        public virtual string Description
        {
            get { return description; }
        }



        public PeriodStateEnum(string value)
            : base(value)
        {
        }

        public PeriodStateEnum(string value, string displayName, string description)
            : base(value, displayName)
        {
            this.description = description;
        }




        public bool SameValueAs(PeriodStateEnum other)
        {
            return Equals(other);
        }

        public static explicit operator int(PeriodStateEnum x)
        {
            if (x == null)
            {
                //throw new InvalidCastException();
                return -1;

            }
            
            return Convert.ToInt32(x.Value);

        }

        public static implicit operator PeriodStateEnum(int val)
        {
            return Enumeration.FromValue<PeriodStateEnum>(val.ToString());
        }

    }
}
