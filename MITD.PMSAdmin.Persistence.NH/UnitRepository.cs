using System;
using System.Collections.Generic;
using System.Linq;
using MITD.Data.NH;
using MITD.Domain.Repository;
using MITD.PMS.Common;
using MITD.PMSAdmin.Domain.Model.Units;

namespace MITD.PMSAdmin.Persistence.NH
{
    public class UnitRepository : NHRepository, IUnitRepository
    {
        private NHRepository<Unit> rep; 

        public UnitRepository(NHUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            init();
        }

        public UnitRepository(IUnitOfWorkScope unitOfWorkScope)
            : base(unitOfWorkScope)
        {
            init();
        }

        private void init()
        {
            rep=new NHRepository<Unit>(unitOfWork);
        }


        public void FindBy(ListFetchStrategy<Unit> fs)
        {
                rep.GetAll(fs);
        }

        public void Add(Unit unit)
        {
            rep.Add(unit);
        }

        public void UpdateUnit(Unit unit)
        {
            rep.Update(unit);
        }

        public Unit GetById(UnitId unitId)
        {
            return rep.FindByKey(unitId);
            
        }

        public UnitId GetNextId()
        {
            using (var ctx = Session.SessionFactory.OpenStatelessSession())
            {
                ctx.BeginTransaction();
                var res = ctx.CreateSQLQuery("Select next value for dbo.UnitSeq").UniqueResult<long>();
                return new UnitId(res);
            }
        }

        public void DeleteUnit(Unit unit)
        {
            rep.Delete(unit);
        }

        public List<Unit> GetAll()
        {
            return rep.GetAll().ToList();
        }

        public UnitException ConvertException(Exception exp)
        {
            string typeName = "";
            string keyName = "";
            if (NHExceptionDetector.IsDublicateException(exp, out typeName, out keyName))
                return new UnitDuplicateException("Unit", "DictionaryName");
            if (NHExceptionDetector.IsDeleteHasRelatedDataException(exp, out typeName, out keyName))
                return new UnitDeleteException("Unit", "Id");
            throw new Exception();
        }

        public UnitException TryConvertException(Exception exp)
        {
            UnitException res = null;
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
