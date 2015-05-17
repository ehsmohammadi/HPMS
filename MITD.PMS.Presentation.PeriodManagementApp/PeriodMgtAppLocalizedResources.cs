using MITD.PMS.Presentation.PeriodManagementApp.Assets.LangResources;
using MITD.PMS.Presentation.Logic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.PeriodManagementApp
{
    public class PeriodMgtAppLocalizedResources : IPeriodMgtAppLocalizedResources
    {
        public string PeriodViewTitle
        {
            get { return "دوره"; }
        }

        public string PeriodViewLabelPeriodNameTitle
        {
            get { return "نام دوره"; }
        }

        public string PeriodViewLabelStartDateTitle
        {
            get { return "تاریخ شروع:"; }
        }

        public string PeriodViewLabelEndDateTitle
        {
            get { return "تاریخ پایان:"; }
        }

        public string PeriodListViewTitle
        {
            get { return "لیست دوره ها"; }
        }

        public string UnitInPeriodViewTitle
        {
            get { return "واحد سازمانی در دوره"; }
        }

        public string UnitInPeriodTreeViewTitle
        {
            get { return "مدیریت واحد سازمانی دوره"; }
        }

        public string JobPositionInPeriodViewTitle
        {
            get { return " پست سازمانی دوره"; }
        }

        public string JobPositionInPeriodTreeViewTitle
        {
            get { return "مدیریت پست سازمانی دوره"; }
        }

        public string JobIndexInPeriodTreeViewTitle
        {
            get { return "مدیریت شاخص دوره"; }
        }

        public string JobIndexInPeriodViewTitle
        {
            get { return "شاخص دوره"; }
        }

        public string JobIndexGroupInPeriodViewTitle
        {
            get { return "گروه شاخص در دوره"; }
        }

        public string JobPositionInPeriodInquiryView
        {
            get { return "مدیریت پیکربندی نظر سنجی"; }
        }

        public string CalculationListViewTitle
        {
            get { return "فهرست محاسبه های دوره"; }
        }

        public string ClaimListViewTitle 
        {
            get { return "فهرست اعتراض های ثبت شده"; } 
        }

        public string ClaimViewTitle
        {
            get { return "اعتراض به نمرات"; }
        }

        public string ShowClaimViewTitle
        {
            get { return "نمایش درخواست اعتراض"; }
        }


        public string PermittedUserListToMyTasksViewTitle 
        {
            get { return "فهرست افراد مجاز به دیدن کارتابل من"; }
        }

        public string PermittedUserToMyTasksViewTitle
        {
            get { return "اضافه کردن افراد مجاز به دیدن کارتابل"; }
        }

        public string PeriodBasicDataCopyViewTitle
        {
            get { return "کپی اطلاعات پایه دوره"; }
        }

        public string CalculationExceptionListViewTitle
        {
            get { return "فهرست خطاهای  محاسبه"; }
        }

        public string CalculationExceptionViewTitle
        {
            get { return "مشاهده خطای  محاسبه"; }
        }

    }
}
