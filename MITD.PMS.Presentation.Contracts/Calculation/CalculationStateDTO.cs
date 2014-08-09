using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class CalculationStateDTO 
    {
        private int state;
        public int State
        {
            get { return state; }
            set { this.SetField(p => p.State, ref state, value); }
        }
    }
}
