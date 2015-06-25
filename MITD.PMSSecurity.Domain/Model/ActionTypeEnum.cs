using System;
using MITD.Core;
#if SILVERLIGHT 

namespace MITD.PMS.Presentation.Contracts
{
    public class ActionType : Enumeration
    {

#else
using MITD.Domain.Model;

namespace MITD.PMSSecurity.Domain
{
    public class ActionType : Enumeration, IValueObject<ActionType>
    {
#endif

        public static readonly ActionType AddPeriod = new ActionType("100", "AddPeriod","ایجاد دوره");
        public static readonly ActionType ModifyPeriod = new ActionType("101", "ModifyPeriod","ویرایش دوره");
        public static readonly ActionType DeletePeriod = new ActionType("102", "DeletePeriod","حذف دوره");
        public static readonly ActionType ManageUnits = new ActionType("103", "ManageUnits","مدیریت واحدهای سازمانی دوره");
        public static readonly ActionType ManageJobIndices = new ActionType("104", "ManageJobIndices", "مدیریت شاخص های دوره ");
        public static readonly ActionType ManageJobs = new ActionType("105", "ManageJobs", "مدیریت مشاغل دوره");
        public static readonly ActionType ManageJobPositions = new ActionType("106", "ManageJobPositions","مدیریت پست های سازمانی دوره");
        public static readonly ActionType ManageEmployees = new ActionType("107", "ManageEmployees", "مدیریت کارمندان دوره");
        public static readonly ActionType ManageCalculations = new ActionType("108", "ManageCalculations","مدیریت محاسبات دوره");
        
        public static readonly ActionType ActivatePeriod = new ActionType("109", "ActivatePeriod", "فعال سازی دوره");
        public static readonly ActionType InitializePeriodForInquiry = new ActionType("110", "InitializePeriodForInquiry", "آماده سازی دوره برای نظرسنجی");
        public static readonly ActionType StartInquiry = new ActionType("111", "StartInquiry", "شروع نظرسنجی ");
        public static readonly ActionType CompleteInquiry = new ActionType("112", "CompelteInquiry", "اتمام نظرسنجی ");
        public static readonly ActionType StartCliaming = new ActionType("113", "StartCliaming", "شروع زمان ثبت اعتراض ");
        public static readonly ActionType FinishCliaming = new ActionType("114", "FinishCliaming", "پايان زمان ثبت اعتراض ");
        public static readonly ActionType ClosePeriod = new ActionType("115", "ClosePeriod", "بستن دوره ");
        public static readonly ActionType CopyPeriodBasicData = new ActionType("116", "CopyPeriodBasicData", "کپی اطلاعات از دوره های قبل ");
        public static readonly ActionType GetPeriodInitializingInquiryStatus = new ActionType("117", "GetPeriodInitializingInquiryStatus", " نمایش وضعیت آماده سازی اجرای نظرسنجی");
        public static readonly ActionType RollBackPeriodState = new ActionType("118", "RollBackPeriodState", " برگشت دوره به وضعیت قبل ");


        public static readonly ActionType AddJobInPeriod = new ActionType("120", "AddJobInPeriod","تخصیص شغل در دوره");
        public static readonly ActionType ModifyJobInPeriod = new ActionType("121", "ModifyJobInPeriod","ویرایش شغل در دوره");
        public static readonly ActionType DeleteJobInPeriod = new ActionType("122", "DeleteJobInPeriod","حذف شغل در دوره");
        //public static readonly ActionType ManageJobInPeriodCustomFields = new ActionType("123", "ManageJobInPeriodCustomFields","مدیریت فیلدهای دلخواه شغل در دوره");

        public static readonly ActionType AddUnitInPeriod = new ActionType("130", "AddUnitInPeriod","تخصیص واحد در دوره");
        public static readonly ActionType DeleteUnitInPeriod = new ActionType("131", "DeleteUnitInPeriod","حذف واحد از دوره");

