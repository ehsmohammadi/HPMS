using System.Collections.Generic;
using System.Linq;
using Castle.Core;
using MITD.Core;
using MITD.PMS.Application.Contracts;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.InquiryJobIndexPoints;
using MITD.PMS.Domain.Model.JobIndices;
using MITD.PMS.Domain.Model.JobPositions;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Presentation.Contracts;
using MITD.PMSReport.Domain.Model;
using MITD.PMSSecurity.Domain;
using MITD.PMSSecurity.Domain.Model;

namespace MITD.PMS.Interface
{
    [Interceptor(typeof(Interception))]
    public class InquiryServiceFacade : IInquiryServiceFacade
    {
        private readonly IInquiryService inquiryService;
        private readonly IMapper<InquirySubjectWithJobPosition, InquirySubjectDTO> inquiySubjectMapper;
        private readonly IJobIndexRepository jobIndexRep;
        private readonly IJobPositionRepository jobPositionRepository;
        private readonly IEmployeeRepository employeeRepository;


        public InquiryServiceFacade(IInquiryService inquiryService,
            IMapper<InquirySubjectWithJobPosition, InquirySubjectDTO> inquiySubjectMapper,
            IJobIndexRepository jobIndexRep,IJobPositionRepository jobPositionRepository,IEmployeeRepository employeeRepository)
        {
            this.inquiryService = inquiryService;
            this.inquiySubjectMapper = inquiySubjectMapper;
            this.jobIndexRep = jobIndexRep;
            this.jobPositionRepository = jobPositionRepository;
            this.employeeRepository = employeeRepository;
        }

        [RequiredPermission(ActionType.ShowEmployeeInquiry)]
        public List<InquirySubjectDTO> GetInquirerInquirySubjects(long periodId, string inquirerEmployeeNo)
        {
            List<InquirySubjectWithJobPosition> inquirySubjects =
                 inquiryService.GetInquirySubjects(new EmployeeId(inquirerEmployeeNo, new PeriodId(periodId)));
            return inquirySubjects.Select(i => inquiySubjectMapper.MapToModel(i)).ToList();
        }

        public List<InquiryIndexDTO> GetInquirerInquiryIndices(long periodId, string inquirerEmployeeNo)
        {
            List<JobIndex> inquiryIndices =
                 inquiryService.GetInquiryIndices(new EmployeeId(inquirerEmployeeNo, new PeriodId(periodId)));
            return inquiryIndices.Select(i => new InquiryIndexDTO
            {
                JobIndexId = i.Id.Id,
                JobIndexName = i.Name,
                ActionCodes = new List<int>
                {
                    (int) ActionType.FillInquiryForm
                }
            }).ToList();
        }

        [RequiredPermission(ActionType.FillInquiryForm)]
        public InquiryFormDTO GetInquiryForm(long periodId,long inquirerJobPositionId, string inquirerEmployeeNo, string inquirySubjectEmployeeNo,
            long jobPositionId)
        {
            List<InquiryJobIndexPoint> inquryJobIndexPoints =
                inquiryService.GetAllInquiryJobIndexPointBy(new JobPositionInquiryConfigurationItemId(
                    new JobPositionId(new PeriodId(periodId), new SharedJobPositionId(inquirerJobPositionId)),
                    new EmployeeId(inquirerEmployeeNo, new PeriodId(periodId)),
                    new JobPositionId(new PeriodId(periodId), new SharedJobPositionId(jobPositionId)),
                    new EmployeeId(inquirySubjectEmployeeNo, new PeriodId(periodId))));

            // TODO:(LOW)Mapper and Domain Report Needed
            var inquiryForm = new InquiryFormDTO
                {
                    InquirerEmployeeNo = inquirerEmployeeNo,
                    InquirerJobPositionId = inquirerJobPositionId,
                    JobPositionId = jobPositionId,

                    PeriodId = periodId,
                    InquirySubjectEmployeeNo = inquirySubjectEmployeeNo,
                };
            var inquiryJobIndexValueList = new List<JobIndexValueDTO>();
            foreach (var inquiryJobIndexPoint in inquryJobIndexPoints)
            {
                var abstractJobIndex = jobIndexRep.GetById(inquiryJobIndexPoint.JobIndexId);
                var jobIndex = abstractJobIndex as JobIndex;
                if (jobIndex != null && jobIndex.IsInquireable)
                {
                    inquiryJobIndexValueList.Add(new JobIndexValueDTO
                    {
                        Id = inquiryJobIndexPoint.Id.Id,
                        IndexValue = inquiryJobIndexPoint.JobIndexValue,
                        JobIndexId = inquiryJobIndexPoint.JobIndexId.Id,
                        JobIndexName = (jobIndex).Name
                    });
                }
            }

            inquiryForm.JobIndexValueList = inquiryJobIndexValueList;

            return inquiryForm;
        }

