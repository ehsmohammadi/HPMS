using System.Collections.Generic;
using System.Linq;
using Castle.Core;
using MITD.Core;
using MITD.Domain.Repository;
using MITD.PMS.Presentation.Contracts;
using MITD.PMSAdmin.Application.Contracts;
using MITD.PMSAdmin.Domain.Model.Policies;
using MITD.PMSSecurity.Domain;
using Omu.ValueInjecter;

namespace MITD.PMS.Interface
{
   [Interceptor(typeof(Interception))]
    public class PolicyFacadeService : IPolicyFacadeService
    {
        #region Fields

        private readonly IMapper<Policy, PolicyDTOWithActions> policyWithActionMapper;
        private readonly IMapper<Policy, PolicyDTO> policyMapper;
        private readonly IPolicyService policyService;
        private readonly IPolicyRepository policyRep; 

        #endregion

        #region Ctor
        public PolicyFacadeService(IMapper<Policy, PolicyDTOWithActions> policyWithActionMapper,
                                        IMapper<Policy, PolicyDTO> policyMapper,
                                        IPolicyService policyService,
                                        IPolicyRepository policyRep)
        {
            this.policyWithActionMapper = policyWithActionMapper;
            this.policyMapper = policyMapper;
            this.policyService = policyService;
            this.policyRep = policyRep;
        } 
        #endregion

        #region Public methods

        [RequiredPermission(ActionType.ManagePolicies)]
        public PageResultDTO<PolicyDTOWithActions> GetAllPolicies(int pageSize, int pageIndex)
        {

            var fs = new ListFetchStrategy<Policy>(Enums.FetchInUnitOfWorkOption.NoTracking);
            fs.OrderBy(x => x.Id);
            fs.WithPaging(pageSize, pageIndex);
            policyRep.FindBy(fs);
            var res = new PageResultDTO<PolicyDTOWithActions>();
            res.InjectFrom(fs.PageCriteria.PageResult);
            res.Result = fs.PageCriteria.PageResult.Result.Select(r => policyWithActionMapper.MapToModel(r)).ToList();
            return res;
        }

        [RequiredPermission(ActionType.ManagePolicies)]
        public List<PolicyDTOWithActions> GetAllPolicies()
        {
            return policyRep.GetAll().Select(r => policyWithActionMapper.MapToModel(r)).ToList();
        }

        [RequiredPermission(ActionType.AddPolicy)]
        public PolicyDTO AddPolicy(PolicyDTO dto)
        {
            var res = policyService.AddPolicy(dto.Name, dto.DictionaryName);
            return policyMapper.MapToModel(res);
        }

        [RequiredPermission(ActionType.ModifyPolicy)]
        public PolicyDTO UpdatePolicy(PolicyDTO dto)
        {

            var res = policyService.UpdatePolicy(new PolicyId(dto.Id), dto.Name, dto.DictionaryName);
            return policyMapper.MapToModel(res);
        }

        [RequiredPermission(ActionType.DeletePolicy)]
        public string DeletePolicy(long id)
        {
            policyService.DeletePolicy(new PolicyId(id));
            return "Policy with" + " " + id + "Deleted";
        }

        public PolicyDTO GetPolicyById(long id)
        {
            var policy = policyRep.GetById(new PolicyId(id));
            return policyMapper.MapToModel(policy);
        } 

        #endregion

    }
}
