using System;
using System.Linq;
using System.Collections.ObjectModel;
using MITD.Core;
using MITD.Presentation;
using MITD.PMS.Presentation.Contracts;
using System.Threading;
using System.Collections.Generic;

namespace MITD.PMS.Presentation.Logic
{

    public sealed class MainWindowVM : WorkspaceViewModel, IEventHandler<MainWindowUpdateArgs>
    {

        #region Report bar view model
        public class ReportBarVM : ViewModelBase
        {
            #region IsBusy
            bool isBusy;
            public bool IsBusy
            {
                get { return isBusy; }
                set { this.SetField(vm => vm.IsBusy, ref isBusy, value); }
            }
            #endregion

            #region BusyMessage
            private string busyMessage;
            public string BusyMessage
            {
                get { return busyMessage; }
                set { this.SetField(vm => vm.BusyMessage, ref busyMessage, value); }
            }
            #endregion
        }
        #endregion

        #region Fields

        private readonly IPMSController controller;
        private readonly IUserServiceWrapper userService;

        #endregion

        #region Properties

        public IMainAppLocalizedResources LocalizedResources { get; set; }

        bool isShiftPressed;
        public bool IsShiftPressed
        {
            get { return isShiftPressed; }
            set { this.SetField(vm => vm.IsShiftPressed, ref isShiftPressed, value); }
        }

        public DateTime TimeToLogOut
        {
            get { return DateTime.Now; }
        }

        private UserStateDTO logonUser;
        public UserStateDTO LogonUser
        {
            get { return logonUser; }
            set
            {
                this.SetField(c => c.LogonUser, ref logonUser, value);
            }
        }

        private UserStateDTO currentUser;
        public UserStateDTO CurrentUser
        {
            get { return controller.CurrentUserState; }
        }

        private UserDescriptionDTO currentWorkListUser;
        public UserDescriptionDTO CurrentWorkListUser
        {
            get { return currentWorkListUser; }
            set
            {
                this.SetField(c => c.CurrentWorkListUser, ref currentWorkListUser, value);
                //todo: what the faze ???
                //if (currentWorkListUser != null)
                //{
                //    controller.ChangeCurrentWorkListUser(currentWorkListUser.PartyName);
                //}

            }
        }


        private PeriodDTO currentPriod;
        public PeriodDTO CurrentPeriod
        {
            get { return currentPriod; }
            set { this.SetField(c => c.CurrentPeriod, ref currentPriod, value); }

        }

        public CalculationStateWithRunSummaryDTO LastFinalCalculation
        {
            get { return controller.LastFinalCalculation; }
        }

        private CommandViewModel signOutCommand;
        public CommandViewModel SignOutCommand
        {
            get
            {
                if (signOutCommand == null)
                {
                    signOutCommand = new CommandViewModel("خروج", new DelegateCommand(signOut));
                }
                return signOutCommand;
            }
        }

        public ReportBarVM ReportVM { get; private set; }

        #endregion

        #region Constructor

        public MainWindowVM()
        {
            DisplayName = "سامانه بررسی عملکرد";

        }

        public MainWindowVM(IPMSController controller,
                            IUserServiceWrapper userService,
                            IMainAppLocalizedResources localizedResources)
        {
            this.controller = controller;
            this.userService = userService;
            LocalizedResources = localizedResources;
            DisplayName = LocalizedResources.PMSTitle;
            ReportVM = new ReportBarVM();
        }

        #endregion

        #region Commands

        private ReadOnlyObservableCollection<CommandViewModel> periodCommands;
        public ReadOnlyObservableCollection<CommandViewModel> PeriodCommands
        {
            get
            {
                if (periodCommands == null)
                {
                    periodCommands = new ReadOnlyObservableCollection<CommandViewModel>(createPeriodCommands());
                }
                return periodCommands;
            }
        }

        private ReadOnlyObservableCollection<CommandViewModel> basicInfoCommands;
        public ReadOnlyObservableCollection<CommandViewModel> BasicInfoCommands
        {
            get
            {
                if (basicInfoCommands == null)
                {
                    basicInfoCommands = new ReadOnlyObservableCollection<CommandViewModel>(createBasicInfoCommands());
                }
                return basicInfoCommands;
            }
        }

        private ReadOnlyObservableCollection<CommandViewModel> employeeManagementCommands;
        public ReadOnlyObservableCollection<CommandViewModel> EmployeeManagementCommands
        {
            get
            {
                if (employeeManagementCommands == null)
                {
                    employeeManagementCommands = new ReadOnlyObservableCollection<CommandViewModel>(createEmployeeManagementCommands());
                }
                return employeeManagementCommands;
            }
        }

        private ReadOnlyObservableCollection<CommandViewModel> workListCommands;
        public ReadOnlyObservableCollection<CommandViewModel> WorkListCommands
        {
            get
            {
                if (workListCommands == null)
                {
                    workListCommands = new ReadOnlyObservableCollection<CommandViewModel>(createWorkListCommands());
                }
                return workListCommands;
            }
        }

        private ReadOnlyObservableCollection<TreeElementViewModel<ReportCommandVM>> reportCommands;
        public ReadOnlyObservableCollection<TreeElementViewModel<ReportCommandVM>> ReportCommands
        {
            get
            {
                if (reportCommands == null)
                {
                    reportCommands = new ReadOnlyObservableCollection<TreeElementViewModel<ReportCommandVM>>(createReportCommands());
                }
                return reportCommands;
            }
        }

        #endregion

        #region Command Methods

        //private string path;
        //public string Path
        //{
        //    get { return path; }
        //    set { this.SetField(p => p.Path, ref path, value); }
        //}

        //public class CommandViewModelTree : ViewModelBase
        //{
        //    private CommandViewModel data;
        //    public CommandViewModel Data
        //    {
        //        get { return data; }
        //        set { this.SetField(p => p.Data, ref data, value); }
        //    }

        //    private ObservableCollection<CommandViewModelTree> childs;
        //    public ObservableCollection<CommandViewModelTree> Childs
        //    {
        //        get { return childs; }
        //        set { this.SetField(p => p.Childs, ref childs, value); }
        //    }
        //}

        private ObservableCollection<TreeElementViewModel<ReportCommandVM>> createReportCommands()
        {

            var cmdList = new ObservableCollection<TreeElementViewModel<ReportCommandVM>>();
            ReportVM.IsBusy = true;
            ReportVM.BusyMessage = "در حال دریافت اطلاعات...";
            controller.GetReportsTree(res =>
            {
                buildTree("/", res, cmdList);
                ReportVM.IsBusy = false;
            });
            return cmdList;
        }

        private void buildTree(string p, IList<ReportDTO> res, ObservableCollection<TreeElementViewModel<ReportCommandVM>> tree)
        {
            res.Where(l => l.Path == p).ToList().ForEach(parentElement =>
                {
                    var x = new TreeElementViewModel<ReportCommandVM>
                     {
                         IsExpanded = false,
                         Childs = new ObservableCollection<TreeElementViewModel<ReportCommandVM>>()
                     };
                    if (parentElement.TypeName == "Report")
                    {
                        x.Data = new ReportCommandVM(parentElement.Description, new DelegateCommand(
                            () =>
                            {
                                controller.OpenReport(parentElement);
                            }
                            ));
                    }
                    else
                    {
                        x.Data = new ReportCommandVM(parentElement.Description, new DelegateCommand(
                            () =>
                            {
                            }
                            ), true);
                    }
                    buildTree(p + parentElement.Name + "/", res, x.Childs);
                    tree.Add(x);
                });
        }

