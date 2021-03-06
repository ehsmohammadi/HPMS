﻿using MITD.PMS.Presentation.Contracts;
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
    public class UpdateJobInPeriodJobIndexListArgs
    {
        public UpdateJobInPeriodJobIndexListArgs(List<JobInPeriodJobIndexDTO> jobInPeriodJobIndices, long periodId, long jobId)
        {
            JobInPeriodJobIndices = jobInPeriodJobIndices;
            PeriodId = periodId;
            JobId = jobId;
        }
        public List<JobInPeriodJobIndexDTO> JobInPeriodJobIndices
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
