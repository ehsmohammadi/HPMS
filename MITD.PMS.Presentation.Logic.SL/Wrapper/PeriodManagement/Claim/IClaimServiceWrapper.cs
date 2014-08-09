using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public interface IClaimServiceWrapper:IServiceWrapper
    {
        void GetClaim(Action<ClaimDTO, Exception> action,long periodId, long claimId);
        void GetAllClaim(Action<PageResultDTO<ClaimDTOWithAction>, Exception> action, long periodId, string employeeNo, int pageSize, int pageIndex);
        void AddClaim(Action<ClaimDTO, Exception> action, ClaimDTO claim);
        //void UpdateClaim(Action<ClaimDTO, Exception> action, ClaimDTO claim);
        void DeleteClaim(Action<string, Exception> action,long periodId, long id);
        void GetClaimTypeList(Action<List<ClaimTypeDTO>, Exception> action, long periodId);
        void GetAllClaimStateList(Action<List<ClaimStateDTO>, Exception> action, long periodId);
        //void GetAcceptableClaimStateList(Action<List<ClaimStateDTO>, Exception> action, long periodId, long id);

        void ChangeClaimState(Action<ClaimDTO, Exception> action, long periodId, long claimId, string changeStateMessage,
                              ClaimStateDTO claimState);

        void GetAllClaim(Action<PageResultDTO<ClaimDTOWithAction>, Exception> action, long periodId, int pageSize, int pageIndex);
    }
}
