using MITD.Presentation.UI;
using MITD.PMS.Presentation.Logic;
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

namespace MITD.PMS.Presentation.BasicInfoApp.Views
{
    public partial class UnitView : ViewBase, IUnitView
    {
        public UnitView()
        {
            InitializeComponent();
        }

        public UnitView(UnitVM vm)
        {
            InitializeComponent();
            ViewModel = vm;
        }

    }
}
