using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.Core;
using MITD.Domain.Model;
using MITD.Domain.Repository;

namespace MITD.PMSSecurity.Domain.Model.AccessPermissions
{
    public class AccessPermission : IEntity<AccessPermission>
    {

        #region Properties and Field
        private readonly AccessPermissionId id;
        public virtual AccessPermissionId Id { get { return id; } }

        private List<PermissionCatalog> PermissionCatalogs { get; set; }



        #endregion



        #region Methods

        public AccessPermission()
        {
            PermissionCatalogs=new List<PermissionCatalog>();
        }

      
        //public void Inintialize(List<Type> types)
        //{

        //    //PermissionCatalogs=new List<PermissionCatalog<IFacadeService>>();
        //    //var tt = types[0];
        //    //var t = typeof(PermissionCatalog<>);
        //    //var ttt = t.MakeGenericType(tt.GetInterfaces()[0]);
        //    //var permissionCatalog = (PermissionCatalog<IFacadeService>)Activator.CreateInstance(ttt);



        //    //permissionCatalog.Set(new List<Permission>() { 
        //    //                      new Permission("MethodName", new List<ActionType>()
        //    //                      {
        //    //                       ActionType.ActivatePeriod
             
        //    //                      })
        //    // });

        //    //permissionCatalog.AddPermission(
        //    //                     new Permission("MethodName", new List<ActionType>()
        //    //                      {
        //    //                       ActionType.ActivatePeriod
             
        //    //                      }));



        //    //if (permissionCatalog.IsValidate())
        //    //    AddPermissionCatalog(Converter(permissionCatalog));

        //    //var x = PermissionCatalogs;
        //}

        //private PermissionCatalog Converter(PermissionCatalog permissionCatalog)
        //{
        //    return (PermissionCatalog<IFacadeService>)Convert.ChangeType(permissionCatalog, typeof(PermissionCatalog<IFacadeService>));
        //}

        public void AddCatalog(PermissionCatalog catalog)
        {
            if(catalog.IsValidate())
            PermissionCatalogs.Add(catalog);
        }

        public PermissionCatalog FindCatalog(string catalogName)
        {
            return PermissionCatalogs.SingleOrDefault(c => c.AutoritySubject.ToLower() == catalogName.ToLower());
        }
        public PermissionCatalog FindCatalog(Type catalogType)
        {
            return PermissionCatalogs.SingleOrDefault(c => c.AutoritySubject.ToLower() == catalogType.Name.ToLower());
        }

        public bool IsPermissive(string catalogName,string methodName, ActionType actionType)
        {
            return FindCatalog(catalogName).Permissive(methodName, actionType);
        }
        public bool IsPermissive(Type catalogType, string methodName, ActionType actionType)
        {
            return FindCatalog(catalogType).Permissive(methodName, actionType);
        }

        public Permission GetPermissionByMethodCatalog(string catalogName,string methodName)
        {
            return FindCatalog(catalogName).GetPermissionByMethod(methodName);
        }
        public Permission GetPermissionByMethodCatalog(Type catalogtype, string methodName)
        {
            return FindCatalog(catalogtype).GetPermissionByMethod(methodName);
        }
        public List<Permission> GetPermissionByAction(string catalogName,ActionType actionType)
        {
            return FindCatalog(catalogName).GetPermissionByAction(actionType);
        }

        public List<Permission> GetPermissionByAction(Type catalogType, ActionType actionType)
        {
            return FindCatalog(catalogType).GetPermissionByAction(actionType);
        }

        public List<string> GetMethodsNameByAction(string catalogName, ActionType actionType)
        {
            return FindCatalog(catalogName).GetMethodsNameByAction(actionType);
        } 
        
        public List<string> GetMethodsNameByAction(Type catalogType, ActionType actionType)
        {
            return FindCatalog(catalogType).GetMethodsNameByAction(actionType);
        } 

        #endregion


        #region IEntity Member

        public virtual bool SameIdentityAs(AccessPermission other)
        {
            return (other != null) && Id.Equals(other.Id);
        }



        #endregion

        #region Object override

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (AccessPermission)obj;
            return SameIdentityAs(other);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return Id.ToString();
        }

        #endregion
    }
}