        private ObservableCollection<CommandViewModel> createPeriodCommands()
        {
            var cmdList = new ObservableCollection<CommandViewModel>();
            cmdList.Add(
               new CommandViewModel(LocalizedResources.PeriodListSubMenu, new DelegateCommand(
                   () =>
                   {
                       controller.ShowBusyIndicator("در حال بارگذاری ماجول...");
                       controller.GetRemoteInstance<IPeriodController>(
                           (res, exp) =>
                           {
                               controller.HideBusyIndicator();
                               if (res != null)
                               {
                                   res.ShowPeriodList(isShiftPressed);
                               }
                               else if (exp != null)
                               {
                                   controller.HandleException(exp);
                               }
                           });
                   }
                   )));
            cmdList.Add(
               new CommandViewModel(" واحد های سازمانی در دوره جاری", new DelegateCommand(
                   () =>
                   {
                       controller.ShowBusyIndicator("در حال بارگذاری ماجول...");
                       controller.GetRemoteInstance<IPeriodController>(
                           (res, exp) =>
                           {
                               controller.HideBusyIndicator();
                               if (res != null)
                               {
                                   res.ShowUnitInPeriodTreeView(new PeriodDTOWithAction { Id = CurrentPeriod.Id, Name = CurrentPeriod.Name }, isShiftPressed);
                               }
                               else if (exp != null)
                               {
                                   controller.HandleException(exp);
                               }
                           });
                   }
                   )));
            cmdList.Add(
             new CommandViewModel(" مدیریت شاخص سازمانی در دوره جاری", new DelegateCommand(
                 () =>
                 {
                     controller.ShowBusyIndicator("در حال بارگذاری ماجول...");
                     controller.GetRemoteInstance<IPeriodController>(
                         (res, exp) =>
                         {
                             controller.HideBusyIndicator();
                             if (res != null)
                             {
                                 res.ShowUnitIndexInPeriodTreeView(new PeriodDTOWithAction { Id = CurrentPeriod.Id, Name = CurrentPeriod.Name }, isShiftPressed);
                             }
                             else if (exp != null)
                             {
                                 controller.HandleException(exp);
                             }
                         });
                 }
                 )));

            cmdList.Add(
              new CommandViewModel("مدیریت مشاغل در دوره جاری", new DelegateCommand(
                  () =>
                  {
                      controller.ShowBusyIndicator("در حال بارگذاری ماجول...");
                      controller.GetRemoteInstance<IPeriodController>(
                          (res, exp) =>
                          {
                              controller.HideBusyIndicator();
                              if (res != null)
                              {
                                  res.ShowJobInPeriodListView(new PeriodDTO { Id = CurrentPeriod.Id, Name = CurrentPeriod.Name }, isShiftPressed);
                              }
                              else if (exp != null)
                              {
                                  controller.HandleException(exp);
                              }
                          });
                  }
                  )));


            cmdList.Add(
               new CommandViewModel(" مدیریت شاخص در دوره جاری", new DelegateCommand(
                   () =>
                   {
                       controller.ShowBusyIndicator("در حال بارگذاری ماجول...");
                       controller.GetRemoteInstance<IPeriodController>(
                           (res, exp) =>
                           {
                               controller.HideBusyIndicator();
                               if (res != null)
                               {
                                   res.ShowJobIndexInPeriodTreeView(new PeriodDTOWithAction { Id = CurrentPeriod.Id, Name = CurrentPeriod.Name }, isShiftPressed);
                               }
                               else if (exp != null)
                               {
                                   controller.HandleException(exp);
                               }
                           });
                   }
                   )));
            cmdList.Add(
               new CommandViewModel(" مدیریت پست ها در دوره جاری", new DelegateCommand(
                   () =>
                   {
                       controller.ShowBusyIndicator("در حال بارگذاری ماجول...");
                       controller.GetRemoteInstance<IPeriodController>(
                           (res, exp) =>
                           {
                               controller.HideBusyIndicator();
                               if (res != null)
                               {
                                   res.ShowJobPositionInPeriodTreeView(new PeriodDTOWithAction { Id = CurrentPeriod.Id, Name = CurrentPeriod.Name }, isShiftPressed);
                               }
                               else if (exp != null)
                               {
                                   controller.HandleException(exp);
                               }
                           });
                   }
                   )));
            cmdList.Add(
               new CommandViewModel(LocalizedResources.EmployeeListSubMenu, new DelegateCommand(
                   () =>
                   {
                       controller.ShowBusyIndicator("در حال بارگذاری ماجول...");
                       controller.GetRemoteInstance<IEmployeeController>(
                           (res, exp) =>
                           {
                               controller.HideBusyIndicator();
                               if (res != null)
                               {
                                   res.ShowEmployeeListView(CurrentPeriod, isShiftPressed);
                               }
                               else if (exp != null)
                               {
                                   controller.HandleException(exp);
                               }
                           });
                   }
                   )));

            cmdList.Add(
               new CommandViewModel(" مدیریت محاسبات در دوره جاری", new DelegateCommand(
                   () =>
                   {
                       controller.ShowBusyIndicator("در حال بارگذاری ماجول...");
                       controller.GetRemoteInstance<IPeriodController>(
                           (res, exp) =>
                           {
                               controller.HideBusyIndicator();
                               if (res != null)
                               {
                                   res.ShowCalculationListView(new PeriodDTOWithAction { Id = CurrentPeriod.Id, Name = CurrentPeriod.Name }, isShiftPressed);
                               }
                               else if (exp != null)
                               {
                                   controller.HandleException(exp);
                               }
                           });
                   }
                   )));
            return cmdList;

        }

