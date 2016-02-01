using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.PMS.Integration.Core;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Integration.PMS.API
{
    public class PeriodServiceWrapper:IPeriodServiceWrapper
    {
        private readonly string baseAddress = PMSClientConfig.BaseApiAddress + "periods";
        private readonly string baseAddressCopyBasicData = PMSClientConfig.BaseApiAddress + "SourcePeriods";
        private IUserProvider userProvider;

        public PeriodServiceWrapper(IUserProvider userProvider)
        {
            this.userProvider = userProvider;
        }


        public void GetAllPeriods(Action<PageResultDTO<PeriodDTOWithAction>, Exception> action, int pageSize, int pageIndex)
        {
            var url = string.Format(baseAddress + "?PageSize={0}&PageIndex={1}", pageSize, pageIndex);
            IntegrationWebClient.Get(new Uri(url, UriKind.Absolute), action, PMSClientConfig.MsgFormat,
                PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        //public void GetAllPeriods(Action<ObservableCollection<PeriodDescriptionDTO>, Exception> action)
        //{
        //    var url = string.Format(baseAddress);
        //    WebClientHelper.Get(new Uri(url, UriKind.Absolute), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        //}

        //public void GetPeriodsWithDeterministicCalculation(Action<ObservableCollection<PeriodDescriptionDTO>, Exception> action)
        //{
        //    var url = string.Format(baseAddress + "?HasDeteministicCalculation=1");
        //    WebClientHelper.Get(new Uri(url, UriKind.Absolute), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        //}

        public void DeletePeriod(Action<string, Exception> action, long id)
        {
            var url = string.Format(baseAddress + "?Id={0}", id);
            IntegrationWebClient.Delete(new Uri(url, UriKind.Absolute), action, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetPeriod(Action<PeriodDTO, Exception> action, long id)
        {
            var url = string.Format(baseAddress + "?Id={0}", id);
            IntegrationWebClient.Get(new Uri(url, UriKind.Absolute), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void AddPeriod(Action<PeriodDTO, Exception> action, PeriodDTO period)
        {
            var url = string.Format(baseAddress);
            IntegrationWebClient.Post(new Uri(url, UriKind.Absolute), action, period, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void UpdatePeriod(Action<PeriodDTO, Exception> action, PeriodDTO period)
        {
            var url = string.Format(baseAddress);
            IntegrationWebClient.Put(new Uri(url, UriKind.Absolute), action, period, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetCurrentPeriod(Action<PeriodDTO, Exception> action)
        {
            var url = string.Format(baseAddress + "?Active=1");
            IntegrationWebClient.Get(new Uri(url, UriKind.Absolute), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void ChangePeriodState(Action<Exception> action, long id, PeriodStateDTO period)
        {
            var url = string.Format(baseAddress) + "/" + id + "/State";
            IntegrationWebClient.Put<PeriodStateDTO, PeriodStateDTO>(new Uri(url, UriKind.Absolute),
                (res, exp) => action(exp), period, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void CopyBasicDataFrom(Action<Exception> action, long sourcePeriodId, long destinationPeriodId, PeriodStateDTO state)
        {
            var url = string.Format(baseAddressCopyBasicData) + "/" + sourcePeriodId + "/DestinationPeriods/" + destinationPeriodId + "/State";
            IntegrationWebClient.Put<PeriodStateDTO, PeriodStateDTO>(new Uri(url, UriKind.Absolute),
                (res, exp) => action(exp), state, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        }

        public void GetPeriodStatus(Action<PeriodStateWithIntializeInquirySummaryDTO, Exception> action, long periodId)
        {
            var url = string.Format(baseAddress) + "/" + periodId + "/State";
            IntegrationWebClient.Get(new Uri(url, UriKind.Absolute),
                action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetPeriodCopyBasicDataStatus(Action<PeriodStateWithCopyingSummaryDTO, Exception> action, long periodId)
        {
            var url = string.Format(baseAddress) + "/" + periodId + "/State/?sumaryType=DataCopying";
            IntegrationWebClient.Get(new Uri(url, UriKind.Absolute),
                action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void RollBackPeriodState(Action<Exception> action, long periodId)
        {
            var url = string.Format(baseAddress) + "/" + periodId + "/State/?dummy='rollback'";
            IntegrationWebClient.Put<string, string>(new Uri(url, UriKind.Absolute),
                (res, exp) => action(exp), "rollback", PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void ChangePeriodActiveStatus(Action<Exception> action, PeriodDTO periodDto)
        {
            var url = string.Format(baseAddress);

            IntegrationWebClient.Put<PeriodDTO, PeriodDTO>(new Uri(url, UriKind.Absolute), (res, exp) => action(exp), periodDto
                , PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        }




        public CalculationStateWithRunSummaryDTO LastFinalCalculation()
        {
            return new CalculationStateWithRunSummaryDTO { Percent = 100 };
        }





    }
}
