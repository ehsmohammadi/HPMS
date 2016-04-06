using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMS.Integration.Data.Contract.DTO
{
    public class JobIndexIntegrationDTO
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Guid TransferId { get; set; }

        public long? JobId { get; set; }

        public long JobIndexId { get; set; }

        public long IndexType { get; set; }

        public int Coefficient { get; set; }
    }
}
