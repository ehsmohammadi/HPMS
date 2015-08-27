﻿using System;
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

        private string makeInquirySubjectJobIndexPointApiAdress(long periodId, string inquirerEmployeeNo,
            string inquirySubjectEmployeeNo, long jobPositionId)
        {
            return "Periods/" + periodId + "/Inquirers/" + inquirerEmployeeNo + "/InquirySubjects/" +
                   inquirySubjectEmployeeNo + "/JobPositions/" + jobPositionId + "/InquiryJobIndexPoints";
        }


        public void GetInquirerInquirySubjects(Action<List<InquiryUnitDTO>, Exception> action, long periodId, string inquirerEmployeeNo)
        {
            var url = string.Format(baseAddress + makeInquirerInquiryUnitApiAdress(periodId, inquirerEmployeeNo ));
            WebClientHelper.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetInquiryForm(Action<InquiryUnitDTO, Exception> action, long periodId, string inquirerEmployeeNo, long inquirerJobPositionId, string inquirySubjectEmployeeNo, long jobPositionId)
        {
            var url = string.Format(baseAddress + makeInquirySubjectJobIndexPointApiAdress(periodId, inquirerEmployeeNo, inquirySubjectEmployeeNo, jobPositionId) + "?InquirerJobPositionId=" + inquirerJobPositionId);
            WebClientHelper.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void UpdateInquirySubjectForm(Action<InquiryUnitFormDTO, Exception> action, InquiryUnitFormDTO inquiryForm)
        {
            //var url =
            //    string.Format(baseAddress +
            //                  makeInquirySubjectJobIndexPointApiAdress(inquiryForm.PeriodId, inquiryForm.InquirerEmployeeNo,
            //                      inquiryForm.InquirySubjectEmployeeNo, inquiryForm.JobPositionId)+"?Batch=1");
            //WebClientHelper.Put(new Uri(url, PMSClientConfig.UriKind), action, inquiryForm, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        
        public void GetInquirySubjectSubEmployeesInquiryFormList(Action<InquirySubjectInquiryFormListDTO, Exception> action,
                                                          long periodId,string inquirySubjectEmployeeNo,long inquirySubjectJobPositionId,
                                                          string managerInquirerEmployeeNo,long managerInquirerJobPositionId)
        {
            //var url = string.Format(baseAddress + makeInquirySubjectJobIndexPointApiAdress(periodId, inquirerEmployeeNo, inquirySubjectEmployeeNo, jobPositionId));
            //WebClientHelper.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat);

            
        }



      
    }
}
