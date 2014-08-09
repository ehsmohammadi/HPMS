using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.Jobs;
using MITD.PMS.Domain.Model.JobIndices;
using MITD.PMS.Domain.Model.JobPositions;
using MITD.PMS.Domain.Model.Units;
using MITD.PMS.Domain.Service;
using MITD.PMSAdmin.Application.Contracts;
using MITD.PMSAdmin.Domain.Model.CustomFieldTypes;
using MITD.PMSAdmin.Domain.Model.Jobs;
using PMSAdminModel = MITD.PMSAdmin.Domain.Model;

namespace MITD.PMS.ACL.PMSAdmin
{
    public class PMSAdminService : IPMSAdminService
    {
        private readonly IUnitService unitService;
        private readonly IJobService  jobService;
        private readonly IJobIndexService jobIndexService;
        private readonly ICustomFieldService customFieldService;
        private readonly IJobPositionService jobPositionService;

        public PMSAdminService(IUnitService unitService, IJobService jobService,
             ICustomFieldService customFieldService,IJobPositionService jobPositionService,
            IJobIndexService jobIndexService
            )
        {
            this.unitService = unitService;
            this.jobService = jobService;
            this.jobIndexService = jobIndexService;
            this.customFieldService = customFieldService;
            this.jobPositionService = jobPositionService;
        }


        public SharedUnit GetSharedUnit(SharedUnitId sharedUnitId)
        {
            var unit = unitService.GetBy(new PMSAdminModel.Units.UnitId(sharedUnitId.Id));
            var sharedUnit = new SharedUnit(new SharedUnitId(unit.Id.Id), unit.Name, unit.DictionaryName);
            return sharedUnit;
        }

        public SharedJobPosition GetSharedJobPosition(SharedJobPositionId sharedJobPositionId)
        {
            var jobPosition = jobPositionService.GetBy(new PMSAdminModel.JobPositions.JobPositionId(sharedJobPositionId.Id));
            var sharedJobPosition = new SharedJobPosition(new SharedJobPositionId(jobPosition.Id.Id), jobPosition.Name, jobPosition.DictionaryName);
            return sharedJobPosition;
        }

        public SharedJobIndex GetSharedJobIndex(SharedJobIndexId jabIndexId)
        {
            var jobIndex = jobIndexService.GetBy(new PMSAdminModel.JobIndices.AbstractJobIndexId(jabIndexId.Id));
            var sharedJob = new SharedJobIndex(new SharedJobIndexId(jobIndex.Id.Id), jobIndex.Name, jobIndex.DictionaryName);
            return sharedJob;
        }

        public List<SharedEmployeeCustomField> GetSharedEmployeeCustomField(List<SharedEmployeeCustomFieldId> customFieldIdList)
        {
            var res = customFieldService.GetBy(customFieldIdList.Select(c => new CustomFieldTypeId(c.Id)).ToList());
            return
                res.Select(
                    r => new SharedEmployeeCustomField(new SharedEmployeeCustomFieldId(r.Id.Id), r.Name, r.DictionaryName, r.MinValue, r.MaxValue))
                   .ToList();
        }

        public SharedJob GetSharedJob(SharedJobId sharedJobId)
        {
            var job = jobService.GetBy(new PMSAdminModel.Jobs.JobId(sharedJobId.Id));
            var sharedJob = new SharedJob(new SharedJobId(job.Id.Id), job.Name, job.DictionaryName);
            return sharedJob;
        }

       
        //public List<SharedJobCustomFieldId> GetSharedCutomFieldListForJob(SharedJobId sharedJobId,
        //                                                                IList<SharedJobCustomFieldId> customFieldIdList)
        //{
        //    var isValid = jobService.IsValidCustomFieldIdList(new PMSAdminModel.Jobs.JobId(sharedJobId.Id),
        //                                                      customFieldIdList.Select(c => new CustomFieldTypeId(c.Id))
        //                                                                       .ToList());
        //    if (!isValid)
        //        throw new ArgumentException("Invalid job customFieldIdList");

        //    var res = customFieldService.GetBy(customFieldIdList.Select(c => new CustomFieldTypeId(c.Id)).ToList());
        //    return
        //        res.Select(
        //            r => new JobCustomField(new SharedJobCustomFieldId(r.Id.Id), r.Name, r.DictionaryName, r.MinValue, r.MaxValue))
        //           .ToList();
        //}

        public List<SharedJobCustomField> GetSharedCutomFieldListForJob(SharedJobId sharedJobId, List<SharedJobCustomFieldId> customFieldIdList)
        {
           var isValid = jobService.IsValidCustomFieldIdList(new PMSAdminModel.Jobs.JobId(sharedJobId.Id),
                                                              customFieldIdList.Select(c => new CustomFieldTypeId(c.Id))
                                                                               .ToList());
           if (!isValid)
               throw new ArgumentException("Invalid job customFieldIdList");

           var res = customFieldService.GetBy(customFieldIdList.Select(c => new CustomFieldTypeId(c.Id)).ToList());
           return
               res.Select(
                   r => new SharedJobCustomField(new SharedJobCustomFieldId(r.Id.Id), r.Name, r.DictionaryName, r.MinValue, r.MaxValue, r.TypeId))
                  .ToList();

        }

      


        public List<SharedJobIndexCustomField> GetSharedCutomFieldListForJobIndex(SharedJobIndexId sharedJobIndexId,
                                                                        IList<SharedJobIndexCustomFieldId> customFieldIdList)
        {
            var isValid = jobIndexService.IsValidCustomFieldIdList(new PMSAdminModel.JobIndices.AbstractJobIndexId(sharedJobIndexId.Id),
                                                              customFieldIdList.Select(c => new CustomFieldTypeId(c.Id))
                                                                               .ToList());
            if (!isValid)
                throw new ArgumentException("Invalid job customFieldIdList");

            var res = customFieldService.GetBy(customFieldIdList.Select(c => new CustomFieldTypeId(c.Id)).ToList());
            return
                res.Select(
                    r => new SharedJobIndexCustomField(new SharedJobIndexCustomFieldId(r.Id.Id), r.Name, r.DictionaryName, r.MinValue, r.MaxValue))
                   .ToList();
        }
    }
}
