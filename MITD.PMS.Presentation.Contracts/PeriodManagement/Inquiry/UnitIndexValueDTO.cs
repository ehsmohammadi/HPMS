
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class UnitIndexValueDTO
    {
        
        private long id;
        public long Id
        {
            get { return id; }
            set { this.SetField(p => p.Id, ref id, value); }
        }


        private long unitIndexId;
        public long UnitIndexId
        {
            get { return unitIndexId; }
            set { this.SetField(p => p.UnitIndexId, ref unitIndexId, value); }
        }

        private string unitIndexName;
        public string UnitIndexName
        {
            get { return unitIndexName; }
            set { this.SetField(p => p.UnitIndexName, ref unitIndexName, value); }
        }

        private string indexValue;
        [Range(0,9,ErrorMessage="برای فیلد مورد نظر مقدار درست وارد کنید")]
        public string IndexValue
        {
            get { return indexValue; }
            set { this.SetField(p => p.IndexValue, ref indexValue, value); }
        }


       
    }
}
