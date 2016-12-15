using System;
using System.Collections.Generic;
using System.Windows;
using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;
using System.Linq;
using MITD.PMS.Presentation.PeriodManagementApp;
using MITD.PMS.Presentation.PeriodManagementApp.Views;
using System.Collections.ObjectModel;

namespace MITD.PMS.Presentation.Logic
{
    public class ClaimVM : PeriodMgtWorkSpaceViewModel
    { 
        #region Fields

        private readonly IPMSController appController;
        private readonly IClaimServiceWrapper claimService;
        private string employeeNo;
        private ActionType actionType;

        #endregion

        #region Properties & BackField


        private bool isReplyMode;
        public bool IsReplyMode
        {
            get { return isReplyMode; }
            set { this.SetField(vm => vm.IsReplyMode, ref isReplyMode, value); }
        }

        public bool IsAddClaimMode
        {
            get { return !IsReplyMode; }
        }

        public Visibility ReplyVisibilityMode
        {
            get
            {
                var visibile = IsReplyMode?  Visibility.Visible :  Visibility.Collapsed;
                return visibile;
            }
        }

        public Visibility AddClaimVisibilityMode
        {
            get
            {
                var visibile = IsAddClaimMode ? Visibility.Visible : Visibility.Collapsed;
                return visibile;
            }
        }


        private ClaimDTO claim;
        public ClaimDTO Claim
        {
            get { return claim; }
            set { this.SetField(vm => vm.Claim, ref claim, value); }
        }

        private List<ClaimTypeDTO> claimTypeList;
        public List<ClaimTypeDTO> ClaimTypeList
        {
            get { return claimTypeList; }
            set { this.SetField(vm => vm.ClaimTypeList, ref claimTypeList, value); }
        }

        private List<ClaimStateDTO> claimStateList;
        public List<ClaimStateDTO> ClaimStateList
        {
            get { return claimStateList; }
            set { this.SetField(vm => vm.ClaimStateList, ref claimStateList, value); }
        }

        private CommandViewModel saveCommand;
        public CommandViewModel SaveCommand
        {
            get
            {
                if (saveCommand == null)
                {
                    saveCommand = new CommandViewModel(" ارسال ", new DelegateCommand(save));
                }
                return saveCommand;
            }
        }

        private CommandViewModel acceptCommand;
        public CommandViewModel AcceptCommand
        {
            get
            {
                if (acceptCommand == null)
                {
                    acceptCommand = new CommandViewModel(" تایید اعتراض ", new DelegateCommand(accept));
                }
                return acceptCommand;
            }
        }

        private CommandViewModel rejectCommand;
        public CommandViewModel RejectCommand
        {
            get
            {
                if (rejectCommand == null)
                {
                    rejectCommand = new CommandViewModel(" رد اعتراض ", new DelegateCommand(reject));
                }
                return rejectCommand;
            }
        }

        private CommandViewModel cancelCommand;
        public CommandViewModel CancelCommand
        {
            get
            {
                if (cancelCommand == null)
                {
                    cancelCommand = new CommandViewModel("انصراف",new DelegateCommand(OnRequestClose));
                }
                return cancelCommand;
            }
        }

       

        #endregion

        #region Constructors

        public ClaimVM()
        {

            Claim = new ClaimDTO { Title = "ff" };
        }

        public ClaimVM(IPMSController appController,
                            IClaimServiceWrapper claimService,
                            IPeriodMgtAppLocalizedResources periodMgtAppLocalizedResources)
        {

            this.appController = appController;
            this.claimService = claimService;
            PeriodMgtAppLocalizedResources = periodMgtAppLocalizedResources;
            Claim = new ClaimDTO();
            DisplayName = PeriodMgtAppLocalizedResources.ClaimViewTitle;
        } 

        #endregion

        #region Methods

        public void Load(ClaimDTO claimParam,ActionType actionTypeParam)
        {
            actionType = actionTypeParam;
            if (actionTypeParam == ActionType.AddClaim)
                IsReplyMode = false;
            else if (actionTypeParam == ActionType.ReplyToClaim)
                IsReplyMode = true;
            else
                throw new Exception("Action Mode Is not Valid");

            Claim = claimParam;
            preload();
        }

        private void preload()
        {
            ShowBusyIndicator("در حال دریافت اطلاعات...");
            claimService.GetClaimTypeList((res, exp) =>  appController.BeginInvokeOnDispatcher(() =>
            {
                HideBusyIndicator();
                if (exp == null)
                {
                    getAllClaimStates();
                    ClaimTypeList = res;
                }
                else
                    appController.HandleException(exp);
            }),Claim.PeriodId);


            //claimService.GetAcceptableClaimStateList((res, exp) =>
            

        }

        private void getAllClaimStates()
        {
            ShowBusyIndicator("در حال دریافت اطلاعات...");
            claimService.GetAllClaimStateList((res, exp) => appController.BeginInvokeOnDispatcher(() =>
            {
                HideBusyIndicator();
                if (exp == null)
                    ClaimStateList = res;
                else
                    appController.HandleException(exp);
            }), Claim.PeriodId);
        }
       
        private void save()
        {
            if (!claim.Validate()) 
                return;
            ShowBusyIndicator();
            if (actionType==ActionType.AddClaim)
            {
                Claim.EmployeeNo = appController.CurrentUser.EmployeeNo;
                Claim.PeriodId = appController.CurrentPriod.Id;
                claimService.AddClaim((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                    {
                        HideBusyIndicator();
                        if (exp != null)
                            appController.HandleException(exp);
                        else
                        {
                            appController.Publish(new UpdateClaimListArgs(Claim.EmployeeNo));
                            FinalizeAction();
                        }
                    }), Claim);
            }
            
        }


        private void accept()
        {
            if (!claim.Validate()) return;
            ShowBusyIndicator();
            if (actionType == ActionType.ReplyToClaim)
            {
                if (appController.ShowConfirmationBox("آیا از تایید  درخواست اعتراض اطمینان دارید؟", "تایید درخواست اعتراض"))
                {
                    claim.ResponseDate = DateTime.Now;
                    claimService.ChangeClaimState((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                        {
                            HideBusyIndicator();
                            if (exp != null)
                                appController.HandleException(exp);
                            else
                            {
                                appController.Publish(new UpdateClaimListArgs(Claim.EmployeeNo));
                                FinalizeAction();
                            }
                        }), claim.PeriodId, claim.Id, claim.Response, claimStateList.Single(s => s.Id == 3));

                }
            }
        }


        private void reject()
        {
            if (!claim.Validate()) return;
            ShowBusyIndicator();
            if (actionType == ActionType.ReplyToClaim)
            {
                if (appController.ShowConfirmationBox("آیا از رد  درخواست اعتراض اطمینان دارید؟", "رد درخواست اعتراض"))
                {
                    claim.ResponseDate = DateTime.Now;
                    claimService.ChangeClaimState((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                        {
                            HideBusyIndicator();
                            if (exp != null)
                                appController.HandleException(exp);
                            else
                            {
                                appController.Publish(new UpdateClaimListArgs(Claim.EmployeeNo));
                                FinalizeAction();
                            }
                        }), claim.PeriodId, claim.Id, claim.Response, claimStateList.Single(s => s.Id == 4));
                }
            }

        }

        private void FinalizeAction()
        {
            
            OnRequestClose();
        }

        protected override void OnRequestClose()
        {
            base.OnRequestClose();
            appController.Close(this);
        } 

        #endregion
    }
}

