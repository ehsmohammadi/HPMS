using MITD.PMS.Integration.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMS.Integration.Domain
{
    public class Period
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public PeriodState State { get; set; }

    }
}
