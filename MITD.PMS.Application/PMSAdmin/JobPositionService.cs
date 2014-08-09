using System;
using MITD.Domain.Repository;
using MITD.PMSAdmin.Application.Contracts;
using MITD.PMSAdmin.Domain.Model.JobPositions;
using System.Transactions;
using MITD.PMSAdmin.Exceptions;

namespace MITD.PMSAdmin.Application
{
    public class JobPositionService : IJobPositionService
    {
        private readonly IJobPositionRepository jobPositionRep;

        public JobPositionService(IJobPositionRepository jobPositionRep)
        {
            this.jobPositionRep = jobPositionRep;
        }



        public JobPosition AddJobPosition(string name, string dictionaryName)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var id = jobPositionRep.GetNextId();
                    var jobPosition = new JobPosition(id, name, dictionaryName);
                    jobPositionRep.Add(jobPosition);
                    scope.Complete();
                    return jobPosition;

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

        public JobPosition UppdateJobPosition(JobPositionId jobPositionId, string name, string dictionaryName)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var jobPosition = jobPositionRep.GetById(jobPositionId);
                    jobPosition.Update(name, dictionaryName);
                    scope.Complete();
                    return jobPosition;

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

        public void Delete(JobPositionId jobPositionId)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var jobPostion = jobPositionRep.GetById(jobPositionId);
                    jobPositionRep.Delete(jobPostion);
                    scope.Complete();
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

        public JobPosition GetBy(JobPositionId jobPositionId)
        {
            return jobPositionRep.GetById(jobPositionId);
        }
    }
}
