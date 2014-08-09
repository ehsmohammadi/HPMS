using System;
using System.Collections.Generic;
using System.Linq;
using MITD.Domain.Repository;
using MITD.PMS.Application.Contracts;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.Jobs;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Model.JobPositions;
using MITD.PMS.Domain.Model.Units;
using MITD.PMS.Domain.Service;
using System.Transactions;
using MITD.PMS.Exceptions;
using MITD.PMSReport.Domain.Model;


namespace MITD.PMS.Application
{
    public class JobPositionService : IJobPositionService
    {
        private readonly IPeriodRepository periodRep;
        private readonly IJobRepository jobRepository;
        private readonly IUnitRepository unitRep;
        private readonly IPMSAdminService pmsAdminService;
        private readonly IJobPositionInquiryConfiguratorService configuratorService;
        private readonly IEmployeeRepository employeeRepository;
        private readonly IPeriodManagerService periodChecker;
        private readonly IJobPositionRepository jobPositionRep;

        public JobPositionService(IJobPositionRepository jobPositionRep,
                                  IPeriodRepository periodRep,
                                  IJobRepository jobRepository,
                                  IUnitRepository unitRep,
                                  IPMSAdminService pmsAdminService,
                                  IJobPositionInquiryConfiguratorService configuratorService,
                                  IEmployeeRepository employeeRepository,
                                  IPeriodManagerService periodChecker)
        {
            this.periodRep = periodRep;
            this.jobRepository = jobRepository;
            this.unitRep = unitRep;
            this.pmsAdminService = pmsAdminService;
            this.configuratorService = configuratorService;
            this.employeeRepository = employeeRepository;
            this.periodChecker = periodChecker;
            this.jobPositionRep = jobPositionRep;
        }


        public void RemoveJobPosition(PeriodId periodId, SharedJobPositionId sharedJobPositionId)
        {
            try{
                using (var tr = new TransactionScope())
                {
                    var jobPosition = jobPositionRep.GetBy(new JobPositionId(periodId, sharedJobPositionId));
                    jobPositionRep.DeleteJobPosition(jobPosition);
                    tr.Complete();
                }
            }
            catch (Exception exp)
            {
                var res = jobPositionRep.TryConvertException(exp);
                if (res == null)
                    throw;
                throw res;
            }


        }

        public JobPosition AssignJobPosition(PeriodId periodId, SharedJobPositionId sharedJobPositionId,
            SharedJobPositionId parentSharedJobPositionId, JobId jobId, UnitId unitId)
        {
            using (var tr = new TransactionScope())
            {
                var period = periodRep.GetById(periodId);
                var sharedJobPosition = pmsAdminService.GetSharedJobPosition(sharedJobPositionId);
                var job = jobRepository.GetById(jobId);
                var unit = unitRep.GetBy(unitId);

                JobPosition parent = null;
                if (parentSharedJobPositionId != null)
                    parent = jobPositionRep.GetBy(new JobPositionId(periodId, parentSharedJobPositionId));

                var jobPosition = new JobPosition(period, sharedJobPosition, parent, job, unit);
                jobPositionRep.Add(jobPosition);
                tr.Complete();
                return jobPosition;

            }
        }

        public List<JobPositionInquiryConfigurationItem> GetInquirySubjectWithInquirer(JobPositionId jobPositionId)
        {
            using (var tr = new TransactionScope())
            {
                var jobPosition = jobPositionRep.GetBy(jobPositionId);
                jobPosition.ConfigeInquirer(configuratorService, false);
                tr.Complete();
                return jobPosition.ConfigurationItemList.ToList();

            }
        }

        public void UpdateInquirers(EmployeeId inquirySubjectEmployeeId, JobPositionId jobPositionId, List<EmployeeIdWithJobPositionId> inquirerEmployeeIdList)
        {
            using (var tr = new TransactionScope())
            {
                var jobPosition = jobPositionRep.GetBy(jobPositionId);
                var inquirySubject = employeeRepository.GetBy(inquirySubjectEmployeeId);
                
                //var inquirers = employeeRepository.FindInList(inquirerEmployeeIdList.Select(i => i.EmployeeNo).ToList(),
                //    jobPositionId.PeriodId);

                jobPosition.UpdateInquirersBy(inquirySubject, inquirerEmployeeIdList, periodChecker);

                tr.Complete();
            }

        }

        public JobPosition ConfigureInquiry(JobPositionId jobPositionId)
        {
            using (var tr = new TransactionScope())
            {
                var jobPosition = jobPositionRep.GetBy(jobPositionId);
                jobPosition.ConfigeInquirer(configuratorService, false);
                tr.Complete();
                return jobPosition;
            }
        }

        public List<JobPositionId> GetAllJobPositionId(Period period)
        {
            return jobPositionRep.GetAllJobPositionId(period);
        }

        public JobPosition GetBy(JobPositionId jobPositionId)
        {
            return jobPositionRep.GetBy(jobPositionId);
        }

        public List<JobPosition> GetAllJobPositionByParentId(JobPositionId jobPositionId)
        {
            return jobPositionRep.GetAllJobPositionByParentId(jobPositionId);
        }

        public List<JobPosition> GetAllParentJobPositions(Period sourcePeriod)
        {
            return jobPositionRep.GetAllParentJobPositions(sourcePeriod);
        }
    }
}
