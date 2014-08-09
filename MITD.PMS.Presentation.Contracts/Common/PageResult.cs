using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMS.Presentation.Contracts
{
    public class PageResultDTO<T> where T : class
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<T> Result { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }
}
