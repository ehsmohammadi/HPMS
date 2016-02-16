using System.ComponentModel;
using MITD.Core;
using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;
using MITD.PMS.Presentation.BasicInfoApp;
using MITD.PMS.Presentation.BasicInfoApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MITD.PMS.Presentation.Logic
{
    public class CustomFieldListVM : BasicInfoWorkSpaceViewModel, IEventHandler<UpdateCustomFieldListArgs>
    {
        #region Fields

        private readonly IPMSController appController;
        private readonly ICustomFieldServiceWrapper customFieldService;
        
        #endregion

        #region Properties & Back fields
         
        private PagedSortableCollectionView<CustomFieldDTOWithActions> customFieldes;
        public PagedSortableCollectionView<CustomFieldDTOWithActions> CustomFieldes
        {
            get { return customFieldes; }
            set { this.SetField(p => p.CustomFieldes, ref customFieldes, value); }
        }

        private CustomFieldDTOWithActions selectedCustomField;
        public CustomFieldDTOWithActions SelectedCustomField
        {
            get { return selectedCustomField; }
            set
            {
                this.SetField(p => p.SelectedCustomField, ref selectedCustomField, value);
                if (selectedCustomField == null) return;
                CustomFieldCommands = createCommands();
                if (View != null)
                    ((ICustomFieldListView)View).CreateContextMenu(new ReadOnlyCollection<DataGridCommandViewModel>(CustomFieldCommands));
            }
        }

        private ObservableCollection<CustomFieldEntity> customFieldEntities;
        public ObservableCollection<CustomFieldEntity> CustomFieldEntities
        {
            get { return customFieldEntities; }
            set { this.SetField(p => p.CustomFieldEntities, ref customFieldEntities, value); }
        }

        private DataGridCommandViewModel selectedCommand;
        public DataGridCommandViewModel SelectedCommand
        {
            get { return selectedCommand; }
            set { this.SetField(p => p.SelectedCommand, ref selectedCommand, value); }
        }

        
        private CommandViewModel filterCommand;
        public CommandViewModel FilterCommand
        {
            get
            {
                if (filterCommand == null)
                {
                    filterCommand = new CommandViewModel(BasicInfoAppLocalizedResources.CustomFieldListViewFilterCommandTitle,new DelegateCommand(refresh));
                }
                return filterCommand;
            }
        }

        private List<DataGridCommandViewModel> customFieldCommands;
        public List<DataGridCommandViewModel> CustomFieldCommands
        {
            get { return customFieldCommands; }
            private set { 
                this.SetField(p => p.CustomFieldCommands, ref customFieldCommands, value);
                if (CustomFieldCommands.Count > 0) SelectedCommand = CustomFieldCommands[0];
            }

        }

        private CustomFieldCriteria customFieldCriteria;
        public CustomFieldCriteria CustomFieldCriteria
        {
            get { return customFieldCriteria; }
            set { this.SetField(p => p.CustomFieldCriteria, ref customFieldCriteria, value); }
        }


        #endregion

        #region Constructors

        public CustomFieldListVM()
        {
            init();
            CustomFieldes.Add(new CustomFieldDTOWithActions{Name = "ehsan"});
            BasicInfoAppLocalizedResources=new BasicInfoAppLocalizedResources();
        }

        public CustomFieldListVM(IBasicInfoController basicInfoController, IPMSController appController,
            ICustomFieldServiceWrapper customFieldService, IBasicInfoAppLocalizedResources localizedResources)
        {
            this.appController = appController;
            this.customFieldService = customFieldService;
            BasicInfoAppLocalizedResources = localizedResources;
            DisplayName = BasicInfoAppLocalizedResources.CustomFieldListViewTitle;
            init();
        }

        #endregion

        #region Methods

        void init()
        {
            CustomFieldCriteria = new CustomFieldCriteria();
            CustomFieldes = new PagedSortableCollectionView<CustomFieldDTOWithActions> { PageSize = 20 };
            CustomFieldEntities = new ObservableCollection<CustomFieldEntity>();
            CustomFieldes.OnRefresh += (s, args) => refresh();
            CustomFieldCommands = new List<DataGridCommandViewModel>
            {
                   CommandHelper.GetControlCommands(this, appController, new List<int>{ (int) ActionType.CreateCustomField }).FirstOrDefault()
            };
            

        }
       
        private List<DataGridCommandViewModel> createCommands()
        {
            return CommandHelper.GetControlCommands(this, appController, SelectedCustomField.ActionCodes);
        }

        public void Load()
        {
            preload();
            refresh();
        }

        private void preload()
        {
            CustomFieldEntities = new ObservableCollection<CustomFieldEntity>();
            foreach (var cfe in appController.CustomFieldEntityList)
            {
                CustomFieldEntities.Add(cfe);
            }
        }

        private void refresh()
        {
            var sortBy = CustomFieldes.SortDescriptions.ToDictionary(sortDesc => sortDesc.PropertyName, sortDesc =>
                (sortDesc.Direction == ListSortDirection.Ascending ? "ASC" : "DESC"));
            ShowBusyIndicator("در حال دریافت اطلاعات...");

            customFieldService.GetAllCustomFieldes(
                   (res, exp) => appController.BeginInvokeOnDispatcher(() =>
                  {
                      HideBusyIndicator();
                      if (exp == null)
                      {
                          if(res.Result!=null)
                          customFieldes.SourceCollection = res.Result;
                          else
                              customFieldes.SourceCollection = new Collection<CustomFieldDTOWithActions>();
                          customFieldes.TotalItemCount = res.TotalCount;
                          customFieldes.PageIndex = Math.Max(0, res.CurrentPage - 1);
                      }
                      else appController.HandleException(exp);
                  }), customFieldes.PageSize, customFieldes.PageIndex + 1,sortBy,CustomFieldCriteria);
        }


        protected override void OnRequestClose()
        {
            base.OnRequestClose();
            appController.Close(this);
        }
     
        public void Handle(UpdateCustomFieldListArgs eventData)
        {
            refresh();
        }

        #endregion


    }
}
