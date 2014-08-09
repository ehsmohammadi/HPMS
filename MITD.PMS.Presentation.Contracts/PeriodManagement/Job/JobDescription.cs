
using System.Collections.Generic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class JobDescription
    {
        
        private long id;
        public long Id
        {
            get { return id; }
            set { this.SetField(p => p.Id, ref id, value); }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set { this.SetField(p => p.Title, ref title, value); }
        }
       
    }
}
