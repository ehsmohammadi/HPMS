using System.Linq;
using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;
using System.Collections.Generic;

namespace MITD.PMS.Presentation.Logic
{
    public class CommandHelper
    {
        public static List<DataGridCommandViewModel> GetControlCommands<T>(T viewModel, IEnumerable<PMSAction> actions)
        {
             return actions.Select(c => new DataGridCommandViewModel
                {
                    Icon = c.ActionIcon ,
                    CommandViewModel = new CommandViewModel(c.ActionName,new DelegateCommand(()=>c.DoAction(viewModel) ))
                }).ToList();
        }

        public static List<DataGridCommandViewModel> GetControlCommands<T>(T viewModel, IPMSController controller, List<int> actionCodes)
        {
            if(actionCodes==null)
                return new List<DataGridCommandViewModel>();
            var actions = controller.PMSActions.Where(a => actionCodes.Contains((int) a.ActionCode) 
                &&  
                controller.CurrentUser.PermittedActions.Select(p=>(int)p).Contains((int)a.ActionCode)
                );
            return GetControlCommands(viewModel, actions);
        }

        public static CommandViewModel GetControlCommands<T>(T viewModel, PMSAction action)
        {
            if (action == null)
                return null;
            return new CommandViewModel(action.ActionName, new DelegateCommand(() => action.DoAction(viewModel)));
        }

        public static CommandViewModel GetControlCommands<T>(T viewModel, IPMSController controller, int actionCode)
        {
            var action = controller.PMSActions.SingleOrDefault(a => actionCode == (int)a.ActionCode
                 &&
                controller.CurrentUser.PermittedActions.Select(p => (int)p).Contains((int)a.ActionCode)
                );
            return GetControlCommands(viewModel,action);
            //new CommandViewModel(action.ActionName, new DelegateCommand(() => action.DoAction(t)));
        }
    }
}
