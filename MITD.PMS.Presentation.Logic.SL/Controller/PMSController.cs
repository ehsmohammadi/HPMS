using System;
using System.Collections.Generic;
using System.Windows;
using MITD.PMS.Presentation.Contracts;
using MITD.Core;
using MITD.Presentation;
using System.Windows.Browser;
using System.Threading;
using Newtonsoft.Json.Linq;
using System.IO;
using MITD.Core.Exceptions;

namespace MITD.PMS.Presentation.Logic
{
    [ScriptableType]
    public partial class PMSController : ApplicationController, IPMSController
    {
        private readonly ICustomFieldServiceWrapper customFieldService;
        private IUserServiceWrapper userService;
        private IUserProvider userProvider;
        private IPeriodServiceWrapper periodService;
        private readonly ILogServiceWrapper logService;
        private IReportServiceWrapper repService;
        private readonly IMainAppLocalizedResources localizedResources;

        #region Properties

        public List<PMSAction> PMSActions
        {
            get;
            set;
        }

        public PeriodDTO CurrentPriod
        {
            get;
            set;
        }

        public CalculationStateWithRunSummaryDTO LastFinalCalculation
        {
            get;
            set;
        }

        public UserStateDTO LoggedInUserState { get; set; }

        public UserStateDTO CurrentUserState { get; set; }

        public List<CustomFieldEntity> CustomFieldEntityList { get; set; }

        #endregion


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

        #region Methods

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

        public void GetCurrentPeriod()
        {
            periodService.GetCurrentPeriod
                (
                    (res, exp) => BeginInvokeOnDispatcher(() =>
                    {
                        HideBusyIndicator();
                        if (exp == null)
                        {
                            CurrentPriod = res;
                            Publish(new MainWindowUpdateArgs());
                        }
                        else
                        {
                            HandleException(exp);
                        }
                    }
                ));
            LastFinalCalculation = periodService.LastFinalCalculation();
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

        public void Logout()
        {
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

        public void ChangeCurrentWorkListUser(string currentWorkListUserName)
        {
            getSessionToken(userProvider.SamlToken, () => { }, currentWorkListUserName);
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
                    getLogonUser();
                    action();
                }
                else
                {
                    HideBusyIndicator();
                    HandleException(exp);
                }
            }), token, newCurrentWorkListUser);
        }

        public void getLogonUser()
        {
            userService.GetLogonUser((res, exp) =>
                {
                    if (exp == null)
                    {
                        BeginInvokeOnDispatcher(() =>
                       {
                           CurrentUserState = res;
                           LoggedInUserState = res;
                           Publish(new MainWindowUpdateArgs());

                       });
                        GetCurrentPeriod();
                        createCustomFieldEntityList();
                    }
                    else
                    {
                        HideBusyIndicator();
                        HandleException(exp);
                    }
                });
        }

        public void ShowLoginView()
        {
            //#if DEBUG 
            //            userService.GetToken((res, exp) => BeginInvokeOnDispatcher(() =>
            //            {
            //                if (exp == null)
            //                {
            //                    userProvider.Token = res;
            //                    createCustomFieldEntityList();
            //                    getCurrentPeriod();
            //                    getLogonUser();
            //                }
            //                else
            //                {
            //                    Publish(new HideBusyIndicatorArgs());
            //                    HandleException(exp);
            //                }

            //            }), "ehsan", "123456");

            //#else
            var view = ServiceLocator.Current.GetInstance<ILoginView>();
            viewManager.ShowInDialog(view, false);
            //#endif
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

            if (CurrentUserState != null && !string.IsNullOrEmpty(CurrentUserState.Username))
                log.UserName = CurrentUserState.Username;

            if (eventArgs is ApplicationUnhandledExceptionEventArgs)
            {
                log.Title = (eventArgs as ApplicationUnhandledExceptionEventArgs).ExceptionObject.Message;
                log.Messages = (eventArgs as ApplicationUnhandledExceptionEventArgs).ExceptionObject.StackTrace;
            }

            logService.AddLog((res, exp) =>
                {
                }, log);

        }

        public void ShowEmployeeListView(PeriodDTOWithAction period,bool inNewTab=false)
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

        #endregion


        public void Login(string userName, string password, Action action)
        {
            throw new NotImplementedException();
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
            var url = new Uri("/Reporting/Report.aspx?ReportPath=" + Path.Combine(parentElement.Path, parentElement.Name).TrimStart(new[] { '/' }), UriKind.Relative);
            //var options = new HtmlPopupWindowOptions { Left = 0, Top = 0, Width = 800, Height = 600 };
            //HtmlPage.PopupWindow(url , "new", options);
            HtmlPage.Window.Navigate(url, "_blank");
        }


    }
}
