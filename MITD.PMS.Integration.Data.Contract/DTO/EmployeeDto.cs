using System;

namespace MITD.PMS.Integration.Data.Contract.DTO
{
    public class EmployeeIntegrationDTO
    {
        public String Name { get; set; }

        public String Family { get; set; }

        public String PersonnelCode { get; set; }

        public Guid JobPositionTransferId { get; set; }
    }
}
