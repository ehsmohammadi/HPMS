using System.Collections.Generic;
using System.Linq;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.JobIndices;
using MITD.PMS.Domain.Model.JobPositions;
using System;
using MITD.PMS.Domain.Model.Jobs;

namespace MITD.PMS.Application.Contracts
{
    public class JobIndexForJob:IJobJobIndex
    {
        public JobIndexForJob(AbstractJobIndexId jobIndexId, bool showforTopLevel, bool showforSameLevel, bool showforLowLevel)
        {
            JobIndexId = jobIndexId;
            ShowforLowLevel = showforLowLevel;
            ShowforTopLevel = showforTopLevel;
            ShowforSameLevel = showforSameLevel;

        }
        public AbstractJobIndexId JobIndexId { get; private set; }
        public bool ShowforTopLevel { get; private set; }
        public bool ShowforSameLevel { get; private set; }
        public bool ShowforLowLevel { get; private set; }
    }
}
