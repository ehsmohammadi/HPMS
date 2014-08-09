using System.Collections.Generic;
using System.ComponentModel;

namespace MITD.Presentation.UI
{
    public interface ITreeViewItemModel 
    {
        string SelectedValuePath { get; }

        string DisplayValuePath { get; }

        bool IsExpanded { get; set; }

        bool IsSelected { get; set; }

        IEnumerable<ITreeViewItemModel> GetHierarchy();

      
    }
}
