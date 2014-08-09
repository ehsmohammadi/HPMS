using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Presentation.Logic
{
    public static partial class TestData
    {
       private static List<int> inquirySubjectListActionCodes =  new List<int>
            {
                (int)ActionType.ManageInquiryForm 
            };

        private static List<int> CustomInquirerActionCodes = new List<int>
            {
                 (int)ActionType.DeleteCustomInquirer 
            };

        public static List<InquirySubjectDTO> inquirySubjectList = new List<InquirySubjectDTO>
            {
                //new InquirySubjectDTO
                //    {
                //        EmployeeId = employeeList.Where(e=>e.Id == 1).Single().Id,
                //        ActionCodes = inquirySubjectListActionCodes,
                //        FullName = employeeList.Where(e=>e.Id == 1).Single().FirstName
                //                + " " +employeeList.Where(e=>e.Id == 1).Single().LastName,
                //        IsInquired = false
                //    },
                //     new InquirySubjectDTO
                //    {
                //        EmployeeId = employeeList.Where(e=>e.Id == 2).Single().Id,
                //        ActionCodes = inquirySubjectListActionCodes,
                //        FullName = employeeList.Where(e=>e.Id == 2).Single().FirstName
                //                + " " +employeeList.Where(e=>e.Id == 2).Single().LastName,
                //        IsInquired = false
                //    },
              
            };

        //public static List<JobIndexValueDTO> jobIndexValuelist = new List<JobIndexValueDTO>
        //    {
        //        new JobIndexValueDTO
        //            {
        //                Title = JobIndexList.Single(j => j.Id == 10).Name,
        //                JobIndexId = JobIndexList.Single(j => j.Id == 10).Id,
        //                IndexValue = 3,

        //            },
        //        new JobIndexValueDTO
        //            {
        //                Title = JobIndexList.Single(j => j.Id == 11).Name,
        //                JobIndexId = JobIndexList.Single(j => j.Id == 11).Id,
        //                IndexValue = 6,
        //            },
        //        new JobIndexValueDTO
        //            {
        //                Title = JobIndexList.Single(j => j.Id == 12).Name,
        //                JobIndexId = JobIndexList.Single(j => j.Id == 12).Id,
        //                IndexValue = 8,
        //            },
        //        new JobIndexValueDTO
        //            {
        //                Title = JobIndexList.Single(j => j.Id == 13).Name,
        //                JobIndexId = JobIndexList.Single(j => j.Id == 13).Id,
        //                IndexValue = 5,
        //            },
        //        new JobIndexValueDTO
        //            {
        //                Title = JobIndexList.Single(j => j.Id == 14).Name,
        //                JobIndexId = JobIndexList.Single(j => j.Id == 14).Id,
        //                IndexValue = 8,
        //            },
        //    };

        

        
    }
}
