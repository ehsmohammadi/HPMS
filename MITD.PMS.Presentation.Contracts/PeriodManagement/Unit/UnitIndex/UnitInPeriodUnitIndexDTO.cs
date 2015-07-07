using System;
using System.ComponentModel.DataAnnotations;
using MITD.Presentation;
using System.Collections.Generic;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class UnitInPeriodUnitIndexDTO
    {
        private long id;
        [Range(1, Int64.MaxValue, ErrorMessage = "انتخاب شاخص الزامی است")]
        public long Id
        {
            get { return id; }
            set
            {
                this.SetField(p => p.Id, ref id, value);
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { this.SetField(p => p.Name, ref name, value); }
        }

        private bool isInquireable;
        public bool IsInquireable
        {
            get { return isInquireable; }
            set
            {
                this.SetField(p => p.IsInquireable, ref isInquireable, value);
            }
        }

        private bool showforTopLevel;
        public bool ShowforTopLevel
        {
            get { return showforTopLevel; }
            set
            {
                this.SetField(p => p.ShowforTopLevel, ref showforTopLevel, value);
            }
        }

        private bool showforLowLevel;
        public bool ShowforLowLevel
        {
            get { return showforLowLevel; }
            set
            {
                this.SetField(p => p.ShowforLowLevel, ref showforLowLevel, value);
            }
        }

        private bool showforSameLevel;
        public bool ShowforSameLevel
        {
            get { return showforSameLevel; }
            set
            {
                this.SetField(p => p.ShowforSameLevel, ref showforSameLevel, value);
            }
        }


    }
}
