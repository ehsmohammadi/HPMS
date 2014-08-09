using System;
using System.Collections.Generic;
using MITD.Domain.Repository;
using MITD.PMS.Domain.Model.Claims;
using MITD.PMS.Domain.Model.Periods;


namespace MITD.PMS.Domain.Model.Claims
{
    public interface IClaimRepository : IRepository
    {
        IList<Claim> FindBy(ListFetchStrategy<Claim> fs, string employeeNo, PeriodId periodId);
        Claim GetById(ClaimId claimId);
        void Update(Claim claim);
        void Add(Claim claim);
        void Delete(Claim claim);

        ClaimId GetNextId();
        void DeleteAll(Period period);
        bool HasOpenClaim(Period period);
        IList<Claim> FindBy(ListFetchStrategy<Claim> fs, PeriodId periodId);

        Exception ConvertException(Exception exp);
        Exception TryConvertException(Exception exp);
    }
}
