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
    public class ShowClaimVM : PeriodMgtWorkSpaceViewModel
    { 
        #region Fields

        private readonly IPMSController appController;
        private readonly IClaimServiceWrapper claimService;

        #endregion

        #region Properties & BackField


        private ClaimDTO claim;
        public ClaimDTO Claim
        {
            get { return claim; }
            set { this.SetField(vm => vm.Claim, ref claim, value); }
        }

        


        private CommandViewModel cancelCommand;
        public CommandViewModel CancelCommand
        {
            get
            {
                if (cancelCommand == null)
                {
                    cancelCommand = new CommandViewModel("خروج",new DelegateCommand(OnRequestClose));
                }
                return cancelCommand;
            }
        }

       

        #endregion

        #region Constructors

        public ShowClaimVM()
        {

            Claim = new ClaimDTO { Title = "ff" };
        }

        public ShowClaimVM(IPMSController appController,
                            IClaimServiceWrapper claimService,
                            IPeriodMgtAppLocalizedResources periodMgtAppLocalizedResources)
        {

            this.appController = appController;
            this.claimService = claimService;
            PeriodMgtAppLocalizedResources = periodMgtAppLocalizedResources;
            Claim = new ClaimDTO();
            DisplayName = PeriodMgtAppLocalizedResources.ShowClaimViewTitle;
        } 

        #endregion

        #region Methods

        public void Load(ClaimDTO claimParam)
        {
            Claim = claimParam;
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

