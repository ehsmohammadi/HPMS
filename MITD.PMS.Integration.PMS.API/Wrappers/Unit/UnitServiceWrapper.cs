using System;
using MITD.PMS.Integration.Core;
using MITD.PMS.Integration.PMS.Contract;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Integration.PMS.API
{
    public class UnitServiceWrapper : IUnitServiceWrapper
    {

        private readonly IUserProvider userProvider;

        private Uri apiUri = new Uri(PMSClientConfig.BaseApiAddress);

        private string endpoint = "Units";

        public UnitServiceWrapper(IUserProvider userProvider)
        {
            this.userProvider = userProvider;
        }


        public UnitDTO GetUnit(long id)
        {
            return IntegrationHttpClient.Get<UnitDTO>(apiUri, endpoint + "?Id=" + id);
            //var url = string.Format(baseAddress + "?Id=" + id);
            //IntegrationWebClient.Get(new Uri(url, UriKind.Absolute),
            //    action, IntegrationWebClient.MessageFormat.Json, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public UnitDTO GetByTransferId(Guid transferId)
        {
            return IntegrationHttpClient.Get<UnitDTO>(apiUri, endpoint + "?TransferId=" + transferId);
        }

        public UnitDTO AddUnit(UnitDTO unit)
        {
            return IntegrationHttpClient.Post<UnitDTO, UnitDTO>(apiUri, endpoint, unit);
            //var url = string.Format(baseAddress);
            //IntegrationWebClient.Post(new Uri(url, UriKind.Absolute),
            //    action, unit, IntegrationWebClient.MessageFormat.Json, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        //public void GetAllUnits(Action<PageResultDTO<UnitDTOWithActions>, Exception> action, int pageSize, int pageIndex)
        //{

        //    var url = string.Format(baseAddress + "?PageSize=" + pageSize + "&PageIndex=" + pageIndex);
        //    IntegrationWebClient.Get(new Uri(url, UriKind.Absolute),
        //        action, IntegrationWebClient.MessageFormat.Json, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        //}

        //public void DeleteUnit(Action<string, Exception> action, long id)
        //{
        //    var url = string.Format(baseAddress + "?Id=" + id);
        //    IntegrationWebClient.Delete(new Uri(url, UriKind.Absolute), action, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        //}

        //public void GetAllUnits(Action<List<UnitDTO>, Exception> action)
        //{
        //    var url = string.Format(baseAddress);
        //    IntegrationWebClient.Get(new Uri(url, UriKind.Absolute),
        //        action, IntegrationWebClient.MessageFormat.Json, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        //}



        //public void UpdateUnit(Action<UnitDTO, Exception> action, UnitDTO unit)
        //{
        //    var url = string.Format(baseAddress);
        //    IntegrationWebClient.Put(new Uri(url, UriKind.Absolute),
        //        action, unit,
        //        IntegrationWebClient.MessageFormat.Json, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        //}

    }
}
