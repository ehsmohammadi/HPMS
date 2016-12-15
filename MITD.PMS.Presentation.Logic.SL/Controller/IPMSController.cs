using System;
using System.Collections.Generic;
using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public interface IPMSController : IApplicationController
    {
        void GetLogonUser();
        List<PMSAction> PMSActions { get; set; }
        PeriodDTO CurrentPriod { get; }
        UserStateDTO CurrentUser { get;}
        UserStateDTO LoggedInUser { get; }
        void HandleException(Exception exp);
        void GetRemoteInstance<T>(Action<T, Exception> action) where T : class;
        List<CustomFieldEntity> CustomFieldEntityList { get;  }

        void Login(Action action);
        void Logout();
        void ShowEmployeeListView(PeriodDTOWithAction period,bool inNewTab=false);
        void GetCurrentPeriod();
        void LogException(object sender, EventArgs eventArgs);
        void GetReportsTree(Action<IList<ReportDTO>> action);

        void OpenReport(ReportDTO parentElement);
        void ChangeCurrentWorkListUser(string currentWorkListUserName);
    }
}
