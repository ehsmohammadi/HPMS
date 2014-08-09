using MITD.PMS.Presentation.Contracts;
using MITD.Presentation.UI;
using MITD.PMS.Presentation.Logic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using MITD.Core;
using System.Collections.Generic;

namespace MITD.PMS.Presentation.BasicInfoApp.Views
{
    public partial class GridRuleListView : ViewBase, IGridRuleListView,IViewWithContextMenu
    {
        public GridRuleListView()
        {
            InitializeComponent();
            // use getinstance for runtime 
            var currentLocator = ServiceLocator.Current;
            if (currentLocator == null)
                return;      
            LayoutRoot.DataContext = currentLocator.GetInstance<GridRuleListVM>();
            ((GridRuleListVM) LayoutRoot.DataContext).View = this;

          

        }

        private void drgRuleList_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var elements = VisualTreeHelper.FindElementsInHostCoordinates(e.GetPosition(null), this);
            var row = elements.Where(el => el is DataGridRow).Cast<DataGridRow>().FirstOrDefault();
            if (row != null)
            {
                drgRuleList.SelectedItem = row.DataContext;
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
            get { return new List<DependencyObject> { drgRuleList }; }
        }

        #region Dependecy Property

        public static readonly DependencyProperty PolicyProperty =
           DependencyProperty.Register("Policy", typeof(PolicyDTO), typeof(GridRuleListView),
                                       new PropertyMetadata(PolicyPropertyChanged));
        public PolicyDTO Policy
        {
            get
            {
                return (PolicyDTO)GetValue(PolicyProperty);
            }
            set
            {
                SetValue(PolicyProperty, value);
            }
        }

        private static void PolicyPropertyChanged(DependencyObject owner, DependencyPropertyChangedEventArgs e)
        {
            var vm = (owner as GridRuleListView).LayoutRoot.DataContext;
            if (vm == null) return;
            var policy = e.NewValue != null ? e.NewValue as PolicyDTO : null;
            if (policy != null)
                (vm as GridRuleListVM).Load(policy);
        } 

        #endregion


        

    }
}
