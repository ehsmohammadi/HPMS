
using System.Collections.Generic;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class InquiryUnitFormDTO
    {
        private long periodId;
        public long PeriodId
        {
            get { return periodId; }
            set { this.SetField(p => p.PeriodId, ref periodId, value); }
        }



        private string inquirerEmployeeNo;
        public string InquirerEmployeeNo
        {
            get { return inquirerEmployeeNo; }
            set { this.SetField(p => p.InquirerEmployeeNo, ref inquirerEmployeeNo, value); }
        }

     
     

        private long inquiryUnitId;
        public long InquiryUnitId
        {
            get { return inquiryUnitId; }
            set { this.SetField(p => p.InquiryUnitId, ref inquiryUnitId, value); }
        }

        private string _unitName;
        public string UnitName
        {
            get { return _unitName; }
            set { this.SetField(p => p.UnitName, ref _unitName, value); }
        }


        private List<UnitIndexValueDTO> unitIndexValueList;
        public List<UnitIndexValueDTO> UnitIndexValueList
        {
            get { return unitIndexValueList; }
            set { this.SetField(p => p.UnitIndexValueList, ref unitIndexValueList, value); }
        }

       
    }
}
