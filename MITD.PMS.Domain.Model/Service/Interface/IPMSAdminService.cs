﻿using System.Collections.Generic;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.Jobs;
using MITD.PMS.Domain.Model.JobIndices;
using MITD.PMS.Domain.Model.JobPositions;
using MITD.PMS.Domain.Model.UnitIndices;
using MITD.PMS.Domain.Model.Units;
using MITD.Core;

namespace MITD.PMS.Domain.Service
{
    public interface IPMSAdminService : IService
    {
        SharedUnit GetSharedUnit(SharedUnitId sharedUnitId);
        SharedUnitIndex GetSharedUnitIndex(SharedUnitIndexId sharedUnitIndexId);

        List<SharedUnitIndexCustomField> GetSharedCutomFieldListForUnitIndex(SharedUnitIndexId sharedUnitIndexId,
            IList<SharedUnitIndexCustomFieldId> customFieldIdList);




        SharedJob GetSharedJob(SharedJobId sharedJobId);

        List<SharedUnitCustomField> GetSharedCutomFieldListForUnit(SharedUnitId sharedJobId,
            List<SharedUnitCustomFieldId> customFieldIdList);
        List<SharedJobCustomField> GetSharedCutomFieldListForJob(SharedJobId sharedJobId, List<SharedJobCustomFieldId> customFieldIdList);

        List<SharedJobIndexCustomField> GetSharedCutomFieldListForJobIndex(SharedJobIndexId sharedJobIndexId,IList<SharedJobIndexCustomFieldId>
                                                                               customFieldIdList);
        SharedJobPosition GetSharedJobPosition(SharedJobPositionId sharedJobPositionId);
        SharedJobIndex GetSharedJobIndex(SharedJobIndexId jabIndexId);
        List<SharedEmployeeCustomField> GetSharedEmployeeCustomField(List<SharedEmployeeCustomFieldId> sharedEmployeeCustomFieldIds);
       
    }
}
