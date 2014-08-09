using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMS.Presentation.Contracts
{
    public class QueryStringConditions
    {
        public string Columns { get; set; }
        public string Filter { get; set; }
        public string SortBy { get; set; }
    }
}
