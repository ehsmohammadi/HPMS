using System;
using System.Collections.Generic;
using System.Linq;
using MITD.Data.NH;
using MITD.Domain.Repository;
using MITD.PMS.Domain.Model.UnitIndices;
using MITD.PMS.Domain.Model.Periods;
using System.Linq.Expressions;
using MITD.PMS.Common;
using MITD.PMS.Domain.Model.UnitIndices;


namespace MITD.PMS.Persistence.NH
{
    public class UnitIndexRepository : NHRepository, IUnitIndexRepository
    {
        private NHRepository<AbstractUnitIndex> rep;

        public UnitIndexRepository(NHUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            init();
        }

        public UnitIndexRepository(IUnitOfWorkScope unitOfWorkScope)
            : base(unitOfWorkScope)
        {
            init();
        }

        private void init()
        {
            rep = new NHRepository<AbstractUnitIndex>(unitOfWork);
        }



        public List<UnitIndex> GetAllUnitIndex(PeriodId periodId)
        {
            return rep.Find<UnitIndex>(u => u.PeriodId == periodId).ToList();
        }

        public List<UnitIndexGroup> GetAllUnitIndexGroup(PeriodId periodId)
        {
            return rep.Find<UnitIndexGroup>(u => u.PeriodId == periodId).ToList();
        }

        public AbstractUnitIndex GetById(AbstractUnitIndexId unitIndexId)
        {
            return rep.FindByKey(unitIndexId);

        }

        public void Add(AbstractUnitIndex unitIndex)
        {
            rep.Add(unitIndex);
        }

        public void Delete(AbstractUnitIndexId unitIndexId)
        {
            var abstractIndex = GetById(unitIndexId);
            rep.Delete(abstractIndex);
        }

        public UnitIndex GetUnitIndexById(AbstractUnitIndexId id)
        {
            return rep.Single<UnitIndex>(u => u.Id == id);
        }

        public UnitIndexGroup GetUnitIndexGroupById(AbstractUnitIndexId id)
        {
            return rep.Single<UnitIndexGroup>(u => u.Id == id);
        }

        public IEnumerable<AbstractUnitIndex> GetAll(PeriodId periodId)
        {
            return rep.Find(i => i.PeriodId == periodId).ToList();
        }

        public AbstractUnitIndexId GetNextId()
        {
            using (var ctx = Session.SessionFactory.OpenStatelessSession())
            {
                ctx.BeginTransaction();
                var res = ctx.CreateSQLQuery("Select next value for dbo.Periods_AbstractUnitIndexSeq").UniqueResult<long>();
                return new AbstractUnitIndexId(res);
            }
        }

        public void Update(AbstractUnitIndex unitIndex)
        {
            rep.Update(unitIndex);
        }

        public IEnumerable<UnitIndex> FindUnitIndices(Expression<Func<UnitIndex, bool>> predicate)
        {
            return rep.Find(predicate).ToList();
        }

        public List<AbstractUnitIndex> GetAllAbstractUnitIndexByParentId(AbstractUnitIndexId id)
        {
            var unitIndexChilds = rep.Find<UnitIndex>(u => u.Group.Id == id).ToList();
            var unitIndexGroupChilds = rep.Find<UnitIndexGroup>(u => u.Parent.Id == id).ToList();

            List<AbstractUnitIndex> res = new List<AbstractUnitIndex>(unitIndexChilds);
            res.AddRange(unitIndexGroupChilds);
            return res;
        }

        public List<UnitIndexGroup> GetAllParentUnitIndexGroup(Period period)
        {
            return rep.Find<UnitIndexGroup>(u => u.Parent == null && u.PeriodId == period.Id).ToList();
        }

        public SharedUnitIndexId GetSharedUnitIndexIdBy(AbstractUnitIndexId abstractUnitIndexId)
        {
            //return rep.Find<UnitIndex>(i => abstractUnitIndexIds.Contains(i.Id)).Select(i => i.SharedUnitIndexId).ToList();
            return rep.Single<UnitIndex>(j => j.Id == abstractUnitIndexId).SharedUnitIndexId;
        }

        public AbstractUnitIndexId GetUnitIndexIdBy(Period period, SharedUnitIndexId sharedUnitIndexId)
        {
            return rep.Single<UnitIndex>(j => j.PeriodId == period.Id && j.SharedUnitIndexId.Id == sharedUnitIndexId.Id).Id;
        }

        public Exception ConvertException(Exception exp)
        {
            string typeName = "";
            string keyName = "";
            if (NHExceptionDetector.IsDublicateException(exp, out typeName, out keyName))
                return new UnitIndexDuplicateException(typeName, keyName);
            if (NHExceptionDetector.IsDeleteHasRelatedDataException(exp, out typeName, out keyName))
                return new UnitIndexDeleteException(typeName, keyName);
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
