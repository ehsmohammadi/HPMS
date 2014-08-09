using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic.Wrapper
{
    public partial class CalculationServiceWrapper : ICalculationServiceWrapper
    {
        private readonly IUserProvider userProvider;

        private readonly string baseAddress = PMSClientConfig.BaseApiAddress;

        public CalculationServiceWrapper(IUserProvider userProvider)
        {
            this.userProvider = userProvider;
        }

        private string makeApiAdress(long periodId)
        {
            return "Periods/" + periodId + "/Calculations";
        }

        public void GetAllCalculation(Action<PageResultDTO<CalculationBriefDTOWithAction>, Exception> action, long periodId, int pageSize, int pageIndex)
        {
            var url = string.Format(baseAddress + makeApiAdress(periodId) + "?PageSize=" + pageSize + "&PageIndex=" + pageIndex);
            WebClientHelper.Get<PageResultDTO<CalculationBriefDTOWithAction>>(new Uri(url, UriKind.Absolute),
                action,
                WebClientHelper.MessageFormat.Json, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void DeleteCalculation(Action<string, Exception> action, long periodId, long id)
        {
            var url = string.Format(baseAddress + makeApiAdress(periodId) + "/" + id);
            WebClientHelper.Delete(new Uri(url, UriKind.Absolute),
                action, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }


        public void GetAllCalculationResults(Action<PageResultDTO<JobIndexPointSummaryDTOWithAction>, Exception> action, 
            long periodId, long calculationId, int pageSize, int pageIndex)
        {
            var url = string.Format(baseAddress + makeApiAdress(periodId) + "/"+calculationId+"/JobIndexPoints"+"?PageSize=" + pageSize + "&PageIndex=" + pageIndex);
            WebClientHelper.Get<PageResultDTO<JobIndexPointSummaryDTOWithAction>>(new Uri(url, UriKind.Absolute),
                action, WebClientHelper.MessageFormat.Json, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        }

        public void GetEmployeeSummaryCalculationResult(Action<JobIndexPointSummaryDTOWithAction, Exception> action, long periodId, long calculationId, string employeeNo)
        {
            var url = string.Format(baseAddress + makeApiAdress(periodId) + "/" + calculationId + "/JobIndexPoints" + "?EmployeeNo=" + employeeNo + "&ResultType=summary");
            WebClientHelper.Get<JobIndexPointSummaryDTOWithAction>(new Uri(url, UriKind.Absolute),
                action, WebClientHelper.MessageFormat.Json, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        //public void GetCalculationResultDetails(Action<EmployeeCalculationResultDetailsDTO, Exception> action, long calculationId, 
        //    string employeeNo, int pageSize, int pageIndex)
        //{
        //    action(
        //        new EmployeeCalculationResultDetailsDTO
        //        {
        //        }, null);
        //}

        public void GetCalculationState(Action<CalculationStateWithRunSummaryDTO, Exception> action, long periodId, long id)
        {
            var url = string.Format(baseAddress + makeApiAdress(periodId) + "/" + id + "/State");
            WebClientHelper.Get<CalculationStateWithRunSummaryDTO>(new Uri(url, UriKind.Absolute),
                (res, exp) => action(res, exp),
                WebClientHelper.MessageFormat.Json, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void ChangeCalculationState(Action<Exception> action, long periodId,  long id, CalculationStateDTO calculationState)
        {
            var url = string.Format(baseAddress + makeApiAdress(periodId) +"/" + id+"/State");
            WebClientHelper.Put<CalculationStateDTO, CalculationStateDTO>(new Uri(url, UriKind.Absolute),
                (res,exp) => action(exp), calculationState,
                WebClientHelper.MessageFormat.Json, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetEmployeeJobPositionsCalculationResult(Action<List<JobPositionValueDTO>, Exception> action, long periodId, long calculationId, string employeeNo)
        {
            var url = string.Format(baseAddress + makeApiAdress(periodId) + "/" + calculationId + "/JobIndexPoints" + "?employeeNo=" + employeeNo);
            WebClientHelper.Get <List<JobPositionValueDTO>>(new Uri(url, UriKind.Absolute),
                action, WebClientHelper.MessageFormat.Json, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetDeterministicCalculation(Action<CalculationDTO, Exception> action, long periodId)
        {
            var url = string.Format(baseAddress + makeApiAdress(periodId) );
            WebClientHelper.Get<CalculationDTO>(new Uri(url, UriKind.Absolute), action, WebClientHelper.MessageFormat.Json, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void ChangeDeterministicCalculation(Action<Exception> action, CalculationDTO calacDto)
        {
            var url = string.Format(baseAddress + makeApiAdress(calacDto.PeriodId));
            WebClientHelper.Put<CalculationDTO, CalculationDTO>(new Uri(url, UriKind.Absolute), (res, exp) => action(exp), calacDto
                , PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetAllCalculationException(Action<PageResultDTO<CalculationExceptionBriefDTOWithAction>, Exception> action, long periodId, long calculationId, int pageSize, int pageIndex)
        {
            var url = string.Format(baseAddress + makeApiAdress(periodId) + "/" +calculationId+ "/Exceptions"  + "?PageSize=" + pageSize + "&PageIndex=" + pageIndex);
            WebClientHelper.Get(new Uri(url, UriKind.Absolute),
                action,WebClientHelper.MessageFormat.Json, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetCalculationException(Action<CalculationExceptionDTO, Exception> action, long periodId, long calculationId, long exceptionId)
        {
            var url = string.Format(baseAddress + makeApiAdress(periodId) + "/" + calculationId + "/Exceptions/" +exceptionId  );
            WebClientHelper.Get(new Uri(url, UriKind.Absolute),
                action,WebClientHelper.MessageFormat.Json, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }


        public void GetCalculation(Action<CalculationDTO, Exception> action, long periodId, long id)
        {
            var url = string.Format(baseAddress + makeApiAdress(periodId) + "/" + id );
            WebClientHelper.Get<CalculationDTO>(new Uri(url, UriKind.Absolute),
                action,
                WebClientHelper.MessageFormat.Json, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void AddCalculation(Action<CalculationDTO, Exception> action, CalculationDTO calculation)
        {
            var url = string.Format(baseAddress + makeApiAdress(calculation.PeriodId));
            WebClientHelper.Post(new Uri(url, UriKind.Absolute), action, calculation, WebClientHelper.MessageFormat.Json, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }      

    }
}
