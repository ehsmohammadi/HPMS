using System;
using System.Linq;
using System.Windows;
using MITD.PMS.Presentation.Contracts;
using System.Collections.Generic;
using MITD.Presentation;


namespace MITD.PMS.Presentation.Logic.Wrapper
{
    public class UnitIndexServiceWrapper : IUnitIndexServiceWrapper
    {
        private readonly IUserProvider userProvider;
        private readonly string apiAddress = PMSClientConfig.BaseApiAddress + "UnitIndex";
        private const string unitIndexClassType = "UnitIndex";
        private const string unitIndexCategoryClassType = "UnitIndexCategory";


        public UnitIndexServiceWrapper(IUserProvider userProvider)
        {
            this.userProvider = userProvider;
        }

        public void GetAllAbstractUnitIndex(Action<List<AbstractUnitIndexDTOWithActions>, Exception> action)
        {
            var url = string.Format(apiAddress );
            WebClientHelper.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
           
        }

        public void GetUnitIndex(Action<UnitIndexDTO, Exception> action, long id)
        {
            var url = string.Format(apiAddress + "?Id=" + id);
            WebClientHelper.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void AddUnitIndex(Action<UnitIndexDTO, Exception> action, UnitIndexDTO unitIndex)
        {
            var url = string.Format(apiAddress);
            WebClientHelper.Post(new Uri(url, PMSClientConfig.UriKind), action, unitIndex, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void UpdateUnitIndex(Action<UnitIndexDTO, Exception> action, UnitIndexDTO unitIndex)
        {
            var url = string.Format(apiAddress + "?Id=" + unitIndex.Id);
            WebClientHelper.Put(new Uri(url, UriKind.Absolute), action, unitIndex, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void DeleteUnitIndex(Action<string, Exception> action, long id)
        {
            var url = string.Format(apiAddress + "?Id=" + id);
            WebClientHelper.Delete(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetAllUnitIndex(Action<List<UnitIndexDTO>, Exception> action)
        {
            var url = string.Format(apiAddress +"?typeOf="+unitIndexClassType );
            WebClientHelper.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetAllUnitIndexCategorys(Action<PageResultDTO<UnitIndexCategoryDTOWithActions>, Exception> action, int pageSize, int pageIndex)
        {
            var url = string.Format(apiAddress + "?typeOf=" + unitIndexCategoryClassType + "&PageSize=" + pageSize + "&PageIndex=" + pageIndex);
            WebClientHelper.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        }

        public void GetUnitIndexCategory(Action<UnitIndexCategoryDTO, Exception> action, long id)
        {
            var url = string.Format(apiAddress + "?Id=" + id);
            WebClientHelper.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        }

        public void AddUnitIndexCategory(Action<UnitIndexCategoryDTO, Exception> action, UnitIndexCategoryDTO unitIndexCategory)
        {
            var url = string.Format(apiAddress , UriKind.Absolute);
            WebClientHelper.Post(new Uri(url), action, unitIndexCategory, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void UpdateUnitIndexCategory(Action<UnitIndexCategoryDTO, Exception> action, UnitIndexCategoryDTO unitIndexCategory)
        {
            var url = string.Format(apiAddress+"?Id="+unitIndexCategory.Id);
            WebClientHelper.Put(new Uri(url, PMSClientConfig.UriKind), action, unitIndexCategory, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }
        
        public void DeleteUnitIndexCategory(Action<string, Exception> action, long id)
        {
            var url = string.Format(apiAddress+ "?Id=" + id);
            WebClientHelper.Delete(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        }



       
    }
}
