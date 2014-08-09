using System.Windows;
using System.Windows.Input;

namespace MITD.Presentation.UI
{
    public class ExtendedTreeView : SilverlightTreeView
    {
        protected override DependencyObject GetContainerForItemOverride()
        {
            var childItem = ExtendedTreeViewItem.CreateItemWithBinding();

            childItem.OnHierarchyMouseUp += OnChildItemMouseLeftButtonUp;

            return childItem;
        }

        private void OnChildItemMouseLeftButtonUp(object sender, MouseEventArgs e)
        {
            if (this.OnHierarchyMouseUp != null)
            {
                this.OnHierarchyMouseUp(this, e);
            }
        }

        public event MouseEventHandler OnHierarchyMouseUp;
    }

    public class ExtendedTreeViewItem : SilverlightTreeViewItem
    {
        public ExtendedTreeViewItem()
        {
            this.MouseLeftButtonUp += OnMouseLeftButtonUp;
        }

        void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (this.OnHierarchyMouseUp != null)
            {
                this.OnHierarchyMouseUp(this, e);
            }
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            var childItem = CreateItemWithBinding();

            childItem.MouseLeftButtonUp += OnMouseLeftButtonUp;

            return childItem;
        }

        public static ExtendedTreeViewItem CreateItemWithBinding()
        {
            return new ExtendedTreeViewItem();         
        }

        public event MouseEventHandler OnHierarchyMouseUp;
    }
}
