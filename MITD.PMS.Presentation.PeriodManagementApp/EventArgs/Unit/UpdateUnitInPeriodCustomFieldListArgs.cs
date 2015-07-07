using MITD.PMS.Presentation.Contracts;
using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace MITD.PMS.Presentation.PeriodManagementApp
{
    public class UpdateUnitInPeriodCustomFieldListArgs
    {
        public UpdateUnitInPeriodCustomFieldListArgs(List<AbstractCustomFieldDescriptionDTO> UnitInPeriodCustomFieldDescriptionList,long periodId,long UnitId)
        {
           this.UnitInPeriodCustomFieldDescriptionList = UnitInPeriodCustomFieldDescriptionList;
            PeriodId = periodId;
            this. UnitId = UnitId;
        }
        public List<AbstractCustomFieldDescriptionDTO> UnitInPeriodCustomFieldDescriptionList
        {
            get; private set; 
        }

        public long PeriodId
        {
            get;
            private set;
        }

        public long UnitId
        {
            get;
            private set;
        }
    }
}
