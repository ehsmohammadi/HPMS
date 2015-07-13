﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.PMS.Integration.Data.Contract.DTO;

namespace MITD.PMS.Integration.Data.Contract.DataProvider
{
    public interface IEmployeeDataProvider
    {

        IList<long> GetEmployeeIds();
        EmployeeDTO GetEmployeeDetails(long id);


    }
}