using System.Linq;
using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;
using MITD.PMS.Presentation.BasicInfoApp;
using System.Collections.Generic;

namespace MITD.PMS.Presentation.Logic
{
    public class PartyCustomActionsVM : BasicInfoWorkSpaceViewModel
    {
        #region Fields

        private readonly IPMSController appController;
        private readonly IUserServiceWrapper userService;
        private ActionType actionType;

        #endregion

        #region Properties

        private PartyDTO party ;
        public PartyDTO Party
        {
            get { return party; }
            set { this.SetField(vm => vm.Party, ref party, value); }
        }

        private List<Privilege> privilegeList;
        public List<Privilege> PrivilegeList
        {
            get { return privilegeList; }
            set { this.SetField(vm => vm.PrivilegeList, ref privilegeList, value); }
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
                    cancelCommand = new CommandViewModel("انصراف",new DelegateCommand(OnRequestClose));
                }
                return cancelCommand;
            }
        } 

        #endregion

        #region Constructors

        public PartyCustomActionsVM()
        {
            BasicInfoAppLocalizedResources=new BasicInfoAppLocalizedResources();
            init();
            party = new PartyDTO();
        }


        public PartyCustomActionsVM(IPMSController appController, 
            IUserServiceWrapper userService,
            IBasicInfoAppLocalizedResources basicInfoAppLocalizedResources
            )
        {
            this.appController = appController;
            this.userService = userService;
            BasicInfoAppLocalizedResources = basicInfoAppLocalizedResources;
            init();

        } 

        #endregion

        #region Methods

        private void init()
        {
            party = new PartyDTO();
            DisplayName = BasicInfoAppLocalizedResources.ManagePartyCustomActions;
            PrivilegeList = new List<Privilege>();
            
        }


        public void Load(PartyDTO partyDto)
        {
            Party = partyDto;
            ShowBusyIndicator();
            userService.GetAllActionTypes((res, exp) => appController.BeginInvokeOnDispatcher(() =>
                {
                    HideBusyIndicator();
                    if (exp == null)
                    {
                        PrivilegeList = res.Select(a => new Privilege() {ActionType = a, IsDeny = false, IsGrant = false}).ToList();
                        setPartyCustomActions();
                    }
                    else
                    {
                        appController.HandleException(exp);
                    }
                }));

        }

        private void setPartyCustomActions()
        {
           PrivilegeList.Where(all => Party.CustomActions.Where(c => c.Value).Select(c => c.Key).Contains(all.ActionType.Id))
                .ToList().ForEach(p => p.IsGrant = true);

            PrivilegeList.Where(all => Party.CustomActions.Where(c => !c.Value).Select(c => c.Key).Contains(all.ActionType.Id))
                .ToList().ForEach(p => p.IsDeny = true);
        }

        private void save()
        {
            var grants = PrivilegeList.Where(p => p.IsGrant).Select(p => p.ActionType).ToDictionary(p => p.Id, p=>true);
            var denies = PrivilegeList.Where(p => p.IsDeny).Select(p => p.ActionType).ToDictionary(p => p.Id, p => false);
            
            foreach (int k in denies.Keys)
            {
                if (!grants.Keys.Contains(k))
                    grants.Add(k,denies[k]);
            }
            appController.Publish(new UpdatePartyCustomActionsArgs(grants,Party.PartyName));
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

