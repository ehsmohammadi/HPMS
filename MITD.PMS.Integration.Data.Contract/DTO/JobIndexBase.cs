using System;
using System.Collections.Generic;
using System.Text;


namespace MITD.PMS.Integration.Data.Contract.DTO
{
    public abstract class JobIndexBase
    {
        public string IndexTitle { get; set; }

        public string Description { get; set; }

        public long IndexTypeID { get; set; }

        public int Coefficient { get; set; }
    }
}
