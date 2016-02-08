using System;
using System.Collections.Generic;
using System.Text;

namespace MITD.PMS.Integration.Data.Contract.DTO
{
    public class JobIntegrationDto
    {

        public long Id { get; set; }

        public string Title { get; set; }

        public string Decscription { get; set; }

        public Guid TransferId { get; set; }
    
    }
}