        public InquiryFormByIndexDTO GetInquiryFormByIndex(long periodId, string inquirerEmployeeNo, long jobIndexId)
        {
            List<InquiryJobIndexPoint> inquryJobIndexPoints =
                inquiryService.GetAllInquiryJobIndexPointByIndex(new PeriodId(periodId), new EmployeeId(inquirerEmployeeNo, new PeriodId(periodId)),new AbstractJobIndexId(jobIndexId));
            var inquiryForm = new InquiryFormByIndexDTO
            {
                PeriodId = periodId,
                JobIndexId = jobIndexId,
            };
            inquiryForm.EmployeeValueList=new List<EmployeeValueDTO>();
            foreach (var inquiryJobIndexPoint in inquryJobIndexPoints)
            {
                var inquirerJobPositionId = inquiryJobIndexPoint.ConfigurationItemId.InquirerJobPositionId;
                var inquirerJobposition = jobPositionRepository.GetBy(inquirerJobPositionId);
                var inquirySubjectId = inquiryJobIndexPoint.ConfigurationItemId.InquirySubjectId;
                var inquirySubject = employeeRepository.GetBy(inquirySubjectId);
                var inquirySubjectJobpositionId = inquiryJobIndexPoint.ConfigurationItemId.InquirySubjectJobPositionId;
                var inquirySubjectJobposition=jobPositionRepository.GetBy(inquirySubjectJobpositionId);
                inquiryForm.EmployeeValueList.Add(new EmployeeValueDTO
                {
                     InquireEmployeeNo = inquiryJobIndexPoint.ConfigurationItemId.InquirerId.EmployeeNo,
                     InquirerJobPositionId = inquirerJobPositionId.SharedJobPositionId.Id,
                     InquirerJobPositionName = inquirerJobposition.Name,
                     EmployeeNo = inquirySubject.Id.EmployeeNo,
                     FullName = inquirySubject.FirstName+" "+inquirySubject.LastName,
                     JobPositionId = inquirySubjectJobpositionId.SharedJobPositionId.Id,
                     JobPositionName =inquirySubjectJobposition.Name,
                     IndexValue = inquiryJobIndexPoint.JobIndexValue
                });
            }
            return inquiryForm;
        }

        [RequiredPermission(ActionType.FillInquiryForm)]
        public InquiryFormDTO UpdateInquirySubjectForm(InquiryFormDTO inquiryForm)
        {
            inquiryService.UpdateInquiryJobIndexPoints(
                inquiryForm.JobIndexValueList.Select(
                    j => new InquiryJobIndexPoinItem(new JobPositionInquiryConfigurationItemId(
                         new JobPositionId(new PeriodId(inquiryForm.PeriodId),
                            new SharedJobPositionId(inquiryForm.InquirerJobPositionId)),
                        new EmployeeId(inquiryForm.InquirerEmployeeNo, new PeriodId(inquiryForm.PeriodId)),
                        new JobPositionId(new PeriodId(inquiryForm.PeriodId),
                            new SharedJobPositionId(inquiryForm.JobPositionId)),
                        new EmployeeId(inquiryForm.InquirySubjectEmployeeNo, new PeriodId(inquiryForm.PeriodId))),
                        new AbstractJobIndexId(j.JobIndexId), j.IndexValue)));

            return inquiryForm;
        }
    }
}
