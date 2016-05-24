using System.Collections.Generic;
using System.Linq;
using MITD.Data.NH;
using MITD.Domain.Repository;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Model.Units;
using System;
using MITD.PMS.Common;

namespace MITD.PMS.Persistence.NH
{
    public class UnitRepository:NHRepository,IUnitRepository
    {
        private NHRepository<Unit> rep; 

        public UnitRepository(NHUnitOfWork unitOfWork) : base(unitOfWork)
        {
            init();
        }

        public UnitRepository(IUnitOfWorkScope unitOfWorkScope) : base(unitOfWorkScope)
        {
            init();
        }

        private void init()
        {
            rep=new NHRepository<Unit>(unitOfWork);
        }

        public List<Unit> GetUnits(PeriodId periodId)
        {
            return rep.Find(u => u.Id.PeriodId == periodId).ToList();
        }

        public void Add(Unit unit)
        {
            rep.Add(unit);
        }

        public void DeleteUnit(Unit unit)
        {
            rep.Delete(unit);
        }

        public Unit GetBy(UnitId unitId)
        {
           // return rep.Single(u => u.Id.PeriodId == unitId.PeriodId && u.Id.SharedUnitId == unitId.SharedUnitId);
            return rep.Find(u => u.Id.PeriodId == unitId.PeriodId && u.Id.SharedUnitId == unitId.SharedUnitId).SingleOrDefault();
        }

        public List<Unit> GetAllParentUnits(Period period)
        {
            return rep.Find(u =>  u.Id.PeriodId == period.Id && u.Parent == null).ToList();
        }

        public List<Unit> GetAllUnitByParentId(UnitId parentId)
        {
            return rep.Find(u => u.Parent.Id == parentId).ToList();
        }

        public UnitId GetUnitIdBy(Period period, SharedUnitId sharedUnitId)
        {
            return rep.Find(u => u.Id.PeriodId == period.Id && u.Id.SharedUnitId == sharedUnitId).Single().Id;
        }

        public List<Unit> GetAllInquirySubjectUnits(EmployeeId inquirerId)
        {
            return rep.Find(j => j.ConfigurationItemList.Any(jc => jc.Id.InquirerId == inquirerId)).ToList();

        }

        public Exception ConvertException(Exception exp)
        {
            string typeName = "";
            string keyName = "";
            if (NHExceptionDetector.IsDublicateException(exp, out typeName, out keyName))
                return new UnitDuplicateException(typeName, keyName);
            if (NHExceptionDetector.IsDeleteHasRelatedDataException(exp, out typeName, out keyName))
                return new UnitDeleteException(typeName, keyName);
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

        public List<UnitId> GetAllUnitId(Period period)
        {
            return rep.GetQuery().Where(u => u.Id.PeriodId.Id == period.Id.Id).Select(u => u.Id).ToList();
        }
    }
}
