using System;
using System.Collections.Generic;
using System.Linq;
using MITD.Data.NH;
using MITD.Domain.Repository;
using MITD.PMS.Common;
using MITD.PMSAdmin.Domain.Model.UnitIndices;
using NHibernate.Linq;

namespace MITD.PMSAdmin.Persistence.NH
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

        public IList<UnitIndex> GetAllUnitIndex()
        {
            return rep.GetAll<UnitIndex>();
        }

        public IList<UnitIndex> GetAllUnitIndex(ListFetchStrategy<UnitIndex> fs)
        {
            return rep.GetAll(fs);
        }


        public IList<UnitIndexCategory> GetAllUnitIndexCategory()
        {
            return rep.GetAll<UnitIndexCategory>();
        }

        public IList<UnitIndexCategory> GetAllUnitIndexCategory(ListFetchStrategy<UnitIndexCategory> fs)
        {
            return rep.GetAll<UnitIndexCategory>(fs);
        }

        public IList<AbstractUnitIndex> GetAll()
        {
            return rep.GetAll();
        }

        public AbstractUnitIndex GetById(AbstractUnitIndexId unitIndexId)
        {
            return  rep.FindByKey(unitIndexId);
        }

        public AbstractUnitIndex GetByTransferId(Guid transferId)
        {
            return rep.Single(u => u.TransferId == transferId);
        }

        public void Add(AbstractUnitIndex unitIndex)
        {
            rep.Add(unitIndex);
        }

        public void Update(AbstractUnitIndex unitIndex)
        {
            rep.Update(unitIndex);
        }

        public void Delete(AbstractUnitIndexId unitIndexId)
        {
            var abstractUnitIndex = GetById(unitIndexId);
            rep.Delete(abstractUnitIndex);
        }

        public AbstractUnitIndexId GetNextId()
        {
            using (var ctx = Session.SessionFactory.OpenStatelessSession())
            {
                ctx.BeginTransaction();
                var res = ctx.CreateSQLQuery("Select next value for dbo.AbstractUnitIndexSeq").UniqueResult<long>();
                return new AbstractUnitIndexId(res);
            }
        }

        public UnitIndex GetUnitIndex(AbstractUnitIndexId id)
        {
            return rep.Single<UnitIndex>(p => p.Id == id);
        }


        public UnitIndexCategory GetUnitIndexCategory(AbstractUnitIndexId id)
        {
            return rep.Single<UnitIndexCategory>(p => p.Id == id);
        }

        public UnitIndexException ConvertException(Exception exp)
        {
            string typeName = "";
            string keyName = "";
            if (NHExceptionDetector.IsDublicateException(exp, out typeName, out keyName))
                return new UnitIndexDuplicateException("UnitIndex", "DictionaryName");
            if (NHExceptionDetector.IsDeleteHasRelatedDataException(exp, out typeName, out keyName))
                return new UnitIndexDeleteException("UnitIndex", "Id");
            throw new Exception();
        }

        public UnitIndexException TryConvertException(Exception exp)
        {
            UnitIndexException res = null;
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
