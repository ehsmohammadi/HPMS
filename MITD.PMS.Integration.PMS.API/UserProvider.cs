using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMS.Integration.PMS.API
{
    public class UserProvider : IUserProvider
    {

        public string Token
        {
            get;
            set;
        }

        public string SamlToken { get; set; }

        public bool IsAuthenticated { get { return SamlToken != null; } }

        public bool IsAuthorized { get { return IsAuthenticated && Token != null; } }
    }

    public interface IUserProvider
    {
        string Token { get; set; }
        string SamlToken { get; set; }
        bool IsAuthenticated { get; }
        bool IsAuthorized { get; }
    }
}
