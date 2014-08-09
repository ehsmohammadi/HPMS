using MITD.Presentation.UI;
using MITD.PMS.Presentation.Logic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Collections.Generic;

namespace MITD.PMS.Presentation.BasicInfoApp.Views
{
    public partial class JobPositionListView : ViewBase, IJobPositionListView,IViewWithContextMenu
    {
        public JobPositionListView()
        {
            InitializeComponent();
        }

        public JobPositionListView(JobPositionListVM vm)
        {
            InitializeComponent();
            ViewModel = vm;
           
        }

        private void drgJobPositionList_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var elements = VisualTreeHelper.FindElementsInHostCoordinates(e.GetPosition(null), this);
            var row = elements.Where(el => el is DataGridRow).Cast<DataGridRow>().FirstOrDefault();
            if (row != null)
            {
                drgJobPositionList.SelectedItem = row.DataContext;
            }
        }

        public void CreateContextMenu(ReadOnlyCollection<DataGridCommandViewModel> jobCommands)
        {
            cmCommands.Items.Clear();
            jobCommands.ToList().ForEach(c =>
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
            get { return new List<DependencyObject> { drgJobPositionList }; }
        }
    
    }
}
