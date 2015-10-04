using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Windows.Input;
using MITD.PMS.Presentation.Contracts;
using MITD.PMSSecurity.Domain;
using MITD.PMSSecurity.Domain.Model.AccessPermissions;

namespace MITD.PMS.Service.Host
{
    public class AccessPermissionSetup
    {

        public void Execute(AccessPermission accessPermission)
        {
          
            var catalog = new PermissionCatalog(typeof(IUnitFacadeService));
            catalog.AddPermission(new Permission("MethodName", new List<ActionType>()));

            accessPermission.AddCatalog(catalog);

            //  var x = ap.FindCatalog("IUnitFacadeService");
        }
    }
}