using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMS.Integration.Data.Contract.DTO
{
    public class JobPositionIntegrationDTO
    {
        public int ID { get; set; }

        public string JobPositionName { get; set; }

        public Guid TransferId { get; set; }

        public UnitIntegrationDTO UnitIntegrationDTO { get; set; }

        public JobIntegrationDto JobIntegrationDto { get; set; }
    }
}
