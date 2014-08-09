using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class IndexInPeriodTreeView : ViewBase, IIndexInPeriodTreeView
    {
        public IndexInPeriodTreeView()
        {
            InitializeComponent();
        }
        public IndexInPeriodTreeView(IndexInPeriodTreeVM vm)
        {
            InitializeComponent();
            ViewModel = vm;
        }
        private void trIndexInPeriodTree_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var elements = VisualTreeHelper.FindElementsInHostCoordinates(e.GetPosition(null), this);
            var item = elements.Where(el => el is SilverlightTreeViewItem).Cast<SilverlightTreeViewItem>().FirstOrDefault();
            if (item != null)
            {
                trIndexInPeriodTree.SelectedItem = item.DataContext;
            }
        }


        public void CreateContextMenu(ReadOnlyCollection<DataGridCommandViewModel> periodCommands)
        {
            cmCommands.Items.Clear();
            periodCommands.ToList().ForEach(c =>
            {
                var converter = new ImageSourceConverter();
                var img = new Image();
                if (c.Icon != null)
                    img.Source = (ImageSource)converter.ConvertFromString(c.Icon);
                var mi = new MenuItem
                {
                    Header = c.CommandViewModel.DisplayName,
                    FontSize = 11,
                    FontWeight = FontWeights.Medium,
                    Command = c.CommandViewModel.Command,
                    Icon = img,
                    Margin = new Thickness(2),
                    Padding = new Thickness(2)
                };
                cmCommands.Items.Add(mi);
            }
            );
        }
    }
}
