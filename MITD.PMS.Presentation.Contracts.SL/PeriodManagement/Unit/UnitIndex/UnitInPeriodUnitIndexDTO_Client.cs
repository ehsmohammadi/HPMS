using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
     public partial class UnitInPeriodUnitIndexDTO:ViewModelBase
    {
        private bool isChecked;
        public bool IsChecked
        {
            get { return isChecked; }
            set { this.SetField(p => p.IsChecked, ref isChecked, value); }
        }
    }
}
