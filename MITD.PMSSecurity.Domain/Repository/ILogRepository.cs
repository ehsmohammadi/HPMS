using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MITD.Domain.Repository;
using MITD.PMSSecurity.Domain.Logs;

namespace MITD.PMSSecurity.Domain
{
    public interface ILogRepository : IRepository
    {
        IList<Log> GetAllLogs();
        Log GetLogById(LogId logId);
        void Delete(Log log);
        void Add(Log log);
       
    }
}
