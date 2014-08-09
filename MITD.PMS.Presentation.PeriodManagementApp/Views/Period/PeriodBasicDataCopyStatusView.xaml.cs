using MITD.PMS.Presentation.Logic;
using MITD.Presentation.UI;
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

namespace MITD.PMS.Presentation.PeriodManagementApp.Views
{
    public partial class PeriodBasicDataCopyStatusView : ViewBase, IPeriodBasicDataCopyStatusView
    {
        public PeriodBasicDataCopyStatusView()
        {
            InitializeComponent();
        }

        public PeriodBasicDataCopyStatusView(PeriodBasicDataCopyStatusVM vm)
        {
            InitializeComponent();
            ViewModel = vm;

        }

        public void  SetMessagesListScrollToBottom()
        {
            var sc = lbMessages.GetScrollHost();
            if (sc != null)
            {
                lbMessages.GetScrollHost().UpdateLayout();
                lbMessages.GetScrollHost().ScrollToVerticalOffset(double.MaxValue);
            }
        }


    }
}
