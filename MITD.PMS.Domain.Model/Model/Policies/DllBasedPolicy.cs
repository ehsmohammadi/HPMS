using MITD.PMS.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMS.Domain.Model.Policies
{
    public class DllBasedPolicy : Policy
    {
        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region Public Methods
        public void SetPolicyEngin(IDllBasedPolicyEngineService dllBasedPolicyEngineService)
        {
            PolicyEngine = dllBasedPolicyEngineService;
        }

        #endregion

        #region IEntity Member
        #endregion


    }
}
