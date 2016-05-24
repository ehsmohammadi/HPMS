using System;
using System.Collections.Generic;
using System.Linq;
using MITD.Data.NH;
using MITD.Domain.Repository;
using MITD.PMS.Common;
using MITD.PMSAdmin.Domain.Model.JobPositions;

namespace MITD.PMSAdmin.Persistence.NH
{
    public class JobPositionRepository : NHRepository, IJobPositionRepository
    {
        private NHRepository<JobPosition> rep;
 
        public JobPositionRepository(NHUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            init();
        }

        public JobPositionRepository(IUnitOfWorkScope unitOfWorkScope)
            : base(unitOfWorkScope)
        {
            init();
        }

        private void init()
        {
            rep=new NHRepository<JobPosition>(unitOfWork);
        }

        public void FindBy( ListFetchStrategy<JobPosition> fs)
        {
                rep.GetAll(fs);
           

        }

        public void Add(JobPosition jobPosition)
        {
            rep.Add(jobPosition);
        }

        public void UpdateJobPosition(JobPosition jobPosition)
        {
           rep.Update(jobPosition);
        }

        public JobPosition GetById(JobPositionId jobPositionId)
        {
            return rep.FindByKey(jobPositionId);
            
        }

        public JobPosition GetByTransferId(Guid transferId)
        {
            return rep.Single(j => j.TransferId == transferId);
        }

        public JobPositionId GetNextId()
        {
            using (var ctx = Session.SessionFactory.OpenStatelessSession())
            {
                ctx.BeginTransaction();
                var res = ctx.CreateSQLQuery("Select next value for dbo.JobPositionSeq").UniqueResult<long>();
                return new JobPositionId(res);
            }
        }

        public void Delete(JobPosition jobPostion)
        {
            rep.Delete(jobPostion);
        }

        public List<JobPosition> GetAll()
        {
            return rep.GetAll().ToList();
        }

        public JobPositionException ConvertException(Exception exp)
        {
            string typeName = "";
            string keyName = "";
            if (NHExceptionDetector.IsDublicateException(exp, out typeName, out keyName))
                return new JobPositionDuplicateException("JobPosition", "DictionaryName");
            if (NHExceptionDetector.IsDeleteHasRelatedDataException(exp, out typeName, out keyName))
                return new JobPositionDeleteException("JobPosition", "Id");
            throw new Exception();
        }

        public JobPositionException TryConvertException(Exception exp)
        {
            JobPositionException res = null;
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
