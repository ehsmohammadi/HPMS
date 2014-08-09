using System.Collections.Generic;
using System.Collections.ObjectModel;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class EmployeeJobPositionAssignmentDTO:ViewModelBase
    {
        private ObservableCollection<CustomFieldValueDTO> customFieldValueList;
        public ObservableCollection<CustomFieldValueDTO> CustomFieldValueList
        {
            get { return customFieldValueList; }
            set { this.SetField(p => p.CustomFieldValueList, ref customFieldValueList, value); }
        }
      
    }
}
