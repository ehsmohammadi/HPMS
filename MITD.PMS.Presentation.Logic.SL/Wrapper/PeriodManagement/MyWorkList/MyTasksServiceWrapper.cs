using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

namespace MITD.PMS.Presentation.Logic.Wrapper
{
    public partial class MyTasksServiceWrapper : IMyTasksServiceWrapper
    {

        private readonly string baseAddress = PMSClientConfig.BaseApiAddress;
        private IUserProvider userProvider;

        public MyTasksServiceWrapper(IUserProvider userProvider)
        {
            this.userProvider = userProvider;
        }
       
        private string makeApiAdress(string username)
        {
            return "Users/" + username + "/PermittedWorkListUsers";
        }
       


        public void GetAllPermittedUsersToMyTasks(Action<PageResultDTO<UserDTOWithActions>, Exception> action, string username, int pageSize, int pageIndex)
        {
            //action(new PageResultDTO<UserDTOWithActions>()
            //    { 
            //        PageSize = 10,
            //        CurrentPage=1,
            //        TotalCount=1,
            //        TotalPages =1,
            //        Result = new Collection<UserDTOWithActions>()
            //            {
            //                new UserDTOWithActions() { FirstName = "پدارم", LastName = "احسانی", PartyName = "ped@g.com", ActionCodes = new List<int>() { 1411 ,1412,1413} },
            //                new UserDTOWithActions() { FirstName = "فرزاد", LastName = "محمدی", PartyName = "ped2@g.com", ActionCodes = new List<int>() { 1411 ,1412,1413} }
            //            }
            //    }, null);

            var url = string.Format(baseAddress + makeApiAdress(username)) + "?PageSize=" + pageSize + "&PageIndex=" + pageIndex ;
            WebClientHelper.Get(new Uri(url, UriKind.Absolute), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        }

        public void RemovePermittedUserFromMyTasks(Action<string, Exception> action, string currentUsername, string removeUsername)
        {
            var url = string.Format(baseAddress + makeApiAdress(currentUsername) + "/" + removeUsername);
  
        }

        public void GetAllAcceptableUsersToPermitOnMyTasks(Action<PageResultDTO<UserDTO>, Exception> action, string username)
        {
            action(new PageResultDTO<UserDTO>()
            {
                PageSize = 10,
                CurrentPage = 1,
                TotalCount = 1,
                TotalPages = 1,
                Result = new Collection<UserDTO>()
                    {
                        new UserDTO() { FirstName = "پدارم", LastName = "احسانی", PartyName = "ped@g.com" },
                        new UserDTO() { FirstName = "فرزاد", LastName = "محمدی", PartyName = "ped2@g.com"}
                    }
            }, null);
        }


        public void PermitUsersToMyTasks(Action<string, Exception> action, string username, List<string> usernameList)
        {
            throw new NotImplementedException();
        }
    }
}
