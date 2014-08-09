using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.Core;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Presentation.Contracts;
using MITD.PMSAdmin.Domain.Model.CustomFieldTypes;
using MITD.PMS.Domain.Model.Claims;
using MITD.PMSSecurity.Domain;

namespace MITD.PMS.Interface
{
    public class ClaimMapper : BaseMapper<Claim,ClaimDTO> , IMapper<Claim,ClaimDTO>
    {
        public override ClaimDTO MapToModel(Claim entity)
        {
            var res = new ClaimDTO()
                {
                    Id = entity.Id.Id,
                    PeriodId = entity.PeriodId.Id,
                    Title = entity.Title,
                    ClaimDate = entity.ClaimDate,
                    ResponseDate = entity.ResponseDate,
                    //ClaimStateId = entity.ClaimStateId,
                    ClaimTypeName = entity.ClaimTypeId.DisplayName,
                    ClaimTypeId = Convert.ToInt32(entity.ClaimTypeId.Value),
                    EmployeeNo = entity.EmployeeNo,
                    Request = entity.Request,
                    Response = entity.Response,
                    ClaimState = new ClaimStateDTO
                        {
                            Id = int.Parse(entity.State.Value),
                            Name = entity.State.DisplayName,
                        },

                    ClaimType = new ClaimTypeDTO()
                        {
                            Id = int.Parse(entity.ClaimTypeId.Value),
                            Name = entity.ClaimTypeId.DisplayName
                        }
                };
            return res;
        }

        public override Claim MapToEntity(ClaimDTO model)
        {
            throw new Exception("You can not do that ");
        }
    }


    public class ClaimWithActionsMapper : BaseMapper<Claim, ClaimDTOWithAction>, IMapper<Claim, ClaimDTOWithAction>
    {
        public override ClaimDTOWithAction MapToModel(Claim entity)
        {
            var res = new ClaimDTOWithAction()
            {
                Id = entity.Id.Id,
                PeriodId = entity.PeriodId.Id,
                Title = entity.Title,
                ClaimDate = entity.ClaimDate,
                ResponseDate = entity.ResponseDate,
                //ClaimStateId = entity.ClaimStateId,
                ClaimTypeName = entity.ClaimTypeId.DisplayName,
                ClaimTypeId = Convert.ToInt32(entity.ClaimTypeId.Value),
                EmployeeNo = entity.EmployeeNo,
                
                 ClaimState = new ClaimStateDTO
                        {
                            Id = int.Parse(entity.State.Value),
                            Name = entity.State.DisplayName,
                        },

                    ClaimType = new ClaimTypeDTO()
                        {
                            Id = int.Parse(entity.ClaimTypeId.Value),
                            Name = entity.ClaimTypeId.DisplayName
                        },
                
                ActionCodes = new List<int> { (int)ActionType.AddClaim, (int)ActionType.ShowClaim,
                     (int)ActionType.ReplyToClaim,(int)ActionType.DeleteClaim,(int)ActionType.CancelClaim
                
                
                }
            };
            return res;
        }

        public override Claim MapToEntity(ClaimDTOWithAction model)
        {
            throw new NotSupportedException("Map ClaimDTOWithAction to Claim not supported");
        }
    }
}
