using System;
using System.Collections.Generic;

#if SILVERLIGHT

namespace MITD.PMS.Presentation.Contracts
{
    //public class ActionType : Enumeration
    //{

#else

namespace MITD.PMSSecurity.Domain
{

#endif

    /// <summary>
    /// This attribute is used to assign action types to methods.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class RequiredPermissionAttribute : Attribute
    {
        /// <summary>
        /// Unique type  for each action.
        /// </summary>
        public ActionType ActionType { get; private set; }

        public RequiredPermissionAttribute(ActionType actionType)
        {
            this.ActionType = actionType;
        }
    }

    public static class EnumExtensions
    {
        public static TAttribute GetAttribute<TAttribute>(this Enum value)
            where TAttribute : Attribute
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            return (TAttribute)type.GetField(name).GetCustomAttributes(typeof(TAttribute), false)[0];
        }
    }
    public class ActionInfoAttribute : Attribute
    {
        internal ActionInfoAttribute(string displayName, string description)
        {
            this.DisplayName = displayName;
            this.Description = description;
        }
        public string DisplayName { get; private set; }
        public string Description { get; private set; }
    }

    public static class ActionTypeHelper
    {
        //public static ActionType? FindActionType(string code)
        //{
        //    var enumItems = Enum.GetValues(typeof(ActionType));
        //    foreach (ActionType item in enumItems)
        //    {
        //        if (item.GetAttribute<ActionInfoAttribute>().Value == code)
        //            return item;
        //    }
        //    return null;
        //}

        public static List<ActionType> SelectActionTypes(Dictionary<int,bool>.KeyCollection codes)
        {
            List<ActionType> result = new List<ActionType>();
            foreach (var code in codes)
            {
                result.Add((ActionType)code);
            }
            return result;
        }
    }

    public enum ActionType
    {
        #region PMSAdmin

        #region Policy

        [ActionInfoAttribute("AddPolicy", "ایجاد نظام محاسبه عملکرد")]
        AddPolicy = 280,
        [ActionInfoAttribute("DeletePolicy", "حذف نظام محاسبه عملکرد")]
        DeletePolicy = 281,
        [ActionInfoAttribute("ModifyPolicy", "ویرایش نظام محاسبه عملکرد")]
        ModifyPolicy = 282,
        [ActionInfoAttribute("ManagePolicies", "مدیریت نظام محاسبه عملکرد")]
        ManagePolicies = 283,
        [ActionInfoAttribute("ShowPolicies", "نمایش نظام محاسبه عملکرد")]
        ShowPolicies = 284,

        [ActionInfoAttribute("ManageRules", "مدیریت قوانین")]
        ManageRules = 290,
        [ActionInfoAttribute("ManageFunctions", "مدیریت توابع")]
        ManageFunctions = 291,

        [ActionInfoAttribute("AddRule", "ایجاد قانون")]
        AddRule = 310,
        [ActionInfoAttribute("DeleteRule", "حذف قانون")]
        DeleteRule = 311,
        [ActionInfoAttribute("ModifyRule", "ویرایش قانون")]
        ModifyRule = 312,
        [ActionInfoAttribute("ShowRuleTrail", "نمایش سابقه قانون")]
        ShowRuleTrail = 313,
        [ActionInfoAttribute("ShowAllRuleTrails", "نمایش سوابق تغییرات قانون")]
        ShowAllRuleTrails = 314,

        #endregion

        #endregion

