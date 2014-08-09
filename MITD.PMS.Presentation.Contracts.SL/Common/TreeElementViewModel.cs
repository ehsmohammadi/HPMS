using System.Collections.Generic;
using System.Collections.ObjectModel;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class TreeElementViewModel<T>:ViewModelBase,ITreeElementViewModel<T>
    {
        private T data;
        public T Data
        {
            get { return data; }
            set { this.SetField(p => p.Data, ref data, value); }
        }

        private ObservableCollection<TreeElementViewModel<T>> childs;
        public ObservableCollection<TreeElementViewModel<T>> Childs
        {
            get { return childs; }
            set { this.SetField(p => p.Childs, ref childs, value); }
        }

        private bool isExpanded;
        public bool IsExpanded
        {
            get { return isExpanded; }
            set { this.SetField(p => p.IsExpanded, ref isExpanded, value); }
        }

        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set { this.SetField(p => p.IsSelected, ref isSelected, value); }
        }

    }
}
