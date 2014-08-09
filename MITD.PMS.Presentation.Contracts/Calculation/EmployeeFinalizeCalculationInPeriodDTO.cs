using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class EmployeeFinalizeCalculationInPeriodDTO 
    {
        private long periodId;
        public long PeriodId
        {
            get { return periodId; }
            set { this.SetField(p => p.PeriodId, ref periodId, value); }
        }

        private string periodName;
        public string PeriodName
        {
            get { return periodName; }
            set { this.SetField(p => p.PeriodName, ref periodName, value); }
        }

        private long rank;
        public long Rank
        {
            get { return rank; }
            set { this.SetField(p => p.Rank, ref rank, value); }
        }

        private long  score;
        public long Score
        {
            get { return score; }
            set { this.SetField(p => p.Score, ref score, value); }
        }

      
    }
}
