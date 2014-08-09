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
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;

namespace MITD.PMS.Presentation.UI
{
    public partial class LoginView : ViewBase, ILoginView
    {
        public LoginView()
        {
            InitializeComponent();
        }

        public LoginView(LoginVM vm)
        {
            InitializeComponent();
            ViewModel = vm;

        }

    }
}
