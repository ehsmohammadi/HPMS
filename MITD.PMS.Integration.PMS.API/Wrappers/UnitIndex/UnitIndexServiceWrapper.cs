using System;
using MITD.PMS.Integration.Core;
using MITD.PMS.Integration.PMS.Contract;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Integration.PMS.API
{
    public class UnitIndexServiceWrapper : IUnitIndexServiceWrapper
    {

        #region Fields
        private readonly string endpoint = "UnitIndex";
        private readonly Uri apiUri = new Uri(PMSClientConfig.BaseApiAddress);
        private readonly IUserProvider userProvider;
        #endregion

        #region Not Use

        private const string unitIndexClassType = "UnitIndex";
        private const string unitIndexCategoryClassType = "UnitIndexCategory"; 

        #endregion

        #region Constructors
        public UnitIndexServiceWrapper(IUserProvider userProvider)
        {
            this.userProvider = userProvider;
        } 
        #endregion

        #region Public Methods
        public UnitIndexDTO GetUnitIndex(long id)
        {
            return IntegrationHttpClient.Get<UnitIndexDTO>(apiUri, endpoint + "?Id=" + id);
            //var url = string.Format(apiAddress + "?Id=" + id);
            //IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public UnitIndexDTO AddUnitIndex(UnitIndexDTO unitIndex)
        {
            return IntegrationHttpClient.Post<UnitIndexDTO, UnitIndexDTO>(apiUri, endpoint, unitIndex);
            //var url = string.Format(apiAddress);
            //IntegrationWebClient.Post(new Uri(url, PMSClientConfig.UriKind), action, unitIndex, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        } 
        #endregion

        //public void GetAllAbstractUnitIndex(Action<List<AbstractUnitIndexDTOWithActions>, Exception> action)
        //{
        //    var url = string.Format(apiAddress);
        //    IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        //}

        //public void UpdateUnitIndex(Action<UnitIndexDTO, Exception> action, UnitIndexDTO unitIndex)
        //{
        //    var url = string.Format(apiAddress + "?Id=" + unitIndex.Id);
        //    IntegrationWebClient.Put(new Uri(url, UriKind.Absolute), action, unitIndex, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        //}

        //public void DeleteUnitIndex(Action<string, Exception> action, long id)
        //{
        //    var url = string.Format(apiAddress + "?Id=" + id);
        //    IntegrationWebClient.Delete(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        //}

        //public void GetAllUnitIndex(Action<List<UnitIndexDTO>, Exception> action)
        //{
        //    var url = string.Format(apiAddress + "?typeOf=" + unitIndexClassType);
        //    IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        //}

        //public void GetAllUnitIndexCategorys(Action<PageResultDTO<UnitIndexCategoryDTOWithActions>, Exception> action, int pageSize, int pageIndex)
        //{
        //    var url = string.Format(apiAddress + "?typeOf=" + unitIndexCategoryClassType + "&PageSize=" + pageSize + "&PageIndex=" + pageIndex);
        //    IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        //}

        //public void GetUnitIndexCategory(Action<UnitIndexCategoryDTO, Exception> action, long id)
        //{
        //    var url = string.Format(apiAddress + "?Id=" + id);
        //    IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        //}

        //public void AddUnitIndexCategory(Action<UnitIndexCategoryDTO, Exception> action, UnitIndexCategoryDTO unitIndexCategory)
        //{
        //    var url = string.Format(apiAddress, UriKind.Absolute);
        //    IntegrationWebClient.Post(new Uri(url), action, unitIndexCategory, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        //}

        //public void UpdateUnitIndexCategory(Action<UnitIndexCategoryDTO, Exception> action, UnitIndexCategoryDTO unitIndexCategory)
        //{
        //    var url = string.Format(apiAddress + "?Id=" + unitIndexCategory.Id);
        //    IntegrationWebClient.Put(new Uri(url, PMSClientConfig.UriKind), action, unitIndexCategory, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        //}

        //public void DeleteUnitIndexCategory(Action<string, Exception> action, long id)
        //{
        //    var url = string.Format(apiAddress + "?Id=" + id);
        //    IntegrationWebClient.Delete(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        //}
    }
}
