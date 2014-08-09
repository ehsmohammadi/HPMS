using System;
using MITD.Domain.Repository;
using MITD.PMSAdmin.Application.Contracts;
using MITD.PMSAdmin.Domain.Model.Units;
using System.Transactions;
using MITD.PMSAdmin.Exceptions;

namespace MITD.PMSAdmin.Application
{
    public class UnitService : IUnitService
    {
        private readonly IUnitRepository unitRep;

        public UnitService(IUnitRepository unitRep)
        {
            this.unitRep = unitRep;
        }

        public Unit AddUnit(string name, string dictionaryName)
        {
            try{
            using (var scope = new TransactionScope())
            {
                var id = unitRep.GetNextId();
                var unit = new Unit(id, name, dictionaryName);
                unitRep.Add(unit);
                scope.Complete();
                return unit;

            }
            }
            catch (Exception exp)
            {
                var res = unitRep.TryConvertException(exp);
                if (res == null)
                    throw;
                throw res;
            }
        }

        public Unit UppdateUnit(UnitId unitId, string name, string dictionaryName)
        {
            try{
            using (var scope = new TransactionScope())
            {
                var unit = unitRep.GetById(unitId);
                unit.Update(name, dictionaryName);
                scope.Complete();
                return unit;

            }
            }
            catch (Exception exp)
            {
                var res = unitRep.TryConvertException(exp);
                if (res == null)
                    throw;
                throw res;
            }
        }

        public void DeleteUnit(UnitId unitId)
        {
            try{
            using (var scope = new TransactionScope())
            {
                var unit = unitRep.GetById(unitId);
                unitRep.DeleteUnit(unit);
                scope.Complete();
            }
            }
            catch (Exception exp)
            {
                var res = unitRep.TryConvertException(exp);
                if (res == null)
                    throw;
                throw res;
            }
        }

        public Unit GetBy(UnitId unitId)
        {
            return unitRep.GetById(unitId);
        }
    }
}
