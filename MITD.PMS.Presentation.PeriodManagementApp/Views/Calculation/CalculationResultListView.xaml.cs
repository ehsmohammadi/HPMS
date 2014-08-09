using System.Collections.ObjectModel;
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
using System.Windows.Threading;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Presentation.PeriodManagementApp.Views
{
    public partial class CalculationResultListView : ViewBase, ICalculationResultListView,IViewWithContextMenu
    {
        public CalculationResultListView()
        {
            InitializeComponent();
        }

        public CalculationResultListView(CalculationResultListVM vm)
        {
            InitializeComponent();
            ViewModel = vm;

        }

        private void drgList_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var elements = VisualTreeHelper.FindElementsInHostCoordinates(e.GetPosition(null), this);
            var row = elements.Where(el => el is DataGridRow).Cast<DataGridRow>().FirstOrDefault();
            if (row != null)
            {
                drgList.SelectedItem = row.DataContext;
            }
        }

        public void CreateContextMenu(ReadOnlyCollection<DataGridCommandViewModel> commands)
        {
            cmCommands.Items.Clear();
            commands.ToList().ForEach(c =>
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
        public IList<DependencyObject> ItemsWithContextMenu
        {
            get { return new List<DependencyObject> { drgList }; }
        }
        
        private void HideShowDetails(object sender, RoutedEventArgs e)
        {
            Button expandCollapseButton = (Button)sender;
            DataGridRow selectedRow = DataGridRow.GetRowContainingElement(expandCollapseButton);

            if (null != expandCollapseButton && "+" == expandCollapseButton.Content.ToString())
            {
                selectedRow.DetailsVisibility = Visibility.Visible;
                expandCollapseButton.Content = "-";
            }
            else
            {
                selectedRow.DetailsVisibility = Visibility.Collapsed;
                expandCollapseButton.Content = "+";
                if (selectedRow.DataContext is JobPositionValueDTO)
                {
                    var parentRow = selectedRow.FindParentOfType<DataGridRow>();
                    parentRow.DetailsVisibility = Visibility.Collapsed;

                    var timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 0, 0, 200) };
                    timer.Tick += (ts, te) =>
                    {
                        Dispatcher.BeginInvoke(() => parentRow.DetailsVisibility = Visibility.Visible);
                        timer.Stop();
                    };
                    timer.Start();
                }
            }
        }

       
    }
}
