using System.Collections.Generic;
using System.Linq;
using Castle.Core;
using MITD.Core;
using MITD.Domain.Repository;
using MITD.PMS.Application.Contracts;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Model.Claims;
using MITD.PMS.Presentation.Contracts;
using Omu.ValueInjecter;

namespace MITD.PMS.Interface
{
   // [Interceptor(typeof(Interception))]
    public class PeriodClaimServiceFacade : IPeriodClaimServiceFacade
    {
        private readonly IClaimService claimService;
        private readonly IMapper<Claim, ClaimDTO> claimDTOMapper;
        private readonly IMapper<Claim, ClaimDTOWithAction> claimDTOWithActionsMapper;
        private readonly IMapper<ClaimState, ClaimStateDTO> claimStateDTOMapper;
        private readonly IMapper<ClaimTypeEnum, ClaimTypeDTO> claimTypeDTOMapper;

        private readonly IClaimRepository claimRep;

        public PeriodClaimServiceFacade(IClaimService claimService,
            IMapper<Claim,ClaimDTO> claimDTOMapper,
            IMapper<Claim,ClaimDTOWithAction> claimDTOWithActionsMapper,
            IMapper<ClaimState, ClaimStateDTO> claimStateDTOMapper,
            IMapper<ClaimTypeEnum, ClaimTypeDTO> claimTypeDTOMapper,
            IClaimRepository claimRep)
        {
            this.claimService = claimService;
            this.claimDTOMapper = claimDTOMapper;
            this.claimDTOWithActionsMapper = claimDTOWithActionsMapper;
            this.claimStateDTOMapper = claimStateDTOMapper;
            this.claimTypeDTOMapper = claimTypeDTOMapper;
            this.claimRep = claimRep;
            
        }


        public ClaimDTO AddClaim(long periodId, ClaimDTO claimDto)
        {
            if (ClaimTypeEnum.FromValue<ClaimTypeEnum>(claimDto.ClaimTypeId.ToString()) == null)
                throw new ClaimArgumentException("Claim", "ClaimType");

            var claim = claimService.AddClaim(new PeriodId(claimDto.PeriodId),claimDto.EmployeeNo,claimDto.Title,claimDto.ClaimDate,
                ClaimTypeEnum.FromValue<ClaimTypeEnum>(claimDto.ClaimTypeId.ToString()), claimDto.Request);
            return claimDTOMapper.MapToModel(claim);
        }

        public string DeleteClaim(long periodId, long claimId)
        {
            claimService.DeleteClaim(new ClaimId(claimId));
            return "Claim with Id " + claimId + " Deleted";
        }

        public ClaimDTO GetClaim(long periodId, long claimId)
        {
            var claim = claimRep.GetById(new ClaimId(claimId));
            return claimDTOMapper.MapToModel(claim);
        }

        public PageResultDTO<ClaimDTOWithAction> GetClaimsWithActions(long periodId, string employeeNo, int pageSize, int pageIndex, QueryStringConditions queryStringConditions)
        {
            var fs = new ListFetchStrategy<Claim>(Enums.FetchInUnitOfWorkOption.NoTracking);
            var sortBy = QueryConditionHelper.GetSortByDictionary(queryStringConditions.SortBy);
            foreach (var item in sortBy)
            {
                fs.SortCriterias.Add(new StringSortCriteria<Claim>(item.Key,
                                                                             (item.Value.ToUpper() == "ASC")
                                                                                 ? Enums.SortOrder.Ascending
                                                                               : Enums.SortOrder.Descending));
            }
            fs.OrderBy(x => x.Id);
            fs.WithPaging(pageSize, pageIndex);
            claimRep.FindBy(fs,employeeNo,new PeriodId(periodId) );
            var res = new PageResultDTO<ClaimDTOWithAction>();
            res.InjectFrom(fs.PageCriteria.PageResult);
            res.Result = fs.PageCriteria.PageResult.Result.Select(r => claimDTOWithActionsMapper.MapToModel(r)).ToList();
            return res;
         }

        public PageResultDTO<ClaimDTOWithAction> GetAllClaimsForAdminWithActions(long periodId, int pageSize, int pageIndex,
           QueryStringConditions queryStringCondition)
        {
            var fs = new ListFetchStrategy<Claim>(Enums.FetchInUnitOfWorkOption.NoTracking);
            var sortBy = QueryConditionHelper.GetSortByDictionary(queryStringCondition.SortBy);
            foreach (var item in sortBy)
            {
                fs.SortCriterias.Add(new StringSortCriteria<Claim>(item.Key,
                                                                             (item.Value.ToUpper() == "ASC")
                                                                                 ? Enums.SortOrder.Ascending
                                                                               : Enums.SortOrder.Descending));
            }
            fs.OrderBy(x => x.Id);
            fs.WithPaging(pageSize, pageIndex);
            claimRep.FindBy(fs, new PeriodId(periodId));
            var res = new PageResultDTO<ClaimDTOWithAction>();
            res.InjectFrom(fs.PageCriteria.PageResult);
            res.Result = fs.PageCriteria.PageResult.Result.Select(r => claimDTOWithActionsMapper.MapToModel(r)).ToList();
            return res;
        }

       

        public List<ClaimStateDTO> GetAllClaimStates(long periodId)
        {
            List<ClaimStateDTO> stateList = new List<ClaimStateDTO>();
            foreach (ClaimState state in ClaimState.GetAll<ClaimState>())
            {
                var stateDto = claimStateDTOMapper.MapToModel(state);
                stateList.Add(stateDto);
            }
            return stateList;

        }

        public ClaimDTO ChangeClaimState(long periodId, long id, string message, ClaimStateDTO claimStateDto)
        {
            var claimState = claimStateDTOMapper.MapToEntity(claimStateDto);
            var claim = claimService.ChangeClaimState(new PeriodId(periodId), new ClaimId(id), message, claimState);
            return claimDTOMapper.MapToModel(claim);
        }

        public List<ClaimTypeDTO> GetAllClaimTypes(int periodId)
        {
            List<ClaimTypeDTO> typeList = new List<ClaimTypeDTO>();
            foreach (ClaimTypeEnum t in ClaimTypeEnum.GetAll<ClaimTypeEnum>())
            {
                var typeDto = claimTypeDTOMapper.MapToModel(t);
                typeList.Add(typeDto);
            }
            return typeList;
        }

       
    }
}
