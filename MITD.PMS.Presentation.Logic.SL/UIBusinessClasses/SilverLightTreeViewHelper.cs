using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Presentation.Logic
{
    public static class SilverLightTreeViewHelper<T> where T : IChildElement
    {
        public static ObservableCollection<TreeElementViewModel<T>> prepareListForTreeView(List<T> elements,bool expandElements)
        {
            var rootTreeElements = new List<TreeElementViewModel<T>>();
            if (elements != null && elements.Count > 0)
            {
                var parentElements = elements.Where(l => l.ParentId == null);               
                foreach (var treeElement in parentElements.Select(parentElement => new TreeElementViewModel<T> { Data = parentElement }))
                {
                    treeElement.IsExpanded = expandElements;
                    setChildNodes(treeElement, elements);
                    rootTreeElements.Add(treeElement);
                }
            }
            var rsl = new ObservableCollection<TreeElementViewModel<T>>(rootTreeElements);
            return rsl;
        }

        public static ObservableCollection<TreeElementViewModel<T>> prepareListForTreeView(List<T> elements)
        {
            return prepareListForTreeView(elements, true);
        }



        private static void setChildNodes(TreeElementViewModel<T> parent, List<T> elements)
        {
            parent.Childs = new ObservableCollection<TreeElementViewModel<T>>();
            foreach (var element in elements.Where(e => e.ParentId == ((IChildElement)parent.Data).Id))
            {
                var treeElement = new TreeElementViewModel<T> { Data = element };
                parent.Childs.Add(treeElement);
                setChildNodes(treeElement, elements);
            }
        }
    }
}