        private ObservableCollection<CommandViewModel> createBasicInfoCommands()
        {
            var cmdList = new ObservableCollection<CommandViewModel>();

            cmdList.Add(
             new CommandViewModel(LocalizedResources.BasicInfoManagementShowPliciesTitle, new DelegateCommand(
                 () =>
                 {
                     controller.ShowBusyIndicator("در حال بارگذاری ماجول...");
                     controller.GetRemoteInstance<IBasicInfoController>(
                         (res, exp) =>
                         {
                             controller.HideBusyIndicator();
                             if (res != null)
                             {
                                 res.ShowPolicyListView(isShiftPressed);
                             }
                             else if (exp != null)
                             {
                                 controller.HandleException(exp);
                             }
                         });
                 }
                 )));

            cmdList.Add(
              new CommandViewModel(LocalizedResources.BasicInfoManagementShowCustomFieldsTitle, new DelegateCommand(
                  () =>
                  {
                      controller.ShowBusyIndicator("در حال بارگذاری ماجول...");
                      controller.GetRemoteInstance<IBasicInfoController>(
                          (res, exp) =>
                          {
                              controller.HideBusyIndicator();
                              if (res != null)
                              {
                                  res.ShowCustomFieldListView(isShiftPressed);
                              }
                              else if (exp != null)
                              {
                                  controller.HandleException(exp);
                              }
                          });
                  }
                  )));

            cmdList.Add(
              new CommandViewModel(LocalizedResources.BasicInfoManagementShowJobsTitle, new DelegateCommand(
                  () =>
                  {
                      controller.ShowBusyIndicator("در حال بارگذاری ماجول...");
                      controller.GetRemoteInstance<IBasicInfoController>(
                          (res, exp) =>
                          {
                              controller.HideBusyIndicator();
                              if (res != null)
                              {
                                  res.ShowJobListView(isShiftPressed);
                              }
                              else if (exp != null)
                              {
                                  controller.HandleException(exp);
                              }
                          });
                  }
                  )));

            cmdList.Add(
              new CommandViewModel(LocalizedResources.BasicInfoManagementShowJobIndexsTitle, new DelegateCommand(
                  () =>
                  {
                      controller.ShowBusyIndicator("در حال بارگذاری ماجول...");
                      controller.GetRemoteInstance<IBasicInfoController>(
                          (res, exp) =>
                          {
                              controller.HideBusyIndicator();
                              if (res != null)
                              {
                                  res.ShowJobIndexTreeView(isShiftPressed);
                              }
                              else if (exp != null)
                              {
                                  controller.HandleException(exp);
                              }
                          });
                  }
                  )));

            cmdList.Add(
                new CommandViewModel(LocalizedResources.BasicInfoManagementShowUnitsTitle, new DelegateCommand(
                    () =>
                    {
                        controller.ShowBusyIndicator("در حال بارگذاری ماجول...");
                        controller.GetRemoteInstance<IBasicInfoController>(
                            (res, exp) =>
                            {
                                controller.HideBusyIndicator();
                                if (res != null)
                                {
                                    res.ShowUnitList(isShiftPressed);
                                }
                                else if (exp != null)
                                {
                                    controller.HandleException(exp);
                                }
                            });
                    }
                    )));

            cmdList.Add(
                new CommandViewModel(LocalizedResources.BasicInfoManagementShowUnitIndexsTitle, new DelegateCommand(
                    () =>
                    {
                        controller.ShowBusyIndicator("در حال بارگذاری ماجول...");
                        controller.GetRemoteInstance<IBasicInfoController>(
                            (res, exp) =>
                            {
                                controller.HideBusyIndicator();
                                if (res != null)
                                {
                                    res.ShowUnitIndexTreeView(isShiftPressed);
                                }
                                else if (exp != null)
                                {
                                    controller.HandleException(exp);
                                }
                            });
                    }
                    )));

            cmdList.Add(
              new CommandViewModel(LocalizedResources.BasicInfoManagementShowJobPositionsTitle, new DelegateCommand(
                  () =>
                  {
                      controller.ShowBusyIndicator("در حال بارگذاری ماجول...");
                      controller.GetRemoteInstance<IBasicInfoController>(
                          (res, exp) =>
                          {
                              controller.HideBusyIndicator();
                              if (res != null)
                              {
                                  res.ShowJobPositionList(isShiftPressed);
                              }
                              else if (exp != null)
                              {
                                  controller.HandleException(exp);
                              }
                          });
                  }
                  )));






            cmdList.Add(
              new CommandViewModel(LocalizedResources.BasicInfoManagementShowUsersTitle, new DelegateCommand(
                  () =>
                  {
                      controller.ShowBusyIndicator("در حال بارگذاری ماجول...");
                      controller.GetRemoteInstance<IBasicInfoController>(
                          (res, exp) =>
                          {
                              controller.HideBusyIndicator();
                              if (res != null)
                              {
                                  res.ShowUserList(isShiftPressed);
                              }
                              else if (exp != null)
                              {
                                  controller.HandleException(exp);
                              }
                          });
                  }
                  )));

            cmdList.Add(
              new CommandViewModel(LocalizedResources.BasicInfoManagementShowUserGroupsTitle, new DelegateCommand(
                  () =>
                  {
                      controller.ShowBusyIndicator("در حال بارگذاری ماجول...");
                      controller.GetRemoteInstance<IBasicInfoController>(
                          (res, exp) =>
                          {
                              controller.HideBusyIndicator();
                              if (res != null)
                              {
                                  res.ShowUserGroupList(isShiftPressed);
                              }
                              else if (exp != null)
                              {
                                  controller.HandleException(exp);
                              }
                          });
                  }
                  )));

            cmdList.Add(
              new CommandViewModel(LocalizedResources.BasicInfoManagementShowLogsTitle, new DelegateCommand(
                  () =>
                  {
                      controller.ShowBusyIndicator("در حال بارگذاری ماجول...");
                      controller.GetRemoteInstance<IBasicInfoController>(
                          (res, exp) =>
                          {
                              controller.HideBusyIndicator();
                              if (res != null)
                              {
                                  res.ShowLogList(isShiftPressed);
                              }
                              else if (exp != null)
                              {
                                  controller.HandleException(exp);
                              }
                          });
                  }
                  )));

            return cmdList;

        }

