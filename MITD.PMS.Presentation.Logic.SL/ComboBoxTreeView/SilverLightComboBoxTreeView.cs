using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MITD.PMS.Presentation.Contracts;

namespace MITD.Presentation.UI
{
    public class ComboBoxTreeView : ComboBoxEx
    {
        private ExtendedTreeView treeView;
        private ContentPresenter contentPresenter;

        public ComboBoxTreeView()
        {
            this.DefaultStyleKey = typeof(ComboBoxTreeView);
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            //For 
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            treeView = (ExtendedTreeView)this.GetTemplateChild("treeView");
            treeView.OnHierarchyMouseUp += new MouseEventHandler(OnTreeViewHierarchyMouseUp);
            contentPresenter = (ContentPresenter)this.GetTemplateChild("ContentPresenter");

            this.SetSelectedItemToHeader();
        }

        protected override void OnDropDownClosed(EventArgs e)
        {
            base.OnDropDownClosed(e);
            this.SelectedItem = treeView.SelectedItem;
            this.SetSelectedItemToHeader();
        }

        protected override void OnDropDownOpened(EventArgs e)
        {
            base.OnDropDownOpened(e);
            this.SetSelectedItemToHeader();
        }


        private void OnTreeViewHierarchyMouseUp(object sender, MouseEventArgs e)
        {
            //This line isn't obligatory because it is executed in the OnDropDownClosed method, but be it so
            this.SelectedItem = treeView.SelectedItem;
            this.IsDropDownOpen = false;
        }


        public new object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public new static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object), typeof(ComboBoxTreeView), new PropertyMetadata(null, new PropertyChangedCallback(OnSelectedItemChanged)));

        private static void OnSelectedItemChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((ComboBoxTreeView)sender).UpdateSelectedItem();
        }

        private void UpdateSelectedItem()
        {
            if (this.SelectedItem is TreeViewItem)
                //I would rather use a correct object instead of TreeViewItem
                this.SelectedItem = ((TreeViewItem) this.SelectedItem).DataContext;
            else
                //Update the selected hierarchy and displays
                this.SetSelectedItemToHeader();
        }

        private void SetSelectedItemToHeader()
        {
            string content = null;

            //var item = this.SelectedItem as TreeElement;
            //if (item != null)
            //{
            //    //Get hierarchy and display it as the selected item
            //    var hierarchy = item.GetHierarchy().Select(i => i.DisplayValuePath).ToArray();
            //    if (hierarchy.Length > 0)
            //    {
            //        content = string.Join(".", hierarchy);
            //    }
            //}

            this.SetContentAsTextBlock(content);
        }

        private void SetContentAsTextBlock(string content)
        {
            if (contentPresenter == null)
            {
                return;
            }

            var tb = contentPresenter.Content as TextBlock;
            if (tb == null)
            {
                contentPresenter.Content = tb = new TextBlock();
            }
            tb.Text = content ?? ' '.ToString();

            contentPresenter.ContentTemplate = null;
        }
    }
}
