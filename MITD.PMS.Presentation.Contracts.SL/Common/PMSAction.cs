using System;
using System.Windows.Input;
using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class PMSAction:ViewModelBase
    {
        public ActionType ActionCode
        {
            get;
            set;
        }
        public int RoleCode
        {
            get;
            set;
        }

        public string ActionName
        {
            get;
            set;
        }

        public string ActionIcon
        {
            get;
            set;
        }


        private IActionService actionService;
        public IActionService ActionService
        {
            get { return actionService; }
            set
            {
                actionService = value;
                command = new DelegateCommand<int>(DoAction);

            }
        }

        private ICommand command;
        


        public void DoAction<T>(T obj)
        {
            ((IActionService<T>)ActionService).DoAction(obj);
            //ActionService.DoAction(obj);
        }
        public ICommand Command
        {
            get { return command; }
            
        }
    }
}
