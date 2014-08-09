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
using MITD.Presentation.UI;

namespace MITD.PMS.Presentation.PeriodManagementApp.Views
{
    public partial class PeriodView : ViewBase,IPeriodView
    {
        public PeriodView()
        {
            InitializeComponent();
        }
        public PeriodView(PeriodVM vm)
        {
            InitializeComponent();
            ViewModel = vm;
        }

        private void drgPeriodList_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {

        }


    }
}
