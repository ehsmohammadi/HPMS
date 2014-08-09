using MITD.Core;
using MITD.PMSAdmin.Domain.Model.Units;

namespace MITD.PMSAdmin.Application.Contracts
{
    public interface IUnitService:IService
    { 
       
        Unit AddUnit(string name, string dictionaryName);
        Unit UppdateUnit(UnitId unit, string name, string dictionaryName);
        void DeleteUnit(UnitId unitId);
        Unit GetBy(UnitId unitId);
    }
}
