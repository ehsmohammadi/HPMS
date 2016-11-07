using System;
using System.Collections.ObjectModel;
using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;
using System.Collections.Generic;

namespace MITD.PMS.Presentation.Logic.Wrapper
{
    public partial class PeriodServiceWrapper : IPeriodServiceWrapper
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
            var url = string.Format(baseAddress + "?PageSize={0}&PageIndex={1}",pageSize,pageIndex);
            WebClientHelper.Get(new Uri(url, UriKind.Absolute), action, PMSClientConfig.MsgFormat,
                PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetAllPeriods(Action<ObservableCollection<PeriodDescriptionDTO>, Exception> action)
        {
            var url = string.Format(baseAddress);
            WebClientHelper.Get(new Uri(url, UriKind.Absolute), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetPeriodsWithConfirmedResult(Action<ObservableCollection<PeriodDescriptionDTO>, Exception> action)
        {
            var url = string.Format(baseAddress + "?hasConfirmedResult=1");
            WebClientHelper.Get(new Uri(url, UriKind.Absolute), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetEmployeeResultInPeriod(Action<EmployeeResultDTO, Exception> action, long periodId, string employeeNo)
        {
            var url = string.Format(baseAddress +"?periodId="+periodId+ "&employeeNo="+employeeNo);
            WebClientHelper.Get(new Uri(url, UriKind.Absolute), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }


        //todo: bad webapi arrangement for below three methods( Iam so sorry for myself )
        public void GetSubordinatesResultInPeriod(Action<SubordinatesResultDTO, Exception> action, long periodId, string managerEmployeeNo)
        {
            var url = string.Format(baseAddress + "?periodId=" + periodId + "&managerEmployeeNo=" + managerEmployeeNo + "&isForManager=true");
            WebClientHelper.Get(new Uri(url, UriKind.Absolute), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetTrainingEmployeeIndicesInPeriod(Action<List<JobIndexValueDTO>, Exception> action, long periodId, string trainerEmployeeNo)
        {
            var url = string.Format(baseAddress + "?periodId=" + periodId + "&isForTrainer=yes");
            WebClientHelper.Get(new Uri(url, UriKind.Absolute), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetTrainingNeedEmployeeInPeriod(Action<SubordinatesResultDTO, Exception> action, long periodId, string trainerEmployeeNo, long jobIndexId)
        {
            var url = string.Format(baseAddress + "?periodId=" + periodId + "&jobindexId="+jobIndexId);
            WebClientHelper.Get(new Uri(url, UriKind.Absolute), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void DeletePeriod(Action<string, Exception> action, long id)
        {
            var url = string.Format(baseAddress + "?Id={0}", id);
            WebClientHelper.Delete(new Uri(url, UriKind.Absolute), action, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetPeriod(Action<PeriodDTO, Exception> action, long id)
        {
            var url = string.Format(baseAddress + "?Id={0}", id);
            WebClientHelper.Get(new Uri(url, UriKind.Absolute), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void AddPeriod(Action<PeriodDTO, Exception> action, PeriodDTO period)
        {
            var url = string.Format(baseAddress);
            WebClientHelper.Post(new Uri(url, UriKind.Absolute), action, period, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void UpdatePeriod(Action<PeriodDTO, Exception> action, PeriodDTO period)
        {
            var url = string.Format(baseAddress);
            WebClientHelper.Put(new Uri(url, UriKind.Absolute), action, period, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetCurrentPeriod(Action<PeriodDTO, Exception> action)
        {
            var url = string.Format(baseAddress+"?Active=1");
            WebClientHelper.Get(new Uri(url, UriKind.Absolute), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void ChangePeriodState(Action<Exception> action, long id, PeriodStateDTO period)
        {
            var url = string.Format(baseAddress) + "/" + id + "/State";
            WebClientHelper.Put<PeriodStateDTO, PeriodStateDTO>(new Uri(url, UriKind.Absolute), 
                (res,exp)=>action(exp), period, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void CopyBasicDataFrom(Action<Exception> action, long sourcePeriodId, long destinationPeriodId,PeriodStateDTO state)
        {
            var url = string.Format(baseAddressCopyBasicData) + "/" + sourcePeriodId + "/DestinationPeriods/" + destinationPeriodId+"/State";
            WebClientHelper.Put<PeriodStateDTO, PeriodStateDTO>(new Uri(url, UriKind.Absolute),
                (res, exp) => action(exp), state, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
  
        }

        public void GetPeriodStatus(Action<PeriodStateWithIntializeInquirySummaryDTO, Exception> action, long periodId)
        {
            var url = string.Format(baseAddress) + "/" + periodId + "/State";
            WebClientHelper.Get(new Uri(url, UriKind.Absolute),
                action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetPeriodCopyBasicDataStatus(Action<PeriodStateWithCopyingSummaryDTO, Exception> action, long periodId)
        {
            var url = string.Format(baseAddress) + "/" + periodId + "/State/?sumaryType=DataCopying";
            WebClientHelper.Get(new Uri(url, UriKind.Absolute),
                action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void RollBackPeriodState(Action<Exception> action, long periodId)
        {
            var url = string.Format(baseAddress) + "/" + periodId + "/State/?dummy='rollback'";
            WebClientHelper.Put<string,string>(new Uri(url, UriKind.Absolute),
                (res, exp) => action(exp), "rollback", PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void ChangePeriodActiveStatus(Action<Exception> action, PeriodDTO periodDto)
        {
            var url = string.Format(baseAddress);

            WebClientHelper.Put<PeriodDTO, PeriodDTO>(new Uri(url, UriKind.Absolute), (res, exp) => action(exp), periodDto
                , PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
 
        }




        public CalculationStateWithRunSummaryDTO LastFinalCalculation()
        {
            return new CalculationStateWithRunSummaryDTO { Percent = 100 };
        }





    }
}
