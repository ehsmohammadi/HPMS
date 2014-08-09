using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public class CheckBoxListViewModel<T>:ViewModelBase
    {
        private T data;
        public T Data
        {
            get { return data; }
            set { this.SetField(p => p.Data, ref data, value); }
        }

        private bool isChecked;
        public bool IsChecked
        {
            get { return isChecked; }
            set { this.SetField(p => p.IsChecked, ref isChecked, value); }
        }

    }
}