        [ActionInfoAttribute("AddPeriod", "ایجاد دوره")]
        AddPeriod = 100,
        [ActionInfoAttribute("ModifyPeriod", "ویرایش دوره")]
        ModifyPeriod = 101,
        [ActionInfoAttribute("DeletePeriod", "حذف دوره")]
        DeletePeriod = 102,
        [ActionInfoAttribute("ManageUnits", "مدیریت واحدهای سازمانی دوره")]
        ManageUnits = 103,
        [ActionInfoAttribute("ManageUnitIndices", "مدیریت شاخص های سازمانی دوره ")]
        ManageUnitIndices = 104,
        [ActionInfoAttribute("ManageJobIndices", "مدیریت شاخص های دوره ")]
        ManageJobIndices = 105,
        [ActionInfoAttribute("ManageJobs", "مدیریت مشاغل دوره")]
        ManageJobs = 106,
        [ActionInfoAttribute("ManageJobPositions", "مدیریت پست های سازمانی دوره")]
        ManageJobPositions = 107,
        [ActionInfoAttribute("ManageEmployees", "مدیریت کارمندان دوره")]
        ManageEmployees = 108,
        [ActionInfoAttribute("ManageCalculations", "مدیریت محاسبات دوره")]
        ManageCalculations = 109,
        [ActionInfoAttribute("ActivatePeriod", "فعال سازی دوره")]
        ActivatePeriod = 110,
        [ActionInfoAttribute("InitializePeriodForInquiry", "آماده سازی دوره برای نظرسنجی")]
        InitializePeriodForInquiry = 111,
        [ActionInfoAttribute("StartInquiry", "شروع نظرسنجی ")]
        StartInquiry = 112,
        [ActionInfoAttribute("CompelteInquiry", "اتمام نظرسنجی ")]
        CompleteInquiry = 113,
        [ActionInfoAttribute("StartCliaming", "شروع زمان ثبت اعتراض ")]
        StartCliaming = 114,
        [ActionInfoAttribute("FinishCliaming", "پايان زمان ثبت اعتراض ")]
        FinishCliaming = 115,
        [ActionInfoAttribute("ClosePeriod", "بستن دوره ")]
        ClosePeriod = 116,
        [ActionInfoAttribute("CopyPeriodBasicData", "کپی اطلاعات از دوره های قبل ")]
        CopyPeriodBasicData = 117,
        [ActionInfoAttribute("GetPeriodInitializingInquiryStatus", " نمایش وضعیت آماده سازی اجرای نظرسنجی")]
        GetPeriodInitializingInquiryStatus = 118,
        [ActionInfoAttribute("RollBackPeriodState", " برگشت دوره به وضعیت قبل ")]
        RollBackPeriodState = 119,
        [ActionInfoAttribute("AddJobInPeriod", "تخصیص شغل در دوره")]
        AddJobInPeriod = 120,
        [ActionInfoAttribute("ModifyJobInPeriod", "ویرایش شغل در دوره")]
        ModifyJobInPeriod = 121,
        [ActionInfoAttribute("DeleteJobInPeriod", "حذف شغل در دوره")]
        DeleteJobInPeriod = 122,
        // ManageJobInPeriodCustomFields , [ActionInfoAttribute("123", "ManageJobInPeriodCustomFields","مدیریت فیلدهای دلخواه شغل در دوره")]
        [ActionInfoAttribute("ShowPeriod", "نمایش دوره")]
        ShowPeriod = 123,
        [ActionInfoAttribute("ShowJobInPeriod", "نمایش شغل در دوره")]
        ShowJobInPeriod = 124,
        [ActionInfoAttribute("ShowEmployees", "نمایش کارمندان دوره")]
        ShowEmployees = 125,
        [ActionInfoAttribute("ShowCalculations", "نمایش محاسبات دوره")]
        ShowCalculations = 126,
        [ActionInfoAttribute("ShowJobs", "نمایش مشاغل دوره")]
        ShowJobs = 127,

        [ActionInfoAttribute("AddUnitInPeriod", "تخصیص واحد در دوره")]
        AddUnitInPeriod = 130,
        [ActionInfoAttribute("ModifyUnitInPeriod", "ویرایش واحد در دوره")]
        ModifyUnitInPeriod = 132,
        [ActionInfoAttribute("DeleteUnitInPeriod", "حذف واحد از دوره")]
        DeleteUnitInPeriod = 131,
        [ActionInfoAttribute("ShowUnitInPeriod", "نمایش واحد سازمانی در دوره")]
        ShowUnitInPeriod = 133,
        [ActionInfoAttribute("ManageUnitInPeriodInquiry", "تعیین افراد نظر دهنده واحد سازمانی")]
        ManageUnitInPeriodInquiry = 134,
        [ActionInfoAttribute("ShowUnitInPeriodInquiry", "نمایش افراد نظر دهنده واحد سازمانی")]
        ShowUnitInPeriodInquiry = 135,
        [ActionInfoAttribute("AddJobPositionInPeriod", "تخصیص پست در دوره")]
        AddJobPositionInPeriod = 140,
        [ActionInfoAttribute("DeleteJobPositionInPeriod", "حذف پست از دوره")]
        DeleteJobPositionInPeriod = 141,
        [ActionInfoAttribute("ManageJobPositionInPeriodInquiry", "تعیین افراد نظر دهنده پست سازمانی")]
        ManageJobPositionInPeriodInquiry = 142,
        [ActionInfoAttribute("ShowJobPositionInPeriod", "نمایش پست در دوره")]
        ShowJobPositionInPeriod = 143,
        [ActionInfoAttribute("AddJobIndexInPeriod", "تخصیص شاخص در دوره")]
        AddJobIndexInPeriod = 150,
        [ActionInfoAttribute("ModifyJobIndexInPeriod", "ویرایش شاخص در دوره")]
        ModifyJobIndexInPeriod = 151,
        [ActionInfoAttribute("DeleteJobIndexInPeriod", "حذف شاخص از دوره")]
        DeleteJobIndexInPeriod = 152,
        [ActionInfoAttribute("AddJobIndexGroupInPeriod", "ایجاد گروه شاخص در دوره")]
        AddJobIndexGroupInPeriod = 153,
        [ActionInfoAttribute("ModifyJobIndexGroupInPeriod", "ویرایش گروه شاخص در دروه")]
        ModifyJobIndexGroupInPeriod = 154,
        [ActionInfoAttribute("DeleteJobIndexGroupInPeriod", "حذف گروه شاخص از دوره")]
        DeleteJobIndexGroupInPeriod = 155,
        [ActionInfoAttribute("ShowJobIndexInPeriod", "نمایش شاخص در دوره")]
        ShowJobIndexInPeriod = 156,
        [ActionInfoAttribute("AddEmployee", "ایجاد کارمند")]
        AddEmployee = 160,
        [ActionInfoAttribute("ModifyEmployee", "ویزایش کارمند")]
        ModifyEmployee = 161,
        [ActionInfoAttribute("DeleteEmployee", "حذف کارمند")]
        DeleteEmployee = 162,
        [ActionInfoAttribute("ManageEmployeeJobPositions", "مدیریت پست های سازمانی کارمند")]
        ManageEmployeeJobPositions = 163,
        [ActionInfoAttribute("AddEmployeeJobCustomFields", "ایجاد فیلدهای دلخواه کارمند")]
        AddEmployeeJobCustomFields = 164,

