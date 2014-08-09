using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public interface IMyTasksServiceWrapper:IServiceWrapper
    {
        void GetAllPermittedUsersToMyTasks(Action<PageResultDTO<UserDTOWithActions>, Exception> action, string username, int pageSize, int pageIndex);
        void RemovePermittedUserFromMyTasks(Action<string, Exception> action, string userName, string removeUsername);
        void GetAllAcceptableUsersToPermitOnMyTasks(Action<PageResultDTO<UserDTO>, Exception> action, string userName);
        void PermitUsersToMyTasks(Action<string, Exception> action, string username, List<string> userIdList);
    }
}
