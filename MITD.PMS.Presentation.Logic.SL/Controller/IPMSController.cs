using System;
using System.Collections.Generic;
using System.Windows;
using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public interface IPMSController : IApplicationController
    {
        List<PMSAction> PMSActions { get; set; }
        PeriodDTO CurrentPriod { get; }
        CalculationStateWithRunSummaryDTO LastFinalCalculation { get; }
        UserStateDTO CurrentUserState { get;}
        UserStateDTO LoggedInUserState { get; }
        void HandleException(Exception exp);
        void GetRemoteInstance<T>(Action<T, Exception> action) where T : class;
        List<CustomFieldEntity> CustomFieldEntityList { get;  }
        void Login(string userName, string password, Action action);
        void Login(Action action);
        void Logout();
        void ShowLoginView();
        void ShowEmployeeListView(PeriodDTOWithAction period,bool inNewTab=false);
        void GetCurrentPeriod();
        void LogException(object sender, EventArgs eventArgs);
        void GetReportsTree(Action<IList<ReportDTO>> action);

        void OpenReport(ReportDTO parentElement);
        void ChangeCurrentWorkListUser(string currentWorkListUserName);
    }
}
