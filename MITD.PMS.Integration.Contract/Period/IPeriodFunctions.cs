using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMS.Integration.Contract.Period
{
    public interface IPeriodFunctions
    {

        void InsertEmployees();

        void InsertUnits();

        void InsertNodeType();

        void InsertJobTitles();

        void InsertOrganChart();

    }
}
