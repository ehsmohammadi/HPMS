using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Presentation.Logic
{ 
    public static partial class TestData
    {
       private static List<int> EmployeeActionCodes =  new List<int>
            {
               (int)ActionType.ModifyEmployee,(int)ActionType.DeleteEmployee,(int)ActionType.AddEmployee,(int) ActionType.ManageEmployeeJobPostion
            };
       public static List<EmployeeDTOWithActions> employeeList = new List<EmployeeDTOWithActions>
            {
                //new EmployeeDTODescriptionWithActions { Id = 1, 
                //    FirstName = "احسان", 
                //    LastName = "محمدی",
                //    PersonnelNo = 1111,
                //    ActionCodes = EmployeeActionCodes
                //},

                //new EmployeeDTODescriptionWithActions { Id = 2, 
                //    FirstName = "رضا", 
                //    LastName = "صادقی",
                //    PersonnelNo = 2222,
                //    ActionCodes = EmployeeActionCodes
                //},
                // new EmployeeDTODescriptionWithActions { Id = 3, 
                //    FirstName = "مهران", 
                //    LastName = "مستوفی",
                //    PersonnelNo = 3333,
                //    ActionCodes = EmployeeActionCodes
                //},
                // new EmployeeDTODescriptionWithActions { Id = 4, 
                //    FirstName = "علیرضا", 
                //    LastName = "احسانی",
                //    PersonnelNo = 4444,
                //    ActionCodes = EmployeeActionCodes
                //},
                // new EmployeeDTODescriptionWithActions { Id = 5, 
                //    FirstName = "نارنین", 
                //    LastName = "طلایی",
                //    PersonnelNo = 5555,
                //    ActionCodes = EmployeeActionCodes
                //},

                //new EmployeeDTODescriptionWithActions { Id = 6, 
                //    FirstName = "محمد", 
                //    LastName = "کاشفی فر",
                //    PersonnelNo = 6666,
                //    ActionCodes = EmployeeActionCodes
                //},
                // new EmployeeDTODescriptionWithActions { Id = 7, 
                //    FirstName = "میترا", 
                //    LastName = "مرتضویان",
                //    PersonnelNo = 7777,
                //    ActionCodes = EmployeeActionCodes
                //},
                // new EmployeeDTODescriptionWithActions { Id = 8, 
                //    FirstName = "محمد رضا", 
                //    LastName = "کیانی فرد",
                //    PersonnelNo = 8888,
                //    ActionCodes = EmployeeActionCodes
                //},
            };

        public static List<CustomFieldValueDTO> GetEmployeeCustomFields()
        {
            var cfs = customFields.Where(c => c.EntityId == 4);
            var res = new List<CustomFieldValueDTO>();
            foreach (var c in cfs)
            {
                res.Add(new CustomFieldValueDTO
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Value = "5"
                    }
                    );
            }
            return res;
        }





    }
}
