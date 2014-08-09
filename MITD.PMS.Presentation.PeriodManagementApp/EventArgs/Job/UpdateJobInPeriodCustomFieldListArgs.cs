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
    public class UpdateJobInPeriodCustomFieldListArgs
    {
        public UpdateJobInPeriodCustomFieldListArgs(List<AbstractCustomFieldDescriptionDTO> jobInPeriodCustomFieldDescriptionList,long periodId,long jobId)
        {
            JobInPeriodCustomFieldDescriptionList = jobInPeriodCustomFieldDescriptionList;
            PeriodId = periodId;
            JobId = jobId;
        }
        public List<AbstractCustomFieldDescriptionDTO> JobInPeriodCustomFieldDescriptionList
        {
            get; private set; 
        }

        public long PeriodId
        {
            get;
            private set;
        }

        public long JobId
        {
            get;
            private set;
        }
    }
}