        [ActionInfoAttribute("ModifyEmployeeJobCustomFields", "ویرایش فیلدهای دلخواه کارمند")]
        ModifyEmployeeJobCustomFields = 165,
        // GetEmployeeJobPositions , [ActionInfoAttribute("166", "GetEmployeeJobPositions","")]
        [ActionInfoAttribute("ShowEmployeeInquiery", "نمایش نظر سنجی کارمند")]
        ShowEmployeeInquiry = 166,
        [ActionInfoAttribute("AddJobIndex", "یجاد شاخص شغل")]
        AddJobIndex = 210,
        [ActionInfoAttribute("ModifyJobIndex", "ویرایش شاخص شغل")]
        ModifyJobIndex = 211,
        [ActionInfoAttribute("DeleteJobIndex", "حذف شاخص شغل")]
        DeleteJobIndex = 212,
        [ActionInfoAttribute("ManageJobIndexCustomFields", "مدیریت فیلدهای دلخواه شاخص")]
        ManageJobIndexCustomFields = 213,
        [ActionInfoAttribute("AddJobIndexCustomFields", "ایجاد فیلد دلخواه برای شاخص")]
        AddJobIndexCustomFields = 214,
        [ActionInfoAttribute("AddUnitIndex", "ایجاد شاخص")]
        AddUnitIndex = 215,
        [ActionInfoAttribute("ModifyUnitIndex", "ویرایش شاخص")]
        ModifyUnitIndex = 216,
        [ActionInfoAttribute("DeleteUnitIndex", "حذف شاخص")]
        DeleteUnitIndex = 217,
        [ActionInfoAttribute("ManageUnitIndexCustomFields", "مدیریت فیلدهای دلخواه شاخص")]
        ManageUnitIndexCustomFields = 218,
        [ActionInfoAttribute("AddUnitIndexCustomFields", "ایجاد فیلد دلخواه برای شاخص")]
        AddUnitIndexCustomFields = 219,
        [ActionInfoAttribute("AddJobIndexCategory", "ایجاد دسته شاخص")]
        AddJobIndexCategory = 220,
        [ActionInfoAttribute("DeleteJobIndexCategory", "حذف دسته شاخص")]
        DeleteJobIndexCategory = 221,
        [ActionInfoAttribute("ModifyJobIndexCategory", "ویرایش دسته شاخص")]
        ModifyJobIndexCategory = 222,
        [ActionInfoAttribute("AddUnitIndexCategory", "ایجاد دسته شاخص")]
        AddUnitIndexCategory = 223,
        [ActionInfoAttribute("DeleteUnitIndexCategory", "حذف دسته شاخص")]
        DeleteUnitIndexCategory = 224,
        [ActionInfoAttribute("ModifyUnitIndexCategory", "ویرایش دسته شاخص")]
        ModifyUnitIndexCategory = 225,
        [ActionInfoAttribute("ShowUnitIndexInPeriod", "نمایش شاخص")]
        ShowUnitIndexInPeriod = 226,
        [ActionInfoAttribute("ShowJobIndex", "نمایش شاخص شغل")]
        ShowJobIndex = 227,
        [ActionInfoAttribute("ShowUnitIndex", "نمایش شاخص واحد")]
        ShowUnitIndex = 228,
        [ActionInfoAttribute("AddJobPosition", "ایجاد پست سازمانی")]
        AddJobPosition = 230,
        [ActionInfoAttribute("DeleteJobPosition", "حذف پست سازمانی")]
        DeleteJobPosition = 231,
        [ActionInfoAttribute("ModifyJobPosition", "ویرایش پست سازمانی")]
        ModifyJobPosition = 232,
        [ActionInfoAttribute("ShowJobPosition", "نمایش پست سازمانی")]
        ShowJobPosition = 233,
        [ActionInfoAttribute("AddFunction", "ایجاد تابع")]
        AddFunction = 240,
        [ActionInfoAttribute("DeleteFunction", "حذف تابع")]
        DeleteFunction = 241,
        [ActionInfoAttribute("ModifyFunction", "ویرایش تابع")]
        ModifyFunction = 242,
        [ActionInfoAttribute("AddCustomField", "ایجاد فیلد دلخواه")]
        AddCustomField = 250,
        [ActionInfoAttribute("DeleteCustomField", "حذف فیلد دلخواه")]
        DeleteCustomField = 251,
        [ActionInfoAttribute("ModifyCustomField", "ویرایش فیلد دلخواه")]
        ModifyCustomField = 252,
        [ActionInfoAttribute("ShowCustomField", "نمایش فیلد دلخواه")]
        ShowCustomField = 253,
        [ActionInfoAttribute("AddJob", "ایجاد شغل")]
        AddJob = 260,
        [ActionInfoAttribute("DeleteJob", "حذف شغل")]
        DeleteJob = 261,
        [ActionInfoAttribute("ModifyJob", "ویرایش شغل")]
        ModifyJob = 262,
        [ActionInfoAttribute("ManageJobCustomFields", "مدیریت فیلدهای دلخواه شغل")]
        ManageJobCustomFields = 263,
        [ActionInfoAttribute("AddJobCustomFields", "ایجاد فیلد دلخواه برای شغل")]
        AddJobCustomFields = 264,
        [ActionInfoAttribute("AddUnit", "ایجاد واحد سازمانی")]
        AddUnit = 270,
        [ActionInfoAttribute("DeleteUnit", "حذف واحد سازمانی")]
        DeleteUnit = 271,
        [ActionInfoAttribute("ShowUnit", "نمایش واحد سازمانی")]
        ShowUnit = 272,
        [ActionInfoAttribute("ManageUnitCustomFields", "مدیریت فیلدهای دلخواه واحد")]
        ManageUnitCustomFields = 273,
        [ActionInfoAttribute("AddUnitCustomFields", "ایجاد فیلد دلخواه برای واحد")]
        AddUnitCustomFields = 274,
        [ActionInfoAttribute("ModifyUnit", "ویرایش واحد سازمانی")]
        ModifyUnit = 275,
        [ActionInfoAttribute("AddCalculation", "ایجاد محاسبه")]
        AddCalculation = 320,
        [ActionInfoAttribute("DeleteCalculation", "حذف محاسبه")]
        DeleteCalculation = 321,
        [ActionInfoAttribute("ModifyCalculation", "ویرایش محاسبه")]
        ModifyCalculation = 322,
        [ActionInfoAttribute("ShowCalculationState", "مشاهده وضعیت محاسبه")]
        ShowCalculationState = 323,
        [ActionInfoAttribute("SetDeterministicCalculation", "قطعی کردن محاسبه")]
        SetDeterministicCalculation = 324,
        [ActionInfoAttribute("ShowCalculationResult", "مشاهده نتایج محاسبه")]
        ShowCalculationResult = 325,
        [ActionInfoAttribute("RunCalculation", "اجرای محاسبه")]
        RunCalculation = 330,
        [ActionInfoAttribute("StopCalculation", "توقف محاسبه")]
        StopCalculation = 331,
        [ActionInfoAttribute("UnsetDeterministicCalculation", "غیر قعطی کردن محاسبه")]
        UnsetDeterministicCalculation = 332,
        [ActionInfoAttribute("ShowAllCalculationException", "مشاهده فهرست خطاهای محاسبه")]
        ShowAllCalculationException = 333,
        [ActionInfoAttribute("ShowCalculationException", "مشاهده خطای محاسبه")]
        ShowCalculationException = 334,
        [ActionInfoAttribute("FillInquiryForm", "پر کردن فرم نظر سنجی")]
        FillInquiryForm = 340,
        [ActionInfoAttribute("DeleteCustomInquirer", "حذف فرد از لیست نظر دهنده ها")]
        DeleteCustomInquirer = 341,
        [ActionInfoAttribute("FillInquiryUnitForm", "پر کردن فرم نظر سنجی")]
        FillInquiryUnitForm = 342,
        [ActionInfoAttribute("AddClaim", "درخواست اعتراض به نمره ارزیابی")]
        AddClaim = 350,
        [ActionInfoAttribute("ShowClaim", "نمایش درخواست اعتراض ")]
        ShowClaim = 351,
        [ActionInfoAttribute("ReplyToClaim", "پاسخ به درخواست اعتراض")]
        ReplyToClaim = 352,
        [ActionInfoAttribute("DeleteClaim", "حذف درخواست اعتراض")]
        DeleteClaim = 353,
        [ActionInfoAttribute("CancelClaim", "انصراف از درخواست اعتراض")]
        CancelClaim = 354,
        [ActionInfoAttribute("ShowAdminClaimList", "نمایش درخواست های ثبت شده جهت مدیریت")]
        ShowAdminClaimList = 355,
        // ShowEmployeeClaimList

