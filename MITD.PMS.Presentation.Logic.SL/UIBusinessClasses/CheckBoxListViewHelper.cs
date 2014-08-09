using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Presentation.Logic
{
    public static class CheckBoxListViewHelper<T>
    {
      public static ObservableCollection<CheckBoxListViewModel<T>> PrepareListForCheckBoxListView(List<T> elements)
      {
          var res = new ObservableCollection<CheckBoxListViewModel<T>>();
          foreach (var element in elements)
          {
              var checkBoxelement = new CheckBoxListViewModel<T> {Data = element,IsChecked = true};
              res.Add(checkBoxelement);            
          }
          return res;
      }
    }
}
