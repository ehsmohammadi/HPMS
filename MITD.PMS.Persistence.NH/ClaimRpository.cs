using System;
using System.Collections.Generic;
using System.Linq;
using MITD.Data.NH;
using MITD.Domain.Repository;
using MITD.PMS.Common;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Model.Claims;

namespace MITD.PMS.Persistence.NH
{
    public class ClaimRpository:NHRepository,IClaimRepository
    {
        private NHRepository<Claim> rep; 

        public ClaimRpository(NHUnitOfWork unitOfWork) : base(unitOfWork)
        {
            init();
        }

        public ClaimRpository(IUnitOfWorkScope unitOfWorkScope) : base(unitOfWorkScope)
        {
            init();
        }

        private void init()
        {
            rep=new NHRepository<Claim>(unitOfWork);
        }


        public IList<Claim> FindBy(ListFetchStrategy<Claim> fs, string employeeNo, PeriodId periodId)
        {
            return rep.Find(c => c.PeriodId == periodId && c.EmployeeNo == employeeNo, fs);
        }

        public IList<Claim> FindBy(ListFetchStrategy<Claim> fs, PeriodId periodId)
        {
            return rep.Find(c => c.PeriodId == periodId, fs);
        }

        public Claim GetById(ClaimId claimId)
        {
            return rep.FindByKey(claimId);
        }

        public void Update(Claim claim)
        {
            rep.Update(claim);
        }

        public void Add(Claim claim)
        {
            rep.Add(claim);
        }

        public void Delete(Claim claim)
        {
            rep.Delete(claim);
        }

        public ClaimId GetNextId()
        {
            var id = session.CreateSQLQuery("select next value for [dbo].[Periods_ClaimsSeq]").UniqueResult<long>();
            return new ClaimId(id);
        }

        public void DeleteAll(Period period)
        {
            session.CreateQuery("Delete from Claim o where o.PeriodId = :id")
                .SetParameter("id", period.Id).ExecuteUpdate();
        }

        public bool HasOpenClaim(Period period)
        {
            return rep.GetQuery().Any(p => p.PeriodId == period.Id && p.State == ClaimState.Opened);
        }

        public Exception ConvertException(Exception exp)
        {
            string typeName = "";
            string keyName = "";
            if (NHExceptionDetector.IsDublicateException(exp, out typeName, out keyName))
                return new ClaimDuplicateException("Claim", "");
            if (NHExceptionDetector.IsDeleteHasRelatedDataException(exp, out typeName, out keyName))
                return new ClaimDeleteException("Claim", "Id");
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
