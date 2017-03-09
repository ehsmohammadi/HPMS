using System;
using System.Globalization;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class EmployeeDTO : ViewModelBase
    {
        public string FullName { get { return FirstName + " " + LastName; } }

        public string PerformanceLevel 
        {
            get
            {
                var point = FinalPoint;
                if (point >= 90)
                    return "عالی";
                if (point < 90 && point >= 70)
                    return "خوب";
                if (point < 70 && point >= 50)
                    return "قابل قبول";
                if (point < 50 && point >= 30)
                    return "نیاز به آموزش";
                if (point < 30 && point > 0)
                    return "نا مطلوب";
                return "-";   
            }
        }
    }
}
