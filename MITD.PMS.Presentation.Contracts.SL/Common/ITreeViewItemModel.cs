using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MITD.PMS.Presentation.Contracts
{
    public interface ITreeElementViewModel<T>
    {
        T Data { get; set; }

        ObservableCollection<TreeElementViewModel<T>> Childs { get; set; }

        bool IsExpanded { get; set; }

        bool IsSelected { get; set; }
    }
}
