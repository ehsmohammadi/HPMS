using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using MITD.PMS.Presentation.Logic;
using MITD.Presentation.UI;


namespace MITD.PMS.Presentation.UI
{

    public partial class MainPage : ViewBase, IMainView
    {
        private bool expanded;
        public MainPage()
        {
            InitializeComponent();
            expanded = outlookbar1.IsExpanded;
            outlookbar1.IsExpanded = !outlookbar1.IsExpanded;
            outlookbar1.IsExpanded = !outlookbar1.IsExpanded;
            ((INotifyCollectionChanged)tabcontrol1.Items).CollectionChanged += (s, e) =>
            {
                if (tabcontrol1.Items.Count == 0)
                {
                  
                    tabcontrol1.Visibility = Visibility.Collapsed;
                }
                else
                {
                    
                    tabcontrol1.Visibility = Visibility.Visible;
                }
            };
        }

        private void OutlookBar_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (outlookbar1.IsExpanded != expanded)
            {
                if (!outlookbar1.IsExpanded)
                {
                    outlookcol.Width = GridLength.Auto;
                    outlookcol.MinWidth = e.NewSize.Width;
                    //splitter1.IsEnabled = false;
                }
                else
                {
                    outlookcol.MinWidth = 200;
                    //splitter1.IsEnabled = true;
                }
                expanded = outlookbar1.IsExpanded;
            }
        }


        public BusyIndicator BusyIndicator
        {
            get
            {
                return BusyInd;
            }
            set
            {
                BusyInd = value;
            }
        }

        public TabControl TabControl
        {
            get
            {
                return tabcontrol1;
            }
            set
            {
                tabcontrol1 = value;
            }
        }

        public DataTemplate TabHeaderTemplate
        {
            get
            {
                return Resources["TabHeaderTemplate"] as DataTemplate;
            }
            set
            {
                Resources["TabHeaderTemplate"] = value;
            }
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            (ViewModel as MainWindowVM).IsShiftPressed = Keyboard.Modifiers == ModifierKeys.Shift;

        }
    }
}
