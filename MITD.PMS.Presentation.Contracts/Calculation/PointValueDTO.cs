using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class PointValueDTO 
    {
      

        private string name;
        public string Name
        {
            get { return name; }
            set { this.SetField(p => p.Name, ref name, value); }
        }

       
        private decimal pointValue;
        public decimal Value
        {
            get { return pointValue; }
            set { this.SetField(p => p.Value, ref pointValue, value); }
        }

       
    }
}
