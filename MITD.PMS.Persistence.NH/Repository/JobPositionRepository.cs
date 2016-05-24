using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MITD.Data.NH;
using MITD.Domain.Repository;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Model.JobPositions;
using NHibernate.Linq;
using MITD.PMS.Common;

namespace MITD.PMS.Persistence.NH
{
    public class JobPositionRepository : NHRepository, IJobPositionRepository
    {
        private NHRepository<JobPosition> rep;

        public JobPositionRepository(NHUnitOfWork jobPositionOfWork)
            : base(jobPositionOfWork)
        {
            init();
        }

        public JobPositionRepository(IUnitOfWorkScope jobPositionOfWorkScope)
            : base(jobPositionOfWorkScope)
        {
            init();
        }

        private void init()
        {
            rep = new NHRepository<JobPosition>(unitOfWork);
        }

        public List<JobPosition> GetJobPositions(PeriodId periodId)
        {
            return rep.Find(u => u.Id.PeriodId == periodId).ToList();
        }

        public void Add(JobPosition jobPosition)
        {
            rep.Add(jobPosition);
        }

        public void DeleteJobPosition(JobPosition jobPosition)
        {
            rep.Delete(jobPosition);
        }

        public JobPosition GetBy(JobPositionId jobPositionId)
        {
            return rep.Single(u => u.Id.PeriodId == jobPositionId.PeriodId && u.Id.SharedJobPositionId == jobPositionId.SharedJobPositionId);
        }

        public List<JobPosition> Find(Expression<Func<JobPosition, bool>> predicate)
        {
            return rep.Find(predicate).ToList();
        }

        public List<JobPosition> GetAllInquirySubjectJobPositions(EmployeeId inquirerId)
        {
            return rep.Find(j => j.ConfigurationItemList.Any(jc => jc.Id.InquirerId == inquirerId)).ToList();

        }

        public List<JobPositionId> GetAllJobPositionId(Period period)
        {
            return rep.GetQuery().Where(j => j.Id.PeriodId.Id == period.Id.Id).Select(j => j.Id).ToList();
        }

        public List<JobPosition> GetAllJobPositionByParentId(JobPositionId jobPositionId)
        {
            return rep.Find(j => j.Parent.Id == jobPositionId).ToList();
        }

        public List<JobPosition> GetAllParentJobPositions(Period sourcePeriod)
        {
            return rep.Find(j => j.Id.PeriodId == sourcePeriod.Id && j.Parent == null).ToList();
        }

        public void DeleteAllJobPositionConfigurations(Period period)
        {
            session.CreateSQLQuery("Delete from Inquiry_JobIndexPoints where PeriodId=:id")
                .SetParameter("id", period.Id.Id).ExecuteUpdate();

            session.CreateSQLQuery("Delete from JobPostion_InquiryConfigurationItems where PeriodId=:id")
                .SetParameter("id", period.Id.Id).ExecuteUpdate();
        }

        public Exception ConvertException(Exception exp)
        {
            string typeName = "";
            string keyName = "";
            if (NHExceptionDetector.IsDublicateException(exp, out typeName, out keyName))
                return new JobPositionDuplicateException(typeName, keyName);
            if (NHExceptionDetector.IsDeleteHasRelatedDataException(exp, out typeName, out keyName))
                return new JobPositionDeleteException(typeName, keyName);
            throw new Exception();
        }

        public Exception TryConvertException(Exception exp)
        {
            Exception res = null;
            try
            {
                res = ConvertException(exp);
            }
            catch (Exception e)
            {

            }
            return res;
        }
    }
}
