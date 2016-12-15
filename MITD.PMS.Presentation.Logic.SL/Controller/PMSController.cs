using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Browser;
using MITD.Core;
using MITD.Core.Exceptions;
using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;
using Newtonsoft.Json.Linq;

namespace MITD.PMS.Presentation.Logic
{
    [ScriptableType]
    public class PMSController : ApplicationController, IPMSController
    {
        #region Fields

        private readonly ICustomFieldServiceWrapper customFieldService;
        private readonly IUserServiceWrapper userService;
        private readonly IUserProvider userProvider;
        private readonly IPeriodServiceWrapper periodService;
        private readonly ILogServiceWrapper logService;
        private readonly IReportServiceWrapper repService;
        private readonly IMainAppLocalizedResources localizedResources; 

        #endregion

        #region Properties

        public List<PMSAction> PMSActions
        {
            get;
            set;
        }

        public UserStateDTO LoggedInUser { get; set; }

        public UserStateDTO CurrentUser { get; set; }

        public PeriodDTO CurrentPriod
        {
            get;
            set;
        }

        public List<CustomFieldEntity> CustomFieldEntityList { get; set; }

        #endregion

        #region Constructors
        public PMSController(IViewManager viewManager,
                             IEventPublisher eventPublisher,
                             IDeploymentManagement deploymentManagement,
                             IPeriodServiceWrapper periodService,
                             ICustomFieldServiceWrapper customFieldService,
                             IUserServiceWrapper userService,
                             IUserProvider userProvider,
                             ILogServiceWrapper logService,
    IReportServiceWrapper repService, IMainAppLocalizedResources localizedResources
    )
            : base(viewManager, eventPublisher, deploymentManagement)
        {
            HtmlPage.RegisterScriptableObject("PMSController", this);
            this.customFieldService = customFieldService;
            this.userService = userService;
            this.userProvider = userProvider;
            this.periodService = periodService;
            this.logService = logService;
            this.repService = repService;
            this.localizedResources = localizedResources;
            PMSActions = new List<PMSAction>();
            CustomFieldEntityList = new List<CustomFieldEntity>();
            createPMSActions();
        } 
        #endregion

        #region Security methods

        public void Login(Action action)
        {
            ShowBusyIndicator("در حال ورود به سامانه...");
            userService.GetToken((res, exp) => BeginInvokeOnDispatcher(() =>
            {
                if (exp == null)
                {
                    userProvider.SamlToken = res;
                    getSessionToken(res, action);
                }
                else
                {
                    HandleException(exp);
                }
            }));
        }

        private void getSessionToken(string token, Action action, string newCurrentWorkListUser = "")
        {
            userService.GetSessionToken((res, exp) => BeginInvokeOnDispatcher(() =>
            {
                if (exp == null)
                {
                    var json = JObject.Parse(res);
                    var sessionToken = json["access_token"].ToString();
                    var expiresIn = int.Parse(json["expires_in"].ToString());
                    var expiration = DateTime.UtcNow.AddSeconds(expiresIn);
                    userProvider.Token = sessionToken;
                    GetLogonUser();
                    action();
                }
                else
                {
                    HideBusyIndicator();
                    HandleException(exp);
                }
            }), token, newCurrentWorkListUser);
        }

        public void GetLogonUser()
        {
            userService.GetLogonUser((res, exp) =>
            {
                if (exp == null)
                {
                    BeginInvokeOnDispatcher(() =>
                    {
                        validateUser(res);
                        CurrentUser = res;
                        LoggedInUser = res;
                        GetCurrentPeriod();
                        createCustomFieldEntityList();
                        Publish(new MainWindowUpdateArgs());

                    });

                }
                else
                {
                    HideBusyIndicator();
                    HandleException(exp);
                }
            });
        }

        public void Logout()
        {
            userProvider.SamlToken = null;
            userProvider.Token = null;
            HtmlPage.Window.Navigate(new Uri("Security/LogOut", UriKind.Relative));
        }

        public static void ShowLoginPage()
        {
            HtmlPage.Document.GetElementById("divLogin").SetStyleAttribute("display", "block");
            var url = PMSClientConfig.BaseAddress + "/SilverlightLogin.aspx";
            var s = "<IFrame id='IframeLogin' style='visibility:visible;height:100%;width:100%;border:0px' src ='" + url + "'></IFrame>";
            var div = HtmlPage.Document.GetElementById("divLogin");
            div.SetProperty("innerHTML", s);
            div.SetStyleAttribute("display", "block");
        }

        [ScriptableMember]
        public void ForceLogin()
        {
            Login(() => { });
            viewManager.CloseAllTabs();
        }

        #endregion

        #region Private Methods

        private void validateUser(UserStateDTO user)
        {
            if (user.IsEmployee && (user.Email.Status == (int)EmailStatusEnum.NotEntered||user.Email.Status == (int)EmailStatusEnum.Unverified))
                ShowEmailInView(user);
        }

        

