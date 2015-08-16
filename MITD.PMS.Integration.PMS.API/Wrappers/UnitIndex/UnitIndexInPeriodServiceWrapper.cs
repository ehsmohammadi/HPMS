using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.PMS.Integration.Core;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Integration.PMS.API
{
    public class UnitIndexInPeriodServiceWrapper : IUnitIndexInPeriodServiceWrapper
    {
        private readonly IUserProvider userProvider;
        private readonly string baseAddress = PMSClientConfig.BaseApiAddress;
        private readonly string unitIndexClassType = "UnitIndex";
        private readonly string unitIndexCategoryClassType = "UnitIndexGroup";

        public UnitIndexInPeriodServiceWrapper(IUserProvider userProvider)
        {
            this.userProvider = userProvider;
        }

        private string makeApiAdress(long periodId)
        {
            return "Periods/" + periodId + "/UnitIndices";
        }

        #region Temprory Presentation Code


        #endregion


        public void DeleteUnitIndexInPeriod(Action<string, Exception> action, long periodId, long unitIndexId)
        {
            var url = string.Format(baseAddress + makeApiAdress(periodId) + "?abstractId=" + unitIndexId);
            IntegrationWebClient.Delete(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetPeriodAbstractIndexes(Action<List<AbstractIndexInPeriodDTOWithActions>, Exception> action, long periodId)
        {
            var url = string.Format(baseAddress + makeApiAdress(periodId));
            IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        }

        public void GetAllPeriodUnitIndexes(Action<List<UnitIndexInPeriodDTO>, Exception> action, long periodId)
        {
            var url = string.Format(baseAddress + makeApiAdress(periodId) + "?typeOf=UnitIndex");
            IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        }

        public void GetUnitIndexInPeriod(Action<UnitIndexInPeriodDTO, Exception> action, long periodId, long abstractId)
        {
            //var c = TestData.abstractIndexInPeriodDtoWithActionses.Single(j => j.Id == id);
            //action(new UnitIndexInPeriodDTO
            //{
            //    Id = c.Id,
            //    Name = c.Name,
            //    ParentId = c.ParentId,
            //    UnitIndexId = ((UnitIndexInPeriodDTOWithActions)c).UnitIndexId 
            //}, null);
            GetUnitIndexInPeriod(action, periodId, abstractId, "");
        }

        public void GetUnitIndexInPeriod(Action<UnitIndexInPeriodDTO, Exception> action, long periodId, long abstractId, string columnNames)
        {
            var url = string.Format(baseAddress + makeApiAdress(periodId) + "?abstractId=" + abstractId);
            url += !string.IsNullOrWhiteSpace(columnNames) ? "&SelectedColumns=" + columnNames : "";
            IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        }

        public void AddUnitIndexInPeriod(Action<UnitIndexInPeriodDTO, Exception> action, UnitIndexInPeriodDTO unitIndexInPeriod)
        {
            //action(unitIndexInPeriod, null);
            //var url = string.Format(baseAddress);
            //WebClientHelper.Post<UnitIndexInPeriod, UnitIndexInPeriod>(new Uri(url, UriKind.Absolute),
            //    (res, exp) => action(res, exp), unitIndexInPeriod,
            //    WebClientHelper.MessageFormat.Json);

            var url = string.Format(baseAddress + makeApiAdress(unitIndexInPeriod.PeriodId));
            IntegrationWebClient.Post(new Uri(url, PMSClientConfig.UriKind), action, unitIndexInPeriod, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        }

        public void UpdateUnitIndexInPeriod(Action<UnitIndexInPeriodDTO, Exception> action, UnitIndexInPeriodDTO unitIndexInPeriod)
        {
            //action(unitIndexInPeriod, null);
            //var url = string.Format(baseAddress + "?Id=" + unitIndexInPeriod.Id);
            //WebClientHelper.Put<UnitIndexInPeriod, UnitIndexInPeriod>(new Uri(url, UriKind.Absolute),
            //    (res, exp) => action(res, exp), unitIndexInPeriod,
            //    WebClientHelper.MessageFormat.Json);
            var url = string.Format(baseAddress + makeApiAdress(unitIndexInPeriod.PeriodId));
            IntegrationWebClient.Put(new Uri(url, PMSClientConfig.UriKind), action, unitIndexInPeriod, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        }

        public void AddUnitIndexGroupInPeriod(Action<UnitIndexGroupInPeriodDTO, Exception> action, UnitIndexGroupInPeriodDTO unitIndexGroupInPeriod)
        {
            var url = string.Format(baseAddress + makeApiAdress(unitIndexGroupInPeriod.PeriodId));
            IntegrationWebClient.Post(new Uri(url, PMSClientConfig.UriKind), action, unitIndexGroupInPeriod, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        }

        public void UpdateUnitIndexGroupInPeriod(Action<UnitIndexGroupInPeriodDTO, Exception> action, UnitIndexGroupInPeriodDTO unitIndexGroupInPeriod)
        {
            var url = string.Format(baseAddress + makeApiAdress(unitIndexGroupInPeriod.PeriodId));
            IntegrationWebClient.Put(new Uri(url, PMSClientConfig.UriKind), action, unitIndexGroupInPeriod, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        }

        public void GetUnitIndexGroupInPeriod(Action<UnitIndexGroupInPeriodDTO, Exception> action, long periodId,
                                             long abstractId)
        {
            GetUnitIndexGroupInPeriod(action, periodId, abstractId, "");
        }

        public void GetUnitIndexGroupInPeriod(Action<UnitIndexGroupInPeriodDTO, Exception> action, long periodId, long abstractId, string columnNames)
        {
            //var c = TestData.abstractIndexInPeriodDtoWithActionses.Single(j => j.Id == id);
            //action(new UnitIndexGroupInPeriodDTO
            //    {
            //        Id = c.Id,
            //        Name = c.Name,
            //        ParentId = c.ParentId
            //    }, null);

            var url = string.Format(baseAddress + makeApiAdress(periodId) + "?abstractId=" + abstractId);
            url += !string.IsNullOrWhiteSpace(columnNames) ? "&SelectedColumns=" + columnNames : "";
            IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        }

        public void DeleteUnitIndexGroupInPeriod(Action<string, Exception> action, long periodId, long abstractId)
        {
            var url = string.Format(baseAddress + makeApiAdress(periodId) + "?abstractId=" + abstractId);
            IntegrationWebClient.Delete(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetPeriodUnitIndexes(Action<List<UnitIndexGroupInPeriodDTO>, Exception> action, long periodId)
        {
            throw new NotImplementedException();
        }
    }
}
