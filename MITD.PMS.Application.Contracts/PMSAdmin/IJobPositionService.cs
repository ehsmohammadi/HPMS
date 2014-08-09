using MITD.Core;
using MITD.PMSAdmin.Domain.Model.JobPositions;

namespace MITD.PMSAdmin.Application.Contracts
{
    public interface IJobPositionService:IService
    { 

        JobPosition AddJobPosition(string name, string dictionaryName);
        JobPosition UppdateJobPosition(JobPositionId jobPositionId, string name, string dictionaryName);
        void Delete(JobPositionId jobPositionId);
        JobPosition GetBy(JobPositionId jobPositionId);
    }
}