        public static readonly ActionType AddJobPositionInPeriod = new ActionType("140", "AddJobPositionInPeriod","تخصیص پست در دوره");
        public static readonly ActionType DeleteJobPositionInPeriod = new ActionType("141", "DeleteJobPositionInPeriod","حذف پست از دوره");
        public static readonly ActionType ManageJobPositionInPeriodInquiry = new ActionType("142", "ManageJobPositionInPeriodInquiry","تعیین افراد نظر دهنده پست سازمانی");

        public static readonly ActionType AddJobIndexInPeriod = new ActionType("150", "AddJobIndexInPeriod","تخصیص شاخص در دوره");
        public static readonly ActionType ModifyJobIndexInPeriod = new ActionType("151", "ModifyJobIndexInPeriod","ویرایش شاخص در دوره");
        public static readonly ActionType DeleteJobIndexInPeriod = new ActionType("152", "DeleteJobIndexInPeriod","حذف شاخص از دوره");
        public static readonly ActionType AddJobIndexGroupInPeriod = new ActionType("153", "AddJobIndexGroupInPeriod","ایجاد گروه شاخص در دوره");
        public static readonly ActionType ModifyJobIndexGroupInPeriod = new ActionType("154", "ModifyJobIndexGroupInPeriod","ویرایش گروه شاخص در دروه");
        public static readonly ActionType DeleteJobIndexGroupInPeriod = new ActionType("155", "DeleteJobIndexGroupInPeriod","حذف گروه شاخص از دوره");

        public static readonly ActionType AddEmployee = new ActionType("160", "AddEmployee","ایجاد کارمند");
        public static readonly ActionType ModifyEmployee = new ActionType("161", "ModifyEmployee","ویزایش کارمند");
        public static readonly ActionType DeleteEmployee = new ActionType("162", "DeleteEmployee","حذف کارمند");
        public static readonly ActionType ManageEmployeeJobPositions = new ActionType("163", "ManageEmployeeJobPositions","مدیریت پست های سازمانی کارمند");
        public static readonly ActionType AddEmployeeJobCustomFields = new ActionType("164", "AddEmployeeJobCustomFields","ایجاد فیلدهای دلخواه کارمند");
        public static readonly ActionType ModifyEmployeeJobCustomFields = new ActionType("165", "ModifyEmployeeJobCustomFields","ویرایش فیلدهای دلخواه کارمند");
        //public static readonly ActionType GetEmployeeJobPositions = new ActionType("166", "GetEmployeeJobPositions","");

        public static readonly ActionType AddJobIndex = new ActionType("210", "AddJobIndex","ایجاد شاخص");
        public static readonly ActionType ModifyJobIndex = new ActionType("211", "ModifyJobIndex","ویرایش شاخص");
        public static readonly ActionType DeleteJobIndex = new ActionType("212", "DeleteJobIndex","حذف شاخص");
        public static readonly ActionType ManageJobIndexCustomFields = new ActionType("213", "ManageJobIndexCustomFields","مدیریت فیلدهای دلخواه شاخص");
        public static readonly ActionType AddJobIndexCustomFields = new ActionType("214", "AddJobIndexCustomFields","ایجاد فیلد دلخواه برای شاخص");


        public static readonly ActionType AddJobIndexCategory = new ActionType("220", "AddJobIndexCategory","ایجاد دسته شاخص");
        public static readonly ActionType DeleteJobIndexCategory = new ActionType("221", "DeleteJobIndexCategory","حذف دسته شاخص");
        public static readonly ActionType ModifyJobIndexCategory = new ActionType("222", "ModifyJobIndexCategory", "ویرایش دسته شاخص");

        public static readonly ActionType AddJobPosition = new ActionType("230", "AddJobPosition","ایجاد پست سازمانی");
        public static readonly ActionType DeleteJobPosition = new ActionType("231", "DeleteJobPosition","حذف پست سازمانی");
        public static readonly ActionType ModifyJobPosition = new ActionType("232", "ModifyJobPosition","ویرایش پست سازمانی");

        public static readonly ActionType AddFunction = new ActionType("240", "AddFunction","ایجاد تابع");
        public static readonly ActionType DeleteFunction = new ActionType("241", "DeleteFunction","حذف تابع");
        public static readonly ActionType ModifyFunction = new ActionType("242", "ModifyFunction","ویرایش تابع");

