using System;
using MITD.PMS.Presentation.Contracts;
using System.Collections.Generic;
using MITD.Presentation;


namespace MITD.PMS.Presentation.Logic.Wrapper
{
    public class UnitInquiryServiceWrapper : IUnitInquiryServiceWrapper
    {
        private readonly IUserProvider userProvider;
        private readonly string baseAddress = PMSClientConfig.BaseApiAddress;

        public UnitInquiryServiceWrapper(IUserProvider userProvider)
        {
            this.userProvider = userProvider;
        }

        private string makeInquirerInquiryUnitApiAdress(long periodId, string inquirerEmployeeNo)
        {
            return "Periods/" + periodId + "/Inquirers/" + inquirerEmployeeNo + "/InquiryUnits";
        }

        private string makeInquiryUnitIndexPointApiAdress(long periodId, string inquirerEmployeeNo,long unitId)
        {
            return "Periods/" + periodId + "/Inquirers/" + inquirerEmployeeNo + "/Units/" + unitId + "/InquiryUnitIndexPoints";
        }


        public void GetInquirerInquirySubjects(Action<List<InquiryUnitDTO>, Exception> action, long periodId, string inquirerEmployeeNo)
        {
            var url = string.Format(baseAddress + makeInquirerInquiryUnitApiAdress(periodId, inquirerEmployeeNo ));
            WebClientHelper.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

      
        public void UpdateInquirySubjectForm(Action<InquiryUnitFormDTO, Exception> action, InquiryUnitFormDTO inquiryForm)
        {
            var url =
                string.Format(baseAddress +makeInquiryUnitIndexPointApiAdress(
                inquiryForm.PeriodId,
                inquiryForm.InquirerEmployeeNo,
                inquiryForm.InquiryUnitId));
            WebClientHelper.Put(new Uri(url, PMSClientConfig.UriKind), action, inquiryForm, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        
        public void GetInquirySubjectSubEmployeesInquiryFormList(Action<InquirySubjectInquiryFormListDTO, Exception> action,
                                                          long periodId,string inquirySubjectEmployeeNo,long inquirySubjectJobPositionId,
                                                          string managerInquirerEmployeeNo,long managerInquirerJobPositionId)
        {
            //var url = string.Format(baseAddress + makeInquirySubjectJobIndexPointApiAdress(periodId, inquirerEmployeeNo, inquirySubjectEmployeeNo, jobPositionId));
            //WebClientHelper.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat);

            
        }






        public void GetInquiryForm(Action<InquiryUnitFormDTO, Exception> action, long periodId, string inquirerEmployeeNo, long unitId)
        {
            var url = string.Format(baseAddress + makeInquiryUnitIndexPointApiAdress(periodId, inquirerEmployeeNo,unitId) );
            WebClientHelper.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        }
    }
}
