using System.Web;
using System.Web.Mvc;

namespace MITD.PMS.Interface.Presentation.Host
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}