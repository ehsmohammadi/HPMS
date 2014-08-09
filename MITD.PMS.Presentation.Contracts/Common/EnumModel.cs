namespace MITD.PMS.Presentation.Contracts
{
    //public enum ActionType
    //{
    //    #region Period

    //    AddPeriod = 1,
    //    ModifyPeriod = 2,
    //    DeletePeriod = 3,
    //    ShowJobInPeriodMgnt = 35,
    //    ManageUnitInPeriod = 20,
    //    ManageJobPositionInPeriod = 23,
    //    ManageJobIndexInPeriod = 50,
    //    BeginCurrentPeriodInquiry=5000,
    //    CloseCurrentPeriodInquiry=7777,
    //    ManagePeriodCaculations=8888,

    //    #endregion

    //    #region JobInPeriod
    //    AddJobInPeriod = 36,
    //    ModifyJobInPeriod = 37,
    //    DeleteJobInPeriod = 38,

    //    AddJobInPrdField = 40,
    //    ModifyJobInPrdField = 41,
    //    DeleteJobInPrdField = 42,
    //    #endregion

    //    #region UnitInPeriod
    //    AddUnitInPeriod = 21,
    //    //ModifyUnitInPeriod = 22,
    //    DeleteUnitInPeriod = 22,
    //    #endregion

    //    #region JobPositionInPeriod
    //    AddJobPositionInPeriod = 24,
    //    //ModifyJobPositionInPeriod = 25,
    //    DeleteJobPositionInPeriod = 25,
    //    ManageJobPositionInPeriodInquiry=26,
    //    #endregion

    //    #region JobIndexInPeriod

    //    AddJobIndexInPeriod = 51,
    //    ModifyJobIndexInPeriod = 52,
    //    DeleteJobIndexInPeriod = 53,
    //    AddJobIndexGroupInPeriod=701,
    //    ModifyJobIndexGroupInPeriod=702,
    //    DeleteJobIndexGroupInPeriod=703,
        
    //    #endregion

       


    //    #region Employee
    //    AddEmployee = 80,
    //    ModifyEmployee = 81,
    //    DeleteEmployee = 82,

    //    AddEmployeeJobPositions = 91,
    //    ModifyEmployeeJobPositions = 92,
    //    ManageEmployeeJobPostion = 93,

    //    AddEmployeeJobCustomFields = 94,
    //    UpdateEmployeeJobCustomFields = 95,
    //    #endregion


    //    #region JobIndex
    //    AddJobIndex = 200,
    //    ModifyJobIndex = 201,
    //    DeleteJobIndex = 202,
    //    ManageJobIndexCustomFields = 203,
    //    ModifyJobIndexFields = 204,
    //    AddJobIndexFields = 205,
    //    ModifyJobIndexCategory = 101,
    //    AddJobIndexCategory = 102,
    //    DeleteJobIndexCategory = 103,
    //    AddRootJobIndexCategory=22222,
    //    #endregion

    //    #region JobPsition
    //    AddJobPosition = 300,
    //    ModifyJobPosition = 301,
    //    DeleteJobPosition = 302, 
    //    #endregion

    //    #region Function
    //    AddFunction = 11,
    //    ModifyFunction = 12,
    //    DeleteFunction = 13, 
    //    #endregion

    //    #region CustomField
    //    AddCustomField = 111,
    //    ModifyCustomField = 112,
    //    DeleteCustomField = 113, 
    //    #endregion

    //    #region Job
    //    AddJob = 121,
    //    ModifyJob = 122,
    //    DeleteJob = 123,
    //    AddJobFields = 130,
    //    ModifyJobFields = 131,
    //    ManageJobCustomFields = 132,
    //    #endregion

    //    #region Unit
    //    AddUnit = 401,
    //    ModifyUnit = 402,
    //    DeleteUnit = 403, 
    //    #endregion

    //    #region Policy
    //    AddPolicy = 501,
    //    ModifyPolicy = 502,
    //    DeletePolicy = 503,
    //    ManageRules = 504,
    //    ManageFunctions = 505, 
    //    #endregion

    //    #region Rule
    //    AddRule = 601,
    //    ModifyRule = 602,
    //    DeleteRule = 603,
    //    AddRuleVersion = 604,
    //    ModifyLastRuleVersion = 605,
    //    DeleteLastRuleVersion = 606,
    //    ViewRuleVersions = 607, 
    //    #endregion

    //    ManageInquiryForm=66666,

    //    #region  Calculation
    //    AddPeriodCalculationExec = 900,
    //    ModifyPeriodCalculationExec = 907,
    //    DeletePeriodCalculationExec = 901,
    //    ShowPeriodCalculationState = 906,
    //    FinalizePeriodCalculationExec = 902,
    //    ShowPeriodCalculationResult = 903,
    //    //ShowPeriodCalculation=904,
    //    ShowPeriodCalculationResultDetails=905,
    //    RunCalculation = 908,
    //    StopCalculation = 909,
        

    //    #endregion
        
    //    DeleteCustomInquirer = 1222,

    //    AddClaim = 1311,
    //    ShowClaim = 1312,
    //    ReplyToClaim = 1313,
    //    DeleteClaim = 1314,
        
    //    AddPermittedUserToMyTasks = 1411,
    //    RemovePermittedUserFromMyTasks = 1412,
    //    SettingPermittedUserToMyTasks = 1413,

    //    AddUser = 1511 ,
    //    ModifyUser = 1512,
    //    DeleteUser= 1513,
    //    ManageUserCustomActions = 1514,

        
    //    AddUserGroup = 1611,
    //    ModifyUserGroup = 1612,
    //    DeleteUserGroup = 1613,
    //    ManageGroupCustomActions = 1614
    //}

    public enum RuleExcuteTime
    {
        CalculationStarting=1,
        CalculationEnding=2,
        PerEmployee=3,
    }

    public enum CalculationStateEnum
    {
        Initial = 1,
        Running=2,
        Paused=3,
        Canceled=4,
        Completed=5,

    }

}
