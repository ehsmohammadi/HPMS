using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.PMS.Integration.Core;
using MITD.PMS.Integration.PMS.Contract;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Integration.PMS.API
{
    public class UnitIndexInPeriodServiceWrapper : IUnitIndexInPeriodServiceWrapper
    {
        private readonly Uri apiUri = new Uri(PMSClientConfig.BaseApiAddress);

        private readonly IUserProvider userProvider;
        private readonly string unitIndexClassType = "UnitIndex";
        private readonly string unitIndexCategoryClassType = "UnitIndexGroup";

        public UnitIndexInPeriodServiceWrapper(IUserProvider userProvider)
        {
            this.userProvider = userProvider;
        }

        private string makeEndPoint(long periodId)
        {
            return "Periods/" + periodId + "/UnitIndices";
        }



        public UnitIndexInPeriodDTO GetUnitIndexInPeriod(long periodId, long abstractId)
        {
            return GetUnitIndexInPeriod(periodId, abstractId, "");
        }

        public UnitIndexInPeriodDTO GetUnitIndexInPeriod(long periodId, long abstractId, string columnNames)
        {
            var endpoint = makeEndPoint(periodId);
            endpoint += !string.IsNullOrWhiteSpace(columnNames) ? "&SelectedColumns=" + columnNames : "";
            return IntegrationHttpClient.Get<UnitIndexInPeriodDTO>(apiUri, endpoint + "?abstractId=" + abstractId);
            //var url = string.Format(baseAddress + makeApiAdress(periodId) + "?abstractId=" + abstractId);
            //url += !string.IsNullOrWhiteSpace(columnNames) ? "&SelectedColumns=" + columnNames : "";
            //IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        }

        public UnitIndexInPeriodDTO AddUnitIndexInPeriod( UnitIndexInPeriodDTO unitIndexInPeriod)
        {
            return IntegrationHttpClient.Post<UnitIndexInPeriodDTO, UnitIndexInPeriodDTO>(apiUri,
                makeEndPoint(unitIndexInPeriod.PeriodId), unitIndexInPeriod);
            //var url = string.Format(baseAddress + makeApiAdress(unitIndexInPeriod.PeriodId));
            //IntegrationWebClient.Post(new Uri(url, PMSClientConfig.UriKind), action, unitIndexInPeriod, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        }

        public List<AbstractUnitIndexInPeriodDTO> GetAllUnitIndexGroup(long periodId)
        {
            var endpoint = makeEndPoint(periodId) + string.Format("?typeOf=" + "UnitIndexGroup" );
            return IntegrationHttpClient.Get<List<AbstractUnitIndexInPeriodDTO>>(apiUri, endpoint);
        }


        //public void DeleteUnitIndexInPeriod(Action<string, Exception> action, long periodId, long unitIndexId)
        //{
        //    var url = string.Format(baseAddress + makeApiAdress(periodId) + "?abstractId=" + unitIndexId);
        //    IntegrationWebClient.Delete(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        //}

        //public void GetPeriodAbstractIndexes(Action<List<AbstractIndexInPeriodDTOWithActions>, Exception> action, long periodId)
        //{
        //    var url = string.Format(baseAddress + makeApiAdress(periodId));
        //    IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        //}

        //public void GetAllPeriodUnitIndexes(Action<List<UnitIndexInPeriodDTO>, Exception> action, long periodId)
        //{
        //    var url = string.Format(baseAddress + makeApiAdress(periodId) + "?typeOf=UnitIndex");
        //    IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        //}

        //public void UpdateUnitIndexInPeriod(Action<UnitIndexInPeriodDTO, Exception> action, UnitIndexInPeriodDTO unitIndexInPeriod)
        //{
        //    var url = string.Format(baseAddress + makeApiAdress(unitIndexInPeriod.PeriodId));
        //    IntegrationWebClient.Put(new Uri(url, PMSClientConfig.UriKind), action, unitIndexInPeriod, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        //}

        //public void AddUnitIndexGroupInPeriod(Action<UnitIndexGroupInPeriodDTO, Exception> action, UnitIndexGroupInPeriodDTO unitIndexGroupInPeriod)
        //{
        //    var url = string.Format(baseAddress + makeApiAdress(unitIndexGroupInPeriod.PeriodId));
        //    IntegrationWebClient.Post(new Uri(url, PMSClientConfig.UriKind), action, unitIndexGroupInPeriod, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        //}

        //public void UpdateUnitIndexGroupInPeriod(Action<UnitIndexGroupInPeriodDTO, Exception> action, UnitIndexGroupInPeriodDTO unitIndexGroupInPeriod)
        //{
        //    var url = string.Format(baseAddress + makeApiAdress(unitIndexGroupInPeriod.PeriodId));
        //    IntegrationWebClient.Put(new Uri(url, PMSClientConfig.UriKind), action, unitIndexGroupInPeriod, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        //}

        //public void GetUnitIndexGroupInPeriod(Action<UnitIndexGroupInPeriodDTO, Exception> action, long periodId,
        //                                     long abstractId)
        //{
        //    GetUnitIndexGroupInPeriod(action, periodId, abstractId, "");
        //}

        //public void GetUnitIndexGroupInPeriod(Action<UnitIndexGroupInPeriodDTO, Exception> action, long periodId, long abstractId, string columnNames)
        //{
        //    var url = string.Format(baseAddress + makeApiAdress(periodId) + "?abstractId=" + abstractId);
        //    url += !string.IsNullOrWhiteSpace(columnNames) ? "&SelectedColumns=" + columnNames : "";
        //    IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        //}

        //public void DeleteUnitIndexGroupInPeriod(Action<string, Exception> action, long periodId, long abstractId)
        //{
        //    var url = string.Format(baseAddress + makeApiAdress(periodId) + "?abstractId=" + abstractId);
        //    IntegrationWebClient.Delete(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        //}

        //public void GetPeriodUnitIndexes(Action<List<UnitIndexGroupInPeriodDTO>, Exception> action, long periodId)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
