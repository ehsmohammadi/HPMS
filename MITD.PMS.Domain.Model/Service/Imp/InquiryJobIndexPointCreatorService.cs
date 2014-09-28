using System.Linq;
using MITD.Core;
using MITD.PMS.Domain.Model.InquiryJobIndexPoints;
using MITD.PMS.Domain.Model.JobIndices;
using MITD.PMS.Domain.Model.JobPositions;
using MITD.PMS.Domain.Model.Jobs;

namespace MITD.PMS.Domain.Service
{
    public class InquiryJobIndexPointCreatorService : IInquiryJobIndexPointCreatorService
    {
        private readonly IJobPositionRepository jobPositionRep;
        private readonly IJobRepository jobRep;
        private readonly IJobIndexRepository jobIndexRep;
        private readonly IInquiryJobIndexPointRepository inquiryJobIndexPointRep;
        private readonly IEventPublisher publisher;


        public InquiryJobIndexPointCreatorService(IJobPositionRepository jobPositionRep, 
            IJobRepository jobRep,
            IJobIndexRepository jobIndexRep, 
            IInquiryJobIndexPointRepository inquiryJobIndexPointRep,IEventPublisher publisher)
        {
            this.jobPositionRep = jobPositionRep;
            this.jobRep = jobRep;
            this.jobIndexRep = jobIndexRep;
            this.inquiryJobIndexPointRep = inquiryJobIndexPointRep;
            this.publisher = publisher;
        }

        public void Create(JobPositionInquiryConfigurationItemId configurationItemId)
        {
            var jobPosition = jobPositionRep.GetBy(configurationItemId.InquirySubjectJobPositionId);
            var itm = jobPosition.ConfigurationItemList.Single(c => c.Id.Equals(configurationItemId));
            var job = jobRep.GetById(jobPosition.JobId);
            foreach (var jobIndexId in job.JobIndexList)
            {
                //todo: check for configuration 
                var jobIndex = jobIndexRep.GetById(jobIndexId.JobIndexId);
                if ((jobIndex as JobIndex).IsInquireable)
                {
                    var id = inquiryJobIndexPointRep.GetNextId();
                    var inquiryIndexPoint = new InquiryJobIndexPoint(new InquiryJobIndexPointId(id), itm, jobIndex as JobIndex,
                        string.Empty);
                    publisher.Publish(new InquiryJobIndexPointCreated(inquiryIndexPoint));

                }

            }
        }
    }
}
