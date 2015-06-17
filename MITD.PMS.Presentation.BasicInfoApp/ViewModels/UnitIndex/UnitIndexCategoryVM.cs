using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;
using MITD.PMS.Presentation.BasicInfoApp;
using System;


namespace MITD.PMS.Presentation.Logic
{
    public class UnitIndexCategoryVM : BasicInfoWorkSpaceViewModel
    {
        #region Fields

        private readonly IPMSController appController;
        private readonly IUnitIndexServiceWrapper unitIndexCategoryService;
        private ActionType actionType;

        #endregion

        #region Properties

        private string parentCategoryName = "ریشه";
        public string ParentCategoryName
        {
            get { return parentCategoryName; }
            set { this.SetField(vm => vm.ParentCategoryName, ref parentCategoryName, value); }
        }

        private UnitIndexCategoryDTO unitIndexCategory;
        public UnitIndexCategoryDTO UnitIndexCategory
        {
            get { return unitIndexCategory; }
            set { this.SetField(vm => vm.UnitIndexCategory, ref unitIndexCategory, value); }
        }

        private CommandViewModel saveCommand;
        public CommandViewModel SaveCommand
        {
            get
            {
                if (saveCommand == null)
                {
                    saveCommand = new CommandViewModel("ذخیره", new DelegateCommand(save));
                }
                return saveCommand;
            }
        }

        private CommandViewModel cancelCommand;
        public CommandViewModel CancelCommand
        {
            get
            {
                if (cancelCommand == null)
                {
                    cancelCommand = new CommandViewModel("انصراف", new DelegateCommand(OnRequestClose));
                }
                return cancelCommand;
            }
        }

        #endregion

        #region Constructors

        public UnitIndexCategoryVM()
        {
            UnitIndexCategory = new UnitIndexCategoryDTO { Name = "شغل یک", DictionaryName = "UnitIndexCategory1" };
        }
        public UnitIndexCategoryVM(IPMSController appController, IUnitIndexServiceWrapper unitIndexCategoryService)
        {
            this.appController = appController;
            this.unitIndexCategoryService = unitIndexCategoryService;
            UnitIndexCategory = new UnitIndexCategoryDTO();
            DisplayName = "دسته شاخص ";
        }

        #endregion

        #region Methods

        public void Load(UnitIndexCategoryDTO unitIndexCategoryParam, ActionType actionTypeParam)
        {
            actionType = actionTypeParam;
            UnitIndexCategory = unitIndexCategoryParam;

            if (UnitIndexCategory != null && unitIndexCategory.ParentId.HasValue)
            {
                ShowBusyIndicator();
                unitIndexCategoryService.GetUnitIndexCategory((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                {
                    HideBusyIndicator();
                    ParentCategoryName = res.Name;
                }), unitIndexCategory.ParentId.Value);
            }

        }


        private void save()
        {
            if (!unitIndexCategory.Validate()) return;

            ShowBusyIndicator();
            if (actionType == ActionType.AddUnitIndexCategory)
            {
                unitIndexCategoryService.AddUnitIndexCategory((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                {
                    HideBusyIndicator();
                    if (exp != null)
                        appController.HandleException(exp);
                    else
                        finalizeAction();
                }), unitIndexCategory);
            }
            else if (actionType == ActionType.ModifyUnitIndexCategory)
            {
                unitIndexCategoryService.UpdateUnitIndexCategory((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                {
                    HideBusyIndicator();
                    if (exp != null)
                        appController.HandleException(exp);
                    else
                        finalizeAction();
                }), unitIndexCategory);
            }
        }

        private void finalizeAction()
        {
            appController.Publish(new UpdateUnitIndexTreeArgs());
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

