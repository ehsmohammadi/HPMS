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
    //  [Interceptor(typeof(Interception))]
    public class InquiryServiceFacade : IInquiryServiceFacade
    {
        private readonly IInquiryService inquiryService;
        private readonly IMapper<InquirySubjectWithJobPosition, InquirySubjectDTO> inquiySubjectMapper;
        private readonly IJobIndexRepository jobIndexRep;


        public InquiryServiceFacade(IInquiryService inquiryService,
            IMapper<InquirySubjectWithJobPosition, InquirySubjectDTO> inquiySubjectMapper,
            IJobIndexRepository jobIndexRep)
        {
            this.inquiryService = inquiryService;
            this.inquiySubjectMapper = inquiySubjectMapper;
            this.jobIndexRep = jobIndexRep;
        }


        public List<InquirySubjectDTO> GetInquirerInquirySubjects(long periodId, string inquirerEmployeeNo)
        {
            List<InquirySubjectWithJobPosition> inquirySubjects =
                 inquiryService.GetInquirySubjects(new EmployeeId(inquirerEmployeeNo, new PeriodId(periodId)));
            return inquirySubjects.Select(i => inquiySubjectMapper.MapToModel(i)).ToList();
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

            // TODO:  Mapper and Domain Report Needed
            var inquiryForm = new InquiryFormDTO
                {
                    InquirerEmployeeNo = inquirerEmployeeNo,
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
