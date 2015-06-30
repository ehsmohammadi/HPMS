using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMS.Integration.Data.Contract.DTO
{
    public class OrganChartNodeDto
    {

        public int ID { get; set; }

        public int? PID { get; set; }

        public String NodeName { get; set; }

        public int? UnitID { get; set; }

        public int? NodeTypeID { get; set; }

        public int? PersonID { get; set; }

        public int? TitleID { get; set; }

    }
}
