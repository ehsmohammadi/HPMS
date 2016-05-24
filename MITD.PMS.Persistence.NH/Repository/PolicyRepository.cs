using MITD.Data.NH;
using MITD.Domain.Repository;
using MITD.PMS.Common;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Model.Policies;
using MITD.PMS.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMS.Persistence.NH
{
    public class PolicyRepository : NHRepository, IPolicyRepository
    {
        private NHRepository<Policy> rep;
        private IPolicyConfigurator policyConfigurator;

        public PolicyRepository(NHUnitOfWork unitOfWork, IPolicyConfigurator policyConfigurator)
            : base(unitOfWork)
        {
            init(policyConfigurator);
        }

        public PolicyRepository(IUnitOfWorkScope unitOfWorkScope, IPolicyConfigurator policyConfigurator)
            : base(unitOfWorkScope)
        {
            init(policyConfigurator);
        }

        private void init(IPolicyConfigurator policyConfigurator)
        {
            rep = new NHRepository<Policy>(unitOfWork);
            this.policyConfigurator = policyConfigurator;
        }

        public void Add(Policy policy)
        {
            rep.Add(policy);
        }

        public Policy GetById(PolicyId policyId)
        {
            var policy = rep.FindByKey(policyId);
            policyConfigurator.Config(policy);
            return policy;
        }

        public long GetNextId()
        {
            return session.CreateSQLQuery("select next value for [dbo].[PolicySeq]").UniqueResult<long>();
        }

        public Exception ConvertException(Exception exp)
        {
            string typeName = "";
            string keyName = "";
            if (NHExceptionDetector.IsDublicateException(exp, out typeName, out keyName))
                return new PolicyDuplicateException(typeName, keyName);
            if (NHExceptionDetector.IsDeleteHasRelatedDataException(exp, out typeName, out keyName))
                return new PolicyDeleteException(typeName, keyName);
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
