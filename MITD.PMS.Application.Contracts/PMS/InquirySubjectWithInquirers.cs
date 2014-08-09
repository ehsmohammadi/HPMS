using System.Collections.Generic;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.JobPositions;
using System;

namespace MITD.PMS.Application.Contracts
{
    public class InquirySubjectWithInquirers
    {

        public string InquirySubjectEmployeeNo
        {
            get; set;
        }


        public List<string> InquirerEmployeeNoList
        {
            get;
            set;
        }

    }
}
