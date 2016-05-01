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

    public enum ActionType
    {

        [ActionInfoAttribute("ShowUnitIndexInPeriod", "نمایش شاخص")]
        ShowUnitIndexInPeriod = 10001,

        [ActionInfoAttribute("ShowPeriod", "نمایش دوره")]
        ShowPeriod = 10004,

        [ActionInfoAttribute("ShowJobInPeriod", "نمایش شغل در دوره")]
        ShowJobInPeriod = 10005,

        

        [ActionInfoAttribute("FillInquiryForm", "پر کردن فرم نظر سنجی")]
        FillInquiryForm = 10009,

        [ActionInfoAttribute("DeleteCustomInquirer", "حذف فرد از لیست نظر دهنده ها")]
        DeleteCustomInquirer = 10010,

        [ActionInfoAttribute("FillInquiryUnitForm", "پر کردن فرم نظر سنجی")]
        FillInquiryUnitForm = 10011,

        #region PMSAdmin  1 

        #region CustomFieldType 1 

        [ActionInfoAttribute("ManageCustomFields", "مدیریت فیلد های دلخواه")]
        ManageCustomFields = 111,

        [ActionInfoAttribute("CreateCustomField", "ایجاد فیلد دلخواه")]
        CreateCustomField = 112,

        [ActionInfoAttribute("DeleteCustomField", "حذف فیلد دلخواه")]
        DeleteCustomField = 113,

        [ActionInfoAttribute("ModifyCustomField", "ویرایش فیلد دلخواه")]
        ModifyCustomField = 114,

        
        #endregion

        #region JobIndex 2 

        [ActionInfoAttribute("ManageJobIndices", "مدیریت شاخص های شغل")]
        ManageJobIndices = 121,

        [ActionInfoAttribute("AddJobIndex", "یجاد شاخص شغل")]
        AddJobIndex = 122,

        [ActionInfoAttribute("ModifyJobIndex", "ویرایش شاخص شغل")]
        ModifyJobIndex = 123,

        [ActionInfoAttribute("DeleteJobIndex", "حذف شاخص شغل")]
        DeleteJobIndex = 124,

        [ActionInfoAttribute("ManageJobIndexCustomFields", "مدیریت فیلدهای دلخواه شاخص")]
        ManageJobIndexCustomFields = 125,

        [ActionInfoAttribute("AddJobIndexCustomFields", "تخصیص فیلد دلخواه برای شاخص")]
        AddJobIndexCustomFields = 126,

        [ActionInfoAttribute("AddJobIndexCategory", "ایجاد دسته شاخص")]
        AddJobIndexCategory = 127,

        [ActionInfoAttribute("DeleteJobIndexCategory", "حذف دسته شاخص")]
        DeleteJobIndexCategory = 128,

        [ActionInfoAttribute("ModifyJobIndexCategory", "ویرایش دسته شاخص")]
        ModifyJobIndexCategory = 129,


        #endregion

        #region Job 3 
 
        [ActionInfoAttribute("ManageJobs", "مدیریت شغل در مخزن")]
        ManageJobs = 131,

        [ActionInfoAttribute("AddJob", "ایجاد شغل")]
        CreateJob = 132,

        [ActionInfoAttribute("ModifyJob", "ویرایش شغل")]
        ModifyJob = 133,

        [ActionInfoAttribute("DeleteJob", "حذف شغل")]
        DeleteJob = 134,

        [ActionInfoAttribute("ManageJobCustomFields", "مدیریت فیلدهای دلخواه شغل")]
        ManageJobCustomFields = 135000,

        [ActionInfoAttribute("AssignJobCustomFields", "تخصیص فیلدهای دلخواه شغل")]
        AssignJobCustomFields = 136000,



        #endregion

        #region UnitIndex 4 

        [ActionInfoAttribute("ManageUnitIndices", "مدیریت شاخص های واحد")]
        ManageUnitIndices = 141,

        [ActionInfoAttribute("AddUnitIndex", "ایجاد شاخص")]
        AddUnitIndex = 142,

        [ActionInfoAttribute("ModifyUnitIndex", "ویرایش شاخص")]
        ModifyUnitIndex = 143,

        [ActionInfoAttribute("DeleteUnitIndex", "حذف شاخص")]
        DeleteUnitIndex = 144,

        [ActionInfoAttribute("ManageUnitIndexCustomFields", "مدیریت فیلدهای دلخواه شاخص")]
        ManageUnitIndexCustomFields = 145,

        [ActionInfoAttribute("AddUnitIndexCustomFields", "ایجاد فیلد دلخواه برای شاخص")]
        AddUnitIndexCustomFields = 146,

        [ActionInfoAttribute("AddUnitIndexCategory", "ایجاد دسته شاخص")]
        AddUnitIndexCategory = 147,

        [ActionInfoAttribute("DeleteUnitIndexCategory", "حذف دسته شاخص")]
        DeleteUnitIndexCategory = 148,

        [ActionInfoAttribute("ModifyUnitIndexCategory", "ویرایش دسته شاخص")]
        ModifyUnitIndexCategory = 149,

        #endregion

        #region Unit 5 

        [ActionInfoAttribute("ManageUnits", "نمایش واحد سازمانی")]
        ManageUnits = 151,

        [ActionInfoAttribute("AddUnit", "ایجاد واحد سازمانی")]
        AddUnit = 152,

        [ActionInfoAttribute("ModifyUnit", "ویرایش واحد سازمانی")]
        ModifyUnit = 153,

        [ActionInfoAttribute("DeleteUnit", "حذف واحد سازمانی")]
        DeleteUnit = 154,

        [ActionInfoAttribute("ManageUnitCustomFields", "مدیریت فیلدهای دلخواه واحد")]
        ManageUnitCustomFields = 155,

        [ActionInfoAttribute("AddUnitCustomFields", "ایجاد فیلد دلخواه برای واحد")]
        AddUnitCustomFields = 156,


        #endregion

        #region JobPosition 6 

        [ActionInfoAttribute("ShowJobPosition", "نمایش پست سازمانی")]
        ManageJobPositions = 161,

        [ActionInfoAttribute("AddJobPosition", "ایجاد پست سازمانی")]
        AddJobPosition = 162,

        [ActionInfoAttribute("ModifyJobPosition", "ویرایش پست سازمانی")]
        ModifyJobPosition = 163,

        [ActionInfoAttribute("DeleteJobPosition", "حذف پست سازمانی")]
        DeleteJobPosition = 164,
   
        #endregion

        #region Policy 7 

        [ActionInfoAttribute("ManagePolicies", "مدیریت نظام محاسبه عملکرد")]
        ManagePolicies = 171,

        [ActionInfoAttribute("AddPolicy", "ایجاد نظام محاسبه عملکرد")]
        AddPolicy = 172,

        [ActionInfoAttribute("ModifyPolicy", "ویرایش نظام محاسبه عملکرد")]
        ModifyPolicy = 173,

        [ActionInfoAttribute("DeletePolicy", "حذف نظام محاسبه عملکرد")]
        DeletePolicy = 174,

        #endregion

        #region Function 8 

        [ActionInfoAttribute("ManageFunctions", "مدیریت توابع")]
        ManageFunctions = 181,

        [ActionInfoAttribute("CreateFunction", "ایجاد تابع")]
        CreateFunction = 182,

        [ActionInfoAttribute("ModifyFunction", "ویرایش تابع")]
        ModifyFunction = 183,

        [ActionInfoAttribute("DeleteFunction", "حذف تابع")]
        DeleteFunction = 184,

        #endregion

        #region Rule 9 

        [ActionInfoAttribute("ManageRules", "مدیریت قوانین")]
        ManageRules = 191,

        [ActionInfoAttribute("AddRule", "ایجاد قانون")]
        AddRule = 192,

        [ActionInfoAttribute("ModifyRule", "ویرایش قانون")]
        ModifyRule = 193,

        [ActionInfoAttribute("DeleteRule", "حذف قانون")]
        DeleteRule = 194,

        [ActionInfoAttribute("ShowRuleTrail", "نمایش سابقه قانون")]
        ShowRuleTrail = 195,

        [ActionInfoAttribute("ShowAllRuleTrails", "نمایش سوابق تغییرات قانون")]
        ShowAllRuleTrails = 196,
        #endregion

        #region Users 10 

        [ActionInfoAttribute("AddPermittedUserToMyTasks", "اضافه کردن کاربر به  کارتابل از طرف")]
        AddPermittedUserToMyTasks = 1101,
        [ActionInfoAttribute("RemovePermittedUserFromMyTasks", "حذف  کاربر به  کارتابل از طرف")]
        RemovePermittedUserFromMyTasks = 1102,
        [ActionInfoAttribute("SettingPermittedUserToMyTasks", "تنظیم دسترسی های کاربر در کارتابل از طرف")]
        SettingPermittedUserToMyTasks = 1103,
        [ActionInfoAttribute("AddUser", "ایحاد کاربر")]
        AddUser = 1104,
        [ActionInfoAttribute("ModifyUser", "ویرایش کاربر")]
        ModifyUser = 1105,
        [ActionInfoAttribute("DeleteUser", "حذف کاربر")]
        DeleteUser = 1106,
        [ActionInfoAttribute("ManageUserCustomActions", "تعیین دسترسی های کاربر")]
        ManageUserCustomActions = 1107,
        [ActionInfoAttribute("ManageUserWorkListUsers", "تنظیم دسترسی کارتابل از طرف کاربر")]
        ManageUserWorkListUsers = 1108,
        [ActionInfoAttribute("ShowUser", "نمایش کاربر")]
        ShowUser = 1109,
        [ActionInfoAttribute("AddUserGroup", "ایجاد گروه کاربری")]
        AddUserGroup = 11010,
        [ActionInfoAttribute("ModifyUserGroup", "ویرایش گروه کاربری")]
        ModifyUserGroup = 11011,
        [ActionInfoAttribute("DeleteUserGroup", "حذف گروه کاربری")]
        DeleteUserGroup = 11012,
        [ActionInfoAttribute("ManageGroupCustomActions", "تعیین دسترسی های گروه کاربری")]
        ManageGroupCustomActions = 11013,
        [ActionInfoAttribute("ShowUserGroup", "نمایش گروه کاربری")]
        ShowUserGroup = 11014,

        #endregion

        #region Log 11

        [ActionInfoAttribute("ShowLog", "نمایش لاگ")]
        ShowLog = 1111,
        [ActionInfoAttribute("DeleteLog", "حذف لاگ")]
        DeleteLog = 1112,

        #endregion

        #endregion

        #region PMS 2 

        #region Period 1 

        [ActionInfoAttribute("AddPeriod", "ایجاد دوره")]
        AddPeriod = 211,
        [ActionInfoAttribute("ModifyPeriod", "ویرایش دوره")]
        ModifyPeriod = 212,
        [ActionInfoAttribute("DeletePeriod", "حذف دوره")]
        DeletePeriod = 213,
        [ActionInfoAttribute("ManageUnitInPeriod", "مدیریت واحدهای سازمانی دوره")]
        ManageUnitInPeriod = 214,
        [ActionInfoAttribute("ManageUnitIndexInPeriod", "مدیریت شاخص های سازمانی دوره ")]
        ManageUnitIndexInPeriod = 215,
        [ActionInfoAttribute("ManageJobIndexInPeriod", "مدیریت شاخص های دوره ")]
        ManageJobIndexInPeriod = 216,
        [ActionInfoAttribute("ManageJobInPeriod", "مدیریت مشاغل دوره")]
        ManageJobInPeriod = 217,
        [ActionInfoAttribute("ManageJobPositions", "مدیریت پست های سازمانی دوره")]
        ManageJobPositionInPeriod = 218,


        [ActionInfoAttribute("ActivatePeriod", "فعال سازی دوره")]
        ActivatePeriod = 2111,
        [ActionInfoAttribute("InitializePeriodForInquiry", "آماده سازی دوره برای نظرسنجی")]
        InitializePeriodForInquiry = 2112,

        [ActionInfoAttribute("GetPeriodInitializingInquiryStatus", " نمایش وضعیت آماده سازی اجرای نظرسنجی")]
        GetPeriodInitializingInquiryStatus = 2119,

        [ActionInfoAttribute("StartInquiry", "شروع نظرسنجی ")]
        StartInquiry = 2113,
        [ActionInfoAttribute("CompelteInquiry", "اتمام نظرسنجی ")]
        CompleteInquiry = 2114,
        //[ActionInfoAttribute("StartCliaming", "شروع زمان ثبت اعتراض ")]
        //StartCliaming = 2115,
        //[ActionInfoAttribute("FinishCliaming", "پايان زمان ثبت اعتراض ")]
        //FinishCliaming = 2116,
        [ActionInfoAttribute("ClosePeriod", "بستن دوره ")]
        ClosePeriod = 2117,
        //[ActionInfoAttribute("CopyPeriodBasicData", "کپی اطلاعات از دوره های قبل ")]
        //CopyPeriodBasicData = 2118,

        [ActionInfoAttribute("RollBackPeriodState", " برگشت دوره به وضعیت قبل ")]
        RollBackPeriodState = 2120,
        
        #endregion 

        #region JobIndexInPeriod 2 

        [ActionInfoAttribute("AddJobIndexInPeriod", "تخصیص شاخص در دوره")]
        AddJobIndexInPeriod = 221,
        [ActionInfoAttribute("ModifyJobIndexInPeriod", "ویرایش شاخص در دوره")]
        ModifyJobIndexInPeriod = 222,
        [ActionInfoAttribute("DeleteJobIndexInPeriod", "حذف شاخص از دوره")]
        DeleteJobIndexInPeriod = 223,
        [ActionInfoAttribute("AddJobIndexGroupInPeriod", "ایجاد گروه شاخص در دوره")]
        AddJobIndexGroupInPeriod = 224,
        [ActionInfoAttribute("ModifyJobIndexGroupInPeriod", "ویرایش گروه شاخص در دروه")]
        ModifyJobIndexGroupInPeriod = 225,
        [ActionInfoAttribute("DeleteJobIndexGroupInPeriod", "حذف گروه شاخص از دوره")]
        DeleteJobIndexGroupInPeriod = 226,
        [ActionInfoAttribute("ShowJobIndexInPeriod", "نمایش شاخص در دوره")]
        ShowJobIndexInPeriod = 227,

        #endregion

        #region JobInPeriod 3 

        [ActionInfoAttribute("AddJobInPeriod", "تخصیص شغل در دوره")]
        AddJobInPeriod = 231,
        [ActionInfoAttribute("ModifyJobInPeriod", "ویرایش شغل در دوره")]
        ModifyJobInPeriod = 232,
        [ActionInfoAttribute("DeleteJobInPeriod", "حذف شغل در دوره")]
        DeleteJobInPeriod = 234,
        
        #endregion

        #region UnitIndexInPeriod 4 

        [ActionInfoAttribute("AddUnitIndexInPeriod", "تخصیص شاخص در دوره")]
        AddUnitIndexInPeriod = 241,
        [ActionInfoAttribute("ModifyUnitIndexInPeriod", "ویرایش شاخص در دوره")]
        ModifyUnitIndexInPeriod = 242,
        [ActionInfoAttribute("DeleteUnitIndexInPeriod", "حذف شاخص از دوره")]
        DeleteUnitIndexInPeriod = 243,
        [ActionInfoAttribute("AddUnitIndexGroupInPeriod", "ایجاد گروه شاخص در دوره")]
        AddUnitIndexGroupInPeriod = 244,
        [ActionInfoAttribute("ModifyUnitIndexGroupInPeriod", "ویرایش گروه شاخص در دروه")]
        ModifyUnitIndexGroupInPeriod = 245,
        [ActionInfoAttribute("DeleteUnitIndexGroupInPeriod", "حذف گروه شاخص از دوره")]
        DeleteUnitIndexGroupInPeriod = 246,

        #endregion

        #region UnitInPeriod 5 

        [ActionInfoAttribute("AddUnitInPeriod", "تخصیص واحد در دوره")]
        AddUnitInPeriod = 251,
        [ActionInfoAttribute("ModifyUnitInPeriod", "ویرایش واحد در دوره")]
        ModifyUnitInPeriod = 252,
        [ActionInfoAttribute("DeleteUnitInPeriod", "حذف واحد از دوره")]
        DeleteUnitInPeriod = 253,
        [ActionInfoAttribute("ShowUnitInPeriod", "نمایش واحد سازمانی در دوره")]
        ShowUnitInPeriod = 254,
        [ActionInfoAttribute("ManageUnitInPeriodInquiry", "تعیین افراد نظر دهنده واحد سازمانی")]
        ManageUnitInPeriodInquiry = 255,
        [ActionInfoAttribute("ShowUnitInPeriodInquiry", "نمایش افراد نظر دهنده واحد سازمانی")]
        ShowUnitInPeriodInquiry = 256,
        
        #endregion

        #region JobPositionInPeriod 6 

        [ActionInfoAttribute("AddJobPositionInPeriod", "تخصیص پست در دوره")]
        AddJobPositionInPeriod = 261,
        [ActionInfoAttribute("DeleteJobPositionInPeriod", "حذف پست از دوره")]
        DeleteJobPositionInPeriod = 262,
        [ActionInfoAttribute("ManageJobPositionInPeriodInquiry", "تعیین افراد نظر دهنده پست سازمانی")]
        ManageJobPositionInPeriodInquiry = 263,
        [ActionInfoAttribute("ShowJobPositionInPeriod", "نمایش پست در دوره")]
        ShowJobPositionInPeriod = 264,
        #endregion

        #region Employee 7 

        [ActionInfoAttribute("ManageEmployees", "مدیریت کارمندان دوره")]
        ManageEmployees = 271,

        [ActionInfoAttribute("AddEmployee", "ایجاد کارمند")]
        AddEmployee = 272,

        [ActionInfoAttribute("ModifyEmployee", "ویزایش کارمند")]
        ModifyEmployee = 273,

        [ActionInfoAttribute("DeleteEmployee", "حذف کارمند")]
        DeleteEmployee = 274,

        [ActionInfoAttribute("AddEmployeeJobCustomFields", "ایجاد فیلدهای دلخواه کارمند")]
        AddEmployeeJobCustomFields = 275,

        [ActionInfoAttribute("ModifyEmployeeJobCustomFields", "ویرایش فیلدهای دلخواه کارمند")]
        ModifyEmployeeJobCustomFields = 276,


        [ActionInfoAttribute("ManageEmployeeJobPositions", "مدیریت پست های سازمانی کارمند")]
        ManageEmployeeJobPositions = 277,
        // GetEmployeeJobPositions , [ActionInfoAttribute("166", "GetEmployeeJobPositions","")]
        [ActionInfoAttribute("ShowEmployeeInquiery", "نمایش نظر سنجی کارمند")]
        ShowEmployeeInquiry = 278,

        #endregion

        #region Calculation 8

        [ActionInfoAttribute("ManageCalculations", "مدیریت محاسبات دوره")]
        ManageCalculations = 281,

        [ActionInfoAttribute("AddCalculation", "ایجاد محاسبه")]
        AddCalculation = 282,

        [ActionInfoAttribute("ModifyCalculation", "ویرایش محاسبه")]
        ModifyCalculation = 283,

        [ActionInfoAttribute("DeleteCalculation", "حذف محاسبه")]
        DeleteCalculation = 284,

        [ActionInfoAttribute("RunCalculation", "اجرای محاسبه")]
        RunCalculation = 285,

        [ActionInfoAttribute("StopCalculation", "توقف محاسبه")]
        StopCalculation = 286,

        [ActionInfoAttribute("SetDeterministicCalculation", "قطعی کردن محاسبه")]
        SetDeterministicCalculation = 287,

        [ActionInfoAttribute("UnsetDeterministicCalculation", "غیر قعطی کردن محاسبه")]
        UnsetDeterministicCalculation = 288,

        [ActionInfoAttribute("ShowCalculationState", "مشاهده وضعیت محاسبه")]
        ShowCalculationState = 289,

        [ActionInfoAttribute("ShowCalculationResult", "مشاهده نتایج محاسبه")]
        ShowCalculationResult = 2810,

        [ActionInfoAttribute("ShowCalculationException", "مشاهده خطای محاسبه")]
        ShowCalculationException = 2811,

        [ActionInfoAttribute("ShowAllCalculationException", "مشاهده فهرست خطاهای محاسبه")]
        ShowAllCalculationException = 2812,
        
        #endregion

        #region Claim 9 

        [ActionInfoAttribute("AddClaim", "درخواست اعتراض به نمره ارزیابی")]
        AddClaim = 291,
        [ActionInfoAttribute("ShowClaim", "نمایش درخواست اعتراض ")]
        ShowClaim = 292,
        [ActionInfoAttribute("ReplyToClaim", "پاسخ به درخواست اعتراض")]
        ReplyToClaim = 293,
        [ActionInfoAttribute("DeleteClaim", "حذف درخواست اعتراض")]
        DeleteClaim = 294,
        [ActionInfoAttribute("CancelClaim", "انصراف از درخواست اعتراض")]
        CancelClaim = 295,
        [ActionInfoAttribute("ShowAdminClaimList", "نمایش درخواست های ثبت شده جهت مدیریت")]
        ShowAdminClaimList = 296,

        #endregion

        #endregion

    }

    #region ActionType Facility and attribute

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
        public static List<ActionType> SelectActionTypes(Dictionary<int, bool>.KeyCollection codes)
        {
            var result = new List<ActionType>();
            foreach (var code in codes)
            {
                result.Add((ActionType)code);
            }
            return result;
        }
    }

    #endregion
}
