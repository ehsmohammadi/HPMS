using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMSSecurity.Domain.Model
{
    /// <summary>
    /// This attribute is used to assign action types to methods.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class RequiredPermissionAttribute : Attribute
    {
        /// <summary>
        /// Unique type  for each action.
        /// </summary>
        public ActionType ActionType { get; private set; }

        public RequiredPermissionAttribute(ActionType actionType)
        {
            this.ActionType = actionType;
        }
    }
}