        [ActionInfoAttribute("AddPermittedUserToMyTasks", "اضافه کردن کاربر به  کارتابل از طرف")]
        AddPermittedUserToMyTasks = 360,
        [ActionInfoAttribute("RemovePermittedUserFromMyTasks", "حذف  کاربر به  کارتابل از طرف")]
        RemovePermittedUserFromMyTasks = 361,
        [ActionInfoAttribute("SettingPermittedUserToMyTasks", "تنظیم دسترسی های کاربر در کارتابل از طرف")]
        SettingPermittedUserToMyTasks = 362,
        [ActionInfoAttribute("AddUser", "ایحاد کاربر")]
        AddUser = 1511,
        [ActionInfoAttribute("ModifyUser", "ویرایش کاربر")]
        ModifyUser = 1512,
        [ActionInfoAttribute("DeleteUser", "حذف کاربر")]
        DeleteUser = 1513,
        [ActionInfoAttribute("ManageUserCustomActions", "تعیین دسترسی های کاربر")]
        ManageUserCustomActions = 1514,
        [ActionInfoAttribute("ManageUserWorkListUsers", "تنظیم دسترسی کارتابل از طرف کاربر")]
        ManageUserWorkListUsers = 1515,
        [ActionInfoAttribute("ShowUser", "نمایش کاربر")]
        ShowUser = 1516,
        [ActionInfoAttribute("AddUserGroup", "ایجاد گروه کاربری")]
        AddUserGroup = 1611,
        [ActionInfoAttribute("ModifyUserGroup", "ویرایش گروه کاربری")]
        ModifyUserGroup = 1612,
        [ActionInfoAttribute("DeleteUserGroup", "حذف گروه کاربری")]
        DeleteUserGroup = 1613,
        [ActionInfoAttribute("ManageGroupCustomActions", "تعیین دسترسی های گروه کاربری")]
        ManageGroupCustomActions = 1614,
        [ActionInfoAttribute("ShowUserGroup", "نمایش گروه کاربری")]
        ShowUserGroup = 1615,
        [ActionInfoAttribute("ShowLog", "نمایش لاگ")]
        ShowLog = 1711,
        [ActionInfoAttribute("DeleteLog", "حذف لاگ")]
        DeleteLog = 1712,
        [ActionInfoAttribute("AddUnitIndexInPeriod", "تخصیص شاخص در دوره")]
        AddUnitIndexInPeriod = 1811,
        [ActionInfoAttribute("ModifyUnitIndexInPeriod", "ویرایش شاخص در دوره")]
        ModifyUnitIndexInPeriod = 1812,
        [ActionInfoAttribute("DeleteUnitIndexInPeriod", "حذف شاخص از دوره")]
        DeleteUnitIndexInPeriod = 1813,
        [ActionInfoAttribute("AddUnitIndexGroupInPeriod", "ایجاد گروه شاخص در دوره")]
        AddUnitIndexGroupInPeriod = 1814,
        [ActionInfoAttribute("ModifyUnitIndexGroupInPeriod", "ویرایش گروه شاخص در دروه")]
        ModifyUnitIndexGroupInPeriod = 1815,
        [ActionInfoAttribute("DeleteUnitIndexGroupInPeriod", "حذف گروه شاخص از دوره")]
        DeleteUnitIndexGroupInPeriod = 1816

    }
}
