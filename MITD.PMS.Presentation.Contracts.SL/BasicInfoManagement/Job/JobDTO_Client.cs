using System.Collections.ObjectModel;
using MITD.Presentation;
using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class JobDTO
    {
        private ObservableCollection<CustomFieldDTO> customFields;
        public ObservableCollection<CustomFieldDTO> CustomFields
        {
            get { return customFields; }
            set { this.SetField(p => p.CustomFields, ref customFields, value); }
        }

    }
}