        private ObservableCollection<CommandViewModel> createEmployeeManagementCommands()
        {
            var cmdList = new ObservableCollection<CommandViewModel>();

            return cmdList;
        }

        private ObservableCollection<CommandViewModel> createWorkListCommands()
        {
            var cmdList = new ObservableCollection<CommandViewModel>();
            cmdList.Add(
               new CommandViewModel(LocalizedResources.EmployeesInquiryListSubMenu, new DelegateCommand(
                   () =>
                   {
                       controller.ShowBusyIndicator("در حال بارگذاری ماجول...");
                       controller.GetRemoteInstance<IPeriodController>(
                           (res, exp) =>
                           {
                               controller.HideBusyIndicator();
                               if (res != null)
                               {
                                   res.ShowEmployeesInquiryListView(CurrentUser.EmployeeNo, CurrentPeriod.Id, isShiftPressed);
                               }
                               else if (exp != null)
                               {
                                   controller.HandleException(exp);
                               }
                           });
                   }
                   )));

            cmdList.Add(
               new CommandViewModel(LocalizedResources.EmployeesMyInquiryResultSubMenu, new DelegateCommand(
                   () =>
                   {
                       controller.ShowBusyIndicator("در حال بارگذاری ماجول...");
                       controller.GetRemoteInstance<IPeriodController>(
                           (res, exp) =>
                           {
                               controller.HideBusyIndicator();
                               if (res != null)
                               {
                                   res.ShowPeriodCalculationResultView(currentPriod, CurrentUser.EmployeeNo, isShiftPressed);
                               }
                               else if (exp != null)
                               {
                                   controller.HandleException(exp);
                               }
                           });
                   }
                   )));


            cmdList.Add(
              new CommandViewModel(LocalizedResources.ClaimSubMenu, new DelegateCommand(
                  () =>
                  {
                      controller.ShowBusyIndicator("در حال بارگذاری ماجول...");
                      controller.GetRemoteInstance<IPeriodController>(
                          (res, exp) =>
                          {
                              controller.HideBusyIndicator();
                              if (res != null)
                              {
                                  res.ShowEmployeeClaimListView(currentPriod, CurrentUser.EmployeeNo, isShiftPressed);
                              }
                              else if (exp != null)
                              {
                                  controller.HandleException(exp);
                              }
                          });
                  }
                  )));

            cmdList.Add(
              new CommandViewModel("لیست کل اعتراضات", new DelegateCommand(
                  () =>
                  {
                      controller.ShowBusyIndicator("در حال بارگذاری ماجول...");
                      controller.GetRemoteInstance<IPeriodController>(
                          (res, exp) =>
                          {
                              controller.HideBusyIndicator();
                              if (res != null)
                              {
                                  res.ShowManagerClaimListView(currentPriod, isShiftPressed);
                              }
                              else if (exp != null)
                              {
                                  controller.HandleException(exp);
                              }
                          });
                  }
                  )));

            cmdList.Add(
              new CommandViewModel(LocalizedResources.MyTasksAccessibilitySettingSubMenu, new DelegateCommand(
                  () =>
                  {
                      controller.ShowBusyIndicator("در حال بارگذاری ماجول...");
                      controller.GetRemoteInstance<IPeriodController>(
                          (res, exp) =>
                          {
                              controller.HideBusyIndicator();
                              if (res != null)
                              {
                                  res.ShowPermittedUserListToMyTasksView(CurrentUser);
                              }
                              else if (exp != null)
                              {
                                  controller.HandleException(exp);
                              }
                          });
                  }
                  )));

            //cmdList.Add(
            //   new CommandViewModel(LocalizedResources.EmployeesMyInquiryHistorySubMenu, new DelegateCommand(
            //       () =>
            //       {
            //           controller.ShowBusyIndicator("در حال بارگذاری ماجول...");
            //           controller.GetRemoteInstance<IPeriodController>(
            //               (res, exp) =>
            //               {
            //                   controller.HideBusyIndicator();
            //                   if (res != null)
            //                   {
            //                       res.ShowEmployeeCalculationResultHistoryView(CurrentUser.EmployeeId);
            //                   }
            //                   else if (exp != null)
            //                   {
            //                       controller.HandleException(exp);
            //                   }
            //               });
            //       }
            //       )));

            return cmdList;
        }

        #endregion

        #region Methods
        public void Handle(MainWindowUpdateArgs eventData)
        {
            controller.BeginInvokeOnDispatcher(() =>
                {
                    CurrentPeriod = controller.CurrentPriod;
                    LogonUser = controller.LoggedInUserState;
                    if (CurrentWorkListUser == null)
                        CurrentWorkListUser = LogonUser.PermittedUsersOnMyWorkList.First();
                    var dummy1 = BasicInfoCommands;
                    var dummy2 = WorkListCommands;
                    var dummy3 = PeriodCommands;
                    HideBusyIndicator();
                });
        }

        private void signOut()
        {
            logonUser = null;
            CurrentPeriod = null;
            controller.Logout();
        }
        #endregion

    }


}


