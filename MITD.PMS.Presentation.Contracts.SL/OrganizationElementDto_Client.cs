using System.Collections.Generic;
using System.Linq;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class OrganizationElementDto : ViewModelBase,ITreeViewItemModel
    {
        public string SelectedValuePath
        {
            get { return Title; }
        }

        public string DisplayValuePath
        {
            get { return Title; }
        }

        private bool isExpanded;

        public bool IsExpanded
        {
            get { return isExpanded; }
            set
            {
                isExpanded = value;
                OnPropertyChanged("IsExpanded");
            }
        }

        private bool isSelected;

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        public IEnumerable<ITreeViewItemModel> GetHierarchy()
        {
            return GetAscendingHierarchy().Reverse().Cast<ITreeViewItemModel>();
        }

        public IEnumerable<ITreeViewItemModel> GetChildren()
        {
            if (this.ChildNodes != null)
            {
                return this.ChildNodes.Cast<ITreeViewItemModel>();
            }

            return null;
        }

        private IEnumerable<ITreeViewItemModel> GetAscendingHierarchy()
        {
            var vm = this;
            var rsl = new List<ITreeViewItemModel> {vm};
            return rsl;

            //yield return vm;
            //while (vm.Parent != null)
            //{
            //    yield return vm.Parent;
            //    vm = vm.Parent;
            //}
        }
    }


}
