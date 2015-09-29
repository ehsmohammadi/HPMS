using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.Core.Builders;
using MITD.Domain.Model;

namespace MITD.PMSSecurity.Domain.Model.AccessPermissions
{
    public class Permission : IValueObject<Permission>
    {

            public string ClassName { get; private set; }
            public string MethodName { get; private set; }
            public List<ActionType> Actions { get; private set; }
            public Permission(string className, string methodName, List<ActionType> actions)
            {
                ClassName = className;
                MethodName = methodName;
                Actions = actions;
            }



        #region IValueObject Member
        public virtual bool SameValueAs(Permission other)
        {
            var builder = new EqualsBuilder();
            builder.Append(this.ClassName, other.ClassName);
            builder.Append(this.MethodName, other.MethodName);
            builder.Append(this.Actions, other.Actions);
            return builder.IsEquals();


        }
        #endregion
    }
}
