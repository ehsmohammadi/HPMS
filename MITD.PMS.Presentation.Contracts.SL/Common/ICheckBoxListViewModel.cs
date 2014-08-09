using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MITD.PMS.Presentation.Contracts
{
    public interface ICheckBoxListViewModel<T>
    {
        T Data { get; set; }

        bool IsChecked { get; set; }
    }
}
