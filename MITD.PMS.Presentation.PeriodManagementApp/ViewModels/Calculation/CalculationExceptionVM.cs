using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MITD.Core;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.PeriodManagementApp;
using MITD.PMS.Presentation.PeriodManagementApp.Views;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public class CalculationExceptionVM : PeriodMgtWorkSpaceViewModel
    {
         #region Fields

        private readonly IPMSController appController;
        private readonly ICalculationServiceWrapper calculationService;

        #endregion

        #region Properties & Backfields

       

        private CalculationExceptionDTO calculationException;
        public CalculationExceptionDTO CalculationException
        {
            get { return calculationException; }
            set { this.SetField(vm => vm.CalculationException, ref calculationException, value); }
        }


        private CommandViewModel cancelCommand;
        public CommandViewModel CancelCommand
        {
            get
            {
                if (cancelCommand == null)
                {
                    cancelCommand = new CommandViewModel("بستن", new DelegateCommand(OnRequestClose));
                }
                return cancelCommand;
            }
        }

        
        #endregion

        #region Constructors

        public CalculationExceptionVM()
        {

            init();
            CalculationException = new CalculationExceptionDTO { EmployeeFullName = "sss"};
        }
        public CalculationExceptionVM(IPMSController appController, ICalculationServiceWrapper calculationService, IPeriodMgtAppLocalizedResources periodMgtAppLocalizedResources)
        {
            init();
            this.appController = appController;
            this.calculationService = calculationService;
            PeriodMgtAppLocalizedResources = periodMgtAppLocalizedResources;
            DisplayName = PeriodMgtAppLocalizedResources.CalculationExceptionViewTitle;
        }

        #endregion

        #region Methods

        private void init()
        {
            CalculationException = new CalculationExceptionDTO();

        }

        private void preLoad()
        {
            //ShowBusyIndicator("در حال دریافت اطلاعات");
            //calculationService.GetCalculationException((res, exp) => appController.BeginInvokeOnDispatcher(() =>
            //{
            //    HideBusyIndicator();
            //    if (exp == null)
            //        CalculationExceptionExcuteTimeStr = res.Single(r=>r.Id == CalculationException.ExcuteTime).Name;
            //    else
            //        appController.HandleException(exp);
            //}),calculationException.CalculationId,calculationException.CalculationId,ca);
        }
        public void Load(CalculationExceptionDTO calculationExceptionParam)
        {
            CalculationException = calculationExceptionParam;
            preLoad();
        }

        
        private void finalizeAction()
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
