using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using MITD.PMS.Presentation.Logic;
using MITD.PMS.Presentation.Logic.Views;
using MITD.Presentation.UI;

namespace MITD.PMS.Presentation.PeriodManagementApp.Views
{
    public partial class UnitInPeriodUnitIndicesView : ViewBase, IUnitInPeriodUnitIndicesView
    {
        public UnitInPeriodUnitIndicesView()
        {
            InitializeComponent();
        }
        public UnitInPeriodUnitIndicesView(UnitInPeriodUnitIndicesVM vm)
        {
            InitializeComponent();
            ViewModel = vm;
        }

        


    }
}