        public static readonly ActionType AddCustomField = new ActionType("250", "AddCustomField","ایجاد فیلد دلخواه");
        public static readonly ActionType DeleteCustomField = new ActionType("251", "DeleteCustomField","حذف فیلد دلخواه");
        public static readonly ActionType ModifyCustomField = new ActionType("252", "ModifyCustomField","ویرایش فیلد دلخواه");

        public static readonly ActionType AddJob = new ActionType("260", "AddJob","ایجاد شغل");
        public static readonly ActionType DeleteJob = new ActionType("261", "DeleteJob","حذف شغل");
        public static readonly ActionType ModifyJob = new ActionType("262", "ModifyJob","ویرایش شغل");
        public static readonly ActionType ManageJobCustomFields = new ActionType("263", "ManageJobCustomFields","مدیریت فیلدهای دلخواه شغل");
        public static readonly ActionType AddJobCustomFields = new ActionType("264", "AddJobCustomFields","ایجاد فیلد دلخواه برای شغل");

        public static readonly ActionType AddUnit = new ActionType("270", "AddUnit","ایجاد واحد سازمانی");
        public static readonly ActionType DeleteUnit = new ActionType("271", "DeleteUnit","حذف واحد سازمانی");
        public static readonly ActionType ManageUnitCustomFields = new ActionType("273", "ManageUnitCustomFields", "مدیریت فیلدهای دلخواه واحد");
        public static readonly ActionType AddUnitCustomFields = new ActionType("274", "AddUnitCustomFields", "ایجاد فیلد دلخواه برای واحد");
        public static readonly ActionType ModifyUnit = new ActionType("272", "ModifyUnit", "ویرایش واحد سازمانی");



        public static readonly ActionType AddPolicy = new ActionType("280", "AddPolicy","ایجاد نظام محاسبه عملکرد");
        public static readonly ActionType DeletePolicy = new ActionType("281", "DeletePolicy", "حذف نظام محاسبه عملکرد");
        public static readonly ActionType ModifyPolicy = new ActionType("282", "ModifyPolicy", "ویرایش نظام محاسبه عملکرد");

        public static readonly ActionType ManageRules = new ActionType("290", "ManageRules","مدیریت قوانین");
        public static readonly ActionType ManageFunctions = new ActionType("291", "ManageFunctions","مدیریت توابع");

        public static readonly ActionType AddRule = new ActionType("310", "AddRule","ایجاد قانون");
        public static readonly ActionType DeleteRule = new ActionType("311", "DeleteRule","حذف قانون");
        public static readonly ActionType ModifyRule = new ActionType("312", "ModifyRule","ویرایش قانون");
        public static readonly ActionType ShowRuleTrail = new ActionType("313", "ShowRuleTrail", "نمایش سابقه قانون");
        public static readonly ActionType ShowAllRuleTrails = new ActionType("314", "ShowAllRuleTrails", "نمایش سوابق تغییرات قانون");

        public static readonly ActionType AddCalculation = new ActionType("320", "AddCalculation","ایجاد محاسبه");
        public static readonly ActionType DeleteCalculation = new ActionType("321", "DeleteCalculation","حذف محاسبه");
        public static readonly ActionType ModifyCalculation = new ActionType("322", "ModifyCalculation","ویرایش محاسبه");
        public static readonly ActionType ShowCalculationState = new ActionType("323", "ShowCalculationState","مشاهده وضعیت محاسبه");
        public static readonly ActionType SetDeterministicCalculation = new ActionType("324", "SetDeterministicCalculation", "قطعی کردن محاسبه");
        public static readonly ActionType ShowCalculationResult = new ActionType("325", "ShowCalculationResult","مشاهده نتایج محاسبه");
        public static readonly ActionType RunCalculation = new ActionType("330", "RunCalculation","اجرای محاسبه");
        public static readonly ActionType StopCalculation = new ActionType("331", "StopCalculation","توقف محاسبه");
        public static readonly ActionType UnsetDeterministicCalculation = new ActionType("332", "UnsetDeterministicCalculation", "غیر قعطی کردن محاسبه");
        public static readonly ActionType ShowAllCalculationException = new ActionType("333", "ShowAllCalculationException", "مشاهده فهرست خطاهای محاسبه");
        public static readonly ActionType ShowCalculationException = new ActionType("334", "ShowCalculationException", "مشاهده خطای محاسبه");

