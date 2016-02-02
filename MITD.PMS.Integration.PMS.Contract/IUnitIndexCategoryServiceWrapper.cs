using System;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Integration.PMS.Contract
{
    public partial interface IUnitIndexServiceWrapper
    {
        void GetUnitIndexCategory(Action<UnitIndexCategoryDTO, Exception> action, long id);
        void AddUnitIndexCategory(Action<UnitIndexCategoryDTO, Exception> action, UnitIndexCategoryDTO unit);
        void UpdateUnitIndexCategory(Action<UnitIndexCategoryDTO, Exception> action, UnitIndexCategoryDTO unit);
        void GetAllUnitIndexCategorys(Action<PageResultDTO<UnitIndexCategoryDTOWithActions>, Exception> action, int pageSize, int pageIndex);
        void DeleteUnitIndexCategory(Action<string, Exception> action, long id);
    }
}
