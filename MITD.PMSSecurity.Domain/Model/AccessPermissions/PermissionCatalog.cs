using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using MITD.Core;
using MITD.Core.Builders;
using MITD.Domain.Model;
using MITD.Domain.Repository;

namespace MITD.PMSSecurity.Domain.Model.AccessPermissions
{

    //public class PermissionCatalog<T> :PermissionCatalog where T :IFacadeService
    //{

    //    private Type _type;
    //    public PermissionCatalog()
    //    {
    //        Permissions = new List<Permission>();
    //    }

    //    public string AutoritySubject
    //    {
    //        get
    //        {
    //            return typeof(T).GetType().Name;
    //        }
    //    }





    //}

    public class PermissionCatalog : IValueObject<PermissionCatalog>
    {
        private readonly Type _type;

        public string AutoritySubject
        {
            get
            {
                return _type.Name;
            }
        }

        public List<Permission> Permissions { get; set; }
        public PermissionCatalog(Type type)
        {
            _type = type;
            Permissions = new List<Permission>();
        }

        #region Methods
        public void Set(List<Permission> permissions)
        {
            Permissions = permissions;
        }

        public void AddPermission(Permission permission)
        {
            var prm = Permissions.SingleOrDefault(c => c.MethodName.ToLower() == permission.MethodName.ToLower());
            if (prm != null)
                Permissions.Add(permission);
        }

        public void UpdatePermission(Permission permission)
        {
            var prm = Permissions.SingleOrDefault(c => c.MethodName.ToLower() == permission.MethodName.ToLower());
            if (prm != null)
            {
                prm.UpdateActions(permission.Actions);
            }

        }

        public void ResetPermissions()
        {
            Permissions = new List<Permission>();
        }

        public bool IsValidate()
        {
            return true;
        }


        public bool Permissive(string methodName, ActionType actionType)
        {
            return
                (Permissions.SingleOrDefault(
                    c => c.MethodName.ToLower() == methodName.ToLower() && c.Actions.Contains(actionType)) != null);
        }

        public Permission GetPermissionByMethod(string methodName)
        {
            return Permissions.SingleOrDefault(c => c.MethodName.ToLower() == methodName.ToLower());
        }
        public List<Permission> GetPermissionByAction(ActionType actionType)
        {
            return Permissions.Where(c => c.Actions.Contains(actionType)).ToList();
        }
        public List<string> GetMethodsNameByAction(ActionType actionType)
        {
            return Permissions.Where(c => c.Actions.Contains(actionType)).Select(c => c.MethodName).ToList();
        }
        #endregion



        #region IValueObject Member
        public virtual bool SameValueAs(PermissionCatalog other)
        {
            var builder = new EqualsBuilder();
            builder.Append(this.Permissions, other.Permissions);
            return builder.IsEquals();


        }
        #endregion
    }

    public class Permission : IValueObject<Permission>
    {
        public Permission(string methodName, List<ActionType> actions)
        {
            MethodName = methodName;
            Actions = actions;
        }

        public string MethodName { get; private set; }
        public List<ActionType> Actions { get; private set; }


        public void UpdateActions(List<ActionType> actionTypes)
        {
            Actions = actionTypes;
        }


        #region IValueObject Member
        public virtual bool SameValueAs(Permission other)
        {
            var builder = new EqualsBuilder();

            builder.Append(this.MethodName, other.MethodName);
            builder.Append(this.Actions, other.Actions);
            return builder.IsEquals();


        }
        #endregion
    }


  




}
