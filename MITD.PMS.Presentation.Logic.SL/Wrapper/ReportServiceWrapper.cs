using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace MITD.PMS.Presentation.Logic
{
    public class ReportServiceWrapper : IReportServiceWrapper
    {

        public void GetAllReports(Action<IList<Contracts.ReportDTO>, Exception> action)
        {
            var x = "/Report";
            var url = string.Format(x);
            var client = new WebClient();

            client.DownloadStringCompleted += (s, a) =>
            {
                IList<Contracts.ReportDTO> res = null;
                if (a.Error == null)
                {
                    var j = JArray.Parse(a.Result);
                    res = JsonConvert.DeserializeObject<List<Contracts.ReportDTO>>(j.ToString());
                }
                action(res, a.Error);
            };
            client.DownloadStringAsync(new Uri(url, UriKind.Relative));

        }
    }
}
