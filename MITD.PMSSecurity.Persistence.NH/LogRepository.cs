using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MITD.Data.NH;
using MITD.Domain.Repository;
using MITD.PMSSecurity.Domain;
using MITD.PMSSecurity.Domain.Logs;
using NHibernate.Linq;

namespace MITD.PMSSecurity.Persistence.NH
{
    public class LogRepository : NHRepository, ILogRepository
    {
        private NHRepository<Log> rep;

        public LogRepository(NHUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            init();
        }

        public LogRepository(IUnitOfWorkScope unitOfWorkScope)
            : base(unitOfWorkScope)
        {
            init();
        }

        private void init()
        {
            rep =  new NHRepository<Log>(unitOfWork);
        }

        public void FindLogs(Expression<Func<Log, bool>> predicate, ListFetchStrategy<Log> fs)
        {
            rep.Find<Log>(predicate, fs);
        }

        public Log GetLogById(LogId logId)
        {
            return rep.Find(l=>l.Id.Guid == logId.Guid).FirstOrDefault();
        }

        public void GetAllLogs(ListFetchStrategy<Log> fs)
        {
            rep.GetAll<Log>(fs);
        }

        public IList<Log> GetAllLogs()
        {
            return rep.GetAll<Log>();
        }

        public IList<Log> FindLogs(Expression<Func<Log, bool>> predicate)
        {
            return rep.Find<Log>(predicate);
        }

        public void Delete(Log log)
        {
            rep.Delete(log);
        }

        public void Add(Log log)
        {
            rep.Add(log);
        }

        
    }
}