        public static readonly ActionType FillInquiryForm = new ActionType("340", "FillInquiryForm","پر کردن فرم نظر سنجی");
        public static readonly ActionType DeleteCustomInquirer = new ActionType("341", "DeleteCustomInquirer","حذف فرد از لیست نظر دهنده ها");

        public static readonly ActionType AddClaim = new ActionType("350", "AddClaim","درخواست اعتراض به نمره ارزیابی");
        public static readonly ActionType ShowClaim = new ActionType("351", "ShowClaim","نمایش درخواست اعتراض ");
        public static readonly ActionType ReplyToClaim = new ActionType("352", "ReplyToClaim","پاسخ به درخواست اعتراض");
        public static readonly ActionType DeleteClaim = new ActionType("353", "DeleteClaim","حذف درخواست اعتراض");
        public static readonly ActionType CancelClaim = new ActionType("354", "CancelClaim", "انصراف از درخواست اعتراض");
        public static readonly ActionType ShowAdminClaimList = new ActionType("355", "ShowAdminClaimList", "نمایش درخواست های ثبت شده جهت مدیریت");
        //public static readonly ActionType ShowEmployeeClaimList
       

        public static readonly ActionType AddPermittedUserToMyTasks = new ActionType("360", "AddPermittedUserToMyTasks","اضافه کردن کاربر به  کارتابل از طرف");
        public static readonly ActionType RemovePermittedUserFromMyTasks = new ActionType("361", "RemovePermittedUserFromMyTasks", "حذف  کاربر به  کارتابل از طرف");
        public static readonly ActionType SettingPermittedUserToMyTasks = new ActionType("362", "SettingPermittedUserToMyTasks","تنظیم دسترسی های کاربر در کارتابل از طرف");

        public static readonly ActionType AddUser = new ActionType("1511", "AddUser","ایحاد کاربر");
        public static readonly ActionType ModifyUser = new ActionType("1512", "ModifyUser","ویرایش کاربر");
        public static readonly ActionType DeleteUser = new ActionType("1513", "DeleteUser","حذف کاربر");
        public static readonly ActionType ManageUserCustomActions = new ActionType("1514", "ManageUserCustomActions","تعیین دسترسی های کاربر");
        public static readonly ActionType ManageUserWorkListUsers = new ActionType("1515", "ManageUserWorkListUsers", "تنظیم دسترسی کارتابل از طرف کاربر");
        

        public static readonly ActionType AddUserGroup = new ActionType("1611", "AddUserGroup","ایجاد گروه کاربری");
        public static readonly ActionType ModifyUserGroup = new ActionType("1612", "ModifyUserGroup","ویرایش گروه کاربری");
        public static readonly ActionType DeleteUserGroup = new ActionType("1613", "DeleteUserGroup","حذف گروه کاربری");
        public static readonly ActionType ManageGroupCustomActions = new ActionType("1614", "ManageGroupCustomActions","تعیین دسترسی های گروه کاربری");

        public static readonly ActionType ShowLog = new ActionType("1711", "ShowLog", "نمایش لاگ");
        public static readonly ActionType DeleteLog = new ActionType("1712", "DeleteLog", "حذف لاگ");


        private readonly string description;
        public virtual string Description
        {
            get { return description; }
        }



        public ActionType(string value)
            : base(value)
        {
        }

        public ActionType(string value, string displayName ,string description)
            : base(value, displayName)
        {
            this.description = description;
        }

        


        public bool SameValueAs(ActionType other)
        {
            return Equals(other);
        }

        public static explicit operator int(ActionType x)
        {
            if (x == null)
            {
                //throw new InvalidCastException();
                return -1;

            }
            
            return Convert.ToInt32(x.Value);

        }

        public static implicit operator ActionType(int val)
        {
            return Enumeration.FromValue<ActionType>(val.ToString());
        }

    }
}
