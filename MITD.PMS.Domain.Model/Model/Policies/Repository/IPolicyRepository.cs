using System;
using MITD.Domain.Repository;
using MITD.PMS.Domain.Model.Periods;

namespace MITD.PMS.Domain.Model.Policies
{
    public interface IPolicyRepository : IRepository
    {
        long GetNextId();
        void Add(Policy policy);
        Policy GetById(PolicyId policyId);

        Exception ConvertException(Exception exp);
        Exception TryConvertException(Exception exp);
    }
}
