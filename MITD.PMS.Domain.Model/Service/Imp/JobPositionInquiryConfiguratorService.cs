using System.Collections.Generic;
using System.Linq;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.JobPositions;

namespace MITD.PMS.Domain.Service
{
    public class JobPositionInquiryConfiguratorService : IJobPositionInquiryConfiguratorService
    {
        private readonly IJobPositionRepository jobPositionRep;


        public JobPositionInquiryConfiguratorService(IJobPositionRepository jobPositionRep)
        {
            this.jobPositionRep = jobPositionRep;

        }

        public List<JobPositionInquiryConfigurationItem> Configure(JobPosition jobPosition)
        {
            var childs = jobPositionRep.Find(j => j.Parent.Id == jobPosition.Id);
            var inquirersjobPositions = childs.ToDictionary(j => j, u => JobPositionLevel.Childs);
            //360 daraje
            //var siblings = jobPositionRep.Find(j => j.Parent == jobPosition.Parent && j.Id.PeriodId.Id == jobPosition.Id.PeriodId.Id);
            //inquirersjobPositions = inquirersjobPositions.Concat(siblings.ToDictionary(j => j, u => JobPositionLevel.Siblings)).ToDictionary(s => s.Key, s => s.Value);
            //irisl
            inquirersjobPositions.Add(jobPosition,JobPositionLevel.None);
            /////////////////////////////////////////////////////////////////////////////////////////

            if (jobPosition.Parent != null)
                inquirersjobPositions.Add(jobPosition.Parent, JobPositionLevel.Parents);
            /////////////////////////////////////////////////////////////////////////////////////////

            var inquirers = new Dictionary<JobPositionEmployee, JobPositionLevel>();
            foreach (var jp in inquirersjobPositions)
                inquirers = inquirers.Concat(jp.Key.Employees.ToDictionary(e => e, u => jp.Value)).ToDictionary(s => s.Key, s => s.Value);

            var res = new List<JobPositionInquiryConfigurationItem>();
            foreach (var inquirySubject in jobPosition.Employees)
            {
                foreach (var inquirer in inquirers)
                    res.Add(
                        new JobPositionInquiryConfigurationItem(
                            new JobPositionInquiryConfigurationItemId(inquirer.Key.JobPosition.Id, inquirer.Key.EmployeeId, jobPosition.Id,
                                inquirySubject.EmployeeId), jobPosition, true, true, inquirer.Value));
            }

            return res;
        }

        public List<JobPositionInquiryConfigurationItem> GetJobPositionInquiryConfigurationItemBy(Employee inquirer)
        {
            if (inquirer == null)
                return new List<JobPositionInquiryConfigurationItem>();
            var jobpositionInquirySubjects = jobPositionRep.GetAllInquirySubjectJobPositions(inquirer.Id);
            var res = new List<JobPositionInquiryConfigurationItem>();
            foreach (var itm in jobpositionInquirySubjects)
            {
                res.AddRange(itm.ConfigurationItemList.Where(c => c.Id.InquirerId.Equals(inquirer.Id)));
            }
            return res.Distinct().ToList();
        }
    }
}
