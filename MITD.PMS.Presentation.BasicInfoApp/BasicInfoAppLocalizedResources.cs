using MITD.PMS.Presentation.BasicInfoApp.Assets.LangResources;
using MITD.PMS.Presentation.Logic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.BasicInfoApp
{
    public class BasicInfoAppLocalizedResources : IBasicInfoAppLocalizedResources
    {
        public string JobListViewDrgJobListColumnName
        {
            get { return Resource.JobListViewDrgJobListColumnName; }
        }

        public string JobListViewDrgJobListColumnNameInVocab
        {
            get { return Resource.JobListViewDrgJobListColumnNameInVocab; }
        }

        public string JobListViewTitle
        {
            get { return Resource.JobListViewTitle; }
        }

        public string JobIndexCategoryListViewTitle
        {
            get { return "دسته شاخص"; }
        }
        public string JobIndexViewTitle
        {
            get { return "شاخص"; }
        }
        public string JobIndexListViewTitle
        {
            get { return "لیست شاخص ها"; }
        }
        public string JobIndexListViewFilterCommandTitle { get; private set; }

        public string CustomFieldListViewTitle
        {
            get { return "لیست فیلد ها"; }
        }

        public string CustomFieldListViewFilterCommandTitle
        {
            get { return "نمایش"; }
        }

        public string CustomFieldViewTitle
        {
            get { return "فیلد"; }
        }

        public string CustomFieldListViewFilterLabelTitle
        {
            get { return "نام موجودیت:"; }
        }

        public string CustomFieldListViewDrgCustomFieldListColumnName
        {
            get { return "نام"; }
        }

        public string CustomFieldListViewDrgCustomFieldListColumnEntityNameTitle
        {
            get { return "نام موجودیت"; }
        }

        public string CustomFieldListViewDrgCustomFieldListColumnCustomFieldTypeTitle
        {
            get { return "نوع فیلد"; }
        }

        public string CustomFieldListViewDrgCustomFieldListColumnMinVlueTitle
        {
            get { return "حد پایین"; }
        }

        public string CustomFieldListViewDrgCustomFieldListColumnMaxVlueTitle
        {
            get { return "حد بالا"; }
        }

        public string CustomFieldListViewDrgCustomFieldListColumnDictionaryNameTitle
        {
            get { return "نام در لغت نامهُ"; }
        }

        public string JobPositionListViewTitle
        {
            get { return Resource.JobPositionListViewTitle; }
        }

        public string JobViewAddFieldsCommandTitle
        {
            get { return "اضافه کردن فیلدها"; }
        }

        public string JobViewModifyFieldsCommandTitle
        {
            get { return "ویرایش کردن فیلدها"; }
        }

        public string JobCustomFieldManageViewTitle
        {
            get { return "مدیریت فیلد های شغل"; }
        }

        public string JobIndexCustomFieldManageViewTitle
        {
            get { return "مدیریت فیلد های شاخص"; }
        }

        public string UnitListViewTitle
        {
            get { return "مدیریت واحد های سازمانی"; }
        }

        public string PolicyListViewTitle
        {
            get { return "مدیریت نظام ها محاسبه عملکرد"; }
        }

        public string RuleListViewTitle
        {
            get { return "فهرست قوانین مرتبط با نظام محاسبه عملکرد"; }
        }

        public string PolicyViewTitle
        {
            get { return " نظام محاسبه عملکرد"; }
        }

        public string GridRuleListViewAddCommandTitle
        {
            get { return "ایجاد قانون"; }
        }

        public string GridRuleListViewTitle
        {
            get { return "فهرست قانون های"; }
        }

        public string RuleViewTitle
        {
            get { return "قانون"; }
        }

        public string RuleVersionViewTitle
        {
            get { return "مدیریت ورژن های قانون"; }
        }

        public string RuleVersionViewAddRuleVersionTitle
        {
            get { return "اضافه کردن ورژن جدید"; }
        }

        public string RuleVersionViewShowRuleVersionsTitle
        {
            get { return "مشاهده ورژن ها"; }
        }

        public string RuleVersionViewModifyRuleVersionTitle
        {
            get { return "ویرایش ورژن"; }
        }

        public string FunctionListViewTitle
        {
            get { return "فهرست توابع نظام محاسبه عملکرد"; }
        }

        public string FunctionViewTitle
        {
            get { return " تابع"; }
        }

        public string UserListViewTitle
        {
            get { return " فهرست کاربران"; }
        }

        public string UserViewTitle
        {
            get { return "کاربر"; }
        }

        public string UserGroupListViewTitle
        {
            get { return " فهرست گروه های کاربری"; }
        }

        public string UserGroupViewTitle
        {
            get { return "گروه کاربر"; }
        }

        public string UserListViewCommandTitle
        {
            get { return "نمایش کاربران"; }
        }

        public string ManagePartyCustomActions
        {
            get { return "تنظیم دسترسی ها"; }
        }

        public string LogListViewTitle
        {
            get { return "فهرست لاگ های سیستم"; }
        }

        public string RuleTrailListViewTitle
        {
            get { return "تاريخچه تغییرات واصلاحات قانون"; }
        }
    }
}
