using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class CalculationStateWithRunSummaryDTO 
    {

        private string stateName;
        public string StateName 
        { 
            get { return stateName; }
            set { this.SetField(p => p.StateName, ref stateName, value); }
        }

        private int percent;
        public int Percent
        {
            get { return percent; }
            set { this.SetField(p => p.Percent, ref percent, value); }
        }

        private List<string> messageList;
        public List<string> MessageList
        {
            get { return messageList; }
            set { this.SetField(p => p.MessageList, ref messageList, value); }
        }

    }
}
