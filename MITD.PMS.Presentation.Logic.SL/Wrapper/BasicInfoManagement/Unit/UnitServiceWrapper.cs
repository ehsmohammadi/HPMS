using System;
using System.Windows;
using MITD.PMS.Presentation.Contracts;
using System.Collections.Generic;
using System.Linq;
using MITD.Presentation;


namespace MITD.PMS.Presentation.Logic.Wrapper
{
    public class UnitServiceWrapper : IUnitServiceWrapper
    {
        private readonly IUserProvider userProvider;

        private string baseAddress = PMSClientConfig.BaseApiAddress + "Units";

        public UnitServiceWrapper(IUserProvider userProvider)
        {
            this.userProvider = userProvider;
        }

        public void GetAllUnits(Action<PageResultDTO<UnitDTOWithActions>, Exception> action, int pageSize, int pageIndex)
        {

            var url = string.Format(baseAddress + "?PageSize=" + pageSize + "&PageIndex=" + pageIndex);
            WebClientHelper.Get(new Uri(url, UriKind.Absolute),
                action, WebClientHelper.MessageFormat.Json, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void DeleteUnit(Action<string, Exception> action, long id)
        {
            var url = string.Format(baseAddress + "?Id="+id);
            WebClientHelper.Delete(new Uri(url, UriKind.Absolute), action, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetAllUnits(Action<List<UnitDTO>, Exception> action)
        {
            var url = string.Format(baseAddress );
            WebClientHelper.Get(new Uri(url, UriKind.Absolute),
                action, WebClientHelper.MessageFormat.Json, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetUnit(Action<UnitDTO, Exception> action, long id)
        {
            var url = string.Format(baseAddress + "?Id=" + id);
            WebClientHelper.Get(new Uri(url, UriKind.Absolute),
                action, WebClientHelper.MessageFormat.Json, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void AddUnit(Action<UnitDTO, Exception> action, UnitDTO unit)
        {

            var url = string.Format(baseAddress);
            WebClientHelper.Post(new Uri(url, UriKind.Absolute),
                action, unit, WebClientHelper.MessageFormat.Json, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void UpdateUnit(Action<UnitDTO, Exception> action, UnitDTO unit)
        {
            var url = string.Format(baseAddress);
            WebClientHelper.Put(new Uri(url, UriKind.Absolute),
                action,unit,
                WebClientHelper.MessageFormat.Json, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

    }
}