        private void createPMSActions()
        {
            foreach (ActionType actionType in Enum.GetValues(typeof(ActionType)))
            {
                PMSActions.Add(new PMSAction
                {
                    ActionCode = actionType,
                    ActionName = actionType.GetAttribute<ActionInfoAttribute>().Description

                });
            }
        }

        private void createCustomFieldEntityList()
        {
            customFieldService.GetCustomFieldEntityList((res, exp) => BeginInvokeOnDispatcher(() =>
            {
                if (exp == null)
                    CustomFieldEntityList = res;
                else
                    HandleException(exp);
            }));
        }

        #endregion

        #region Public Methods

        public void ShowEmployeeListView(PeriodDTOWithAction period, bool inNewTab = false)
        {
            ShowBusyIndicator("در حال بارگذاری ماجول...");
            GetRemoteInstance<IEmployeeController>((res, exp) =>
            {
                HideBusyIndicator();
                if (exp == null)
                {
                    if (res != null)
                        res.ShowEmployeeListView(period, inNewTab);
                }
                else
                    this.HandleException(exp);
            });
        }

        public void ShowEmailInView(UserStateDTO user, bool inNewTab=false)
        {
            ShowBusyIndicator("در حال بارگذاری ماجول...");
            GetRemoteInstance<IBasicInfoController>((res, exp) =>
            {
                HideBusyIndicator();
                if (exp == null)
                {
                    if (res != null)
                        res.ShowEmailInView(user, inNewTab);
                }
                else
                    this.HandleException(exp);
            });
        }




        public void GetCurrentPeriod()
        {
            periodService.GetCurrentPeriod
                (
                    (res, exp) => BeginInvokeOnDispatcher
                        (() =>
                        {
                            HideBusyIndicator();
                            if (exp == null)
                            {
                                CurrentPriod = res;
                                Publish(new MainWindowUpdateArgs());
                            }
                            else
                                HandleException(exp);
                        })
                );
        }

        public void ChangeCurrentWorkListUser(string currentWorkListUserName)
        {
            getSessionToken(userProvider.SamlToken, () => { }, currentWorkListUserName);
        }

        public void LogException(object sender, EventArgs eventArgs)
        {
            var log = new ExceptionLogDTO()
                {
                    Code = "UI_SilverLight_Exp",
                    ClassName = "",
                    LogLevel = LogLevel.Error.ToString(),
                    Id = new Guid(),
                    LogDate = DateTime.Now,
                    Title = eventArgs.GetType().ToString(),
                    MethodName = ""
                };

            if (CurrentUser != null && !string.IsNullOrEmpty(CurrentUser.Username))
                log.UserName = CurrentUser.Username;

            if (eventArgs is ApplicationUnhandledExceptionEventArgs)
            {
                log.Title = (eventArgs as ApplicationUnhandledExceptionEventArgs).ExceptionObject.Message;
                log.Messages = (eventArgs as ApplicationUnhandledExceptionEventArgs).ExceptionObject.StackTrace;
            }

            logService.AddLog((res, exp) =>
                {
                }, log);

        }

        public void HandleException(Exception exp)
        {
            if (exp is SecurityException)
            {
                if (userProvider.IsAuthorized)
                {
                    ShowLoginPage();
                    return;
                }
                if (userProvider.IsAuthenticated)
                {
                    BeginInvokeOnDispatcher(() => viewManager.ShowMessage("شما در این سیستم تعریف نشده اید."));
                    return;
                }
                return;
            }
            var exption = ExceptionAdapter.Convert(exp, localizedResources);
            BeginInvokeOnDispatcher(() => viewManager.ShowMessage(exption.Message));
        }

        public void GetRemoteInstance<T>(Action<T, Exception> action) where T : class
        {
            deploymentManagement.AddModule(typeof(T),
                res =>
                {
                    action(ServiceLocator.Current.GetInstance(typeof(T)) as T, null);
                },
                exp => { action(null, exp); });
        }

        public void GetReportsTree(Action<IList<ReportDTO>> action)
        {
            repService.GetAllReports((res, exp) =>
            {
                if (exp == null)
                    action(res);
                else
                    HandleException(exp);
            });
        }

        public void OpenReport(ReportDTO parentElement)
        {
            // RDL
            // var url = new Uri("/Reporting/Report.aspx?ReportPath=" + Path.Combine(parentElement.Path, parentElement.Name).TrimStart(new[] { '/' }), UriKind.Relative);

            // RDLC
            var url = new Uri("/Reporting/ReportPage.aspx?ReportPath=" + parentElement.Name, UriKind.Relative);

            //var options = new HtmlPopupWindowOptions { Left = 0, Top = 0, Width = 800, Height = 600 };
            //HtmlPage.PopupWindow(url , "new", options);
            HtmlPage.Window.Navigate(url, "_blank");
        }

        #endregion

    }
}
