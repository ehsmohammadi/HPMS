using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using MITD.PMSSecurity.Domain;
using MITD.PMSSecurity.Domain.Logs;
using MITD.PMSSecurity.Domain.Service;

namespace MITD.PMSSecurity.Application
{
    public class DbLoggerService : ILoggerService
    {
        private readonly ILogRepository logRepository;
        public DbLoggerService(ILogRepository logRepository)
        {
            this.logRepository = logRepository;
        }

        public void AddLog(Log log)
        {
            using (var scope = new TransactionScope())
            {
                logRepository.Add(log);
                scope.Complete();
            }
        }

        public IList<Log> GetAll()
        {
            return logRepository.GetAllLogs();
        }

        public Log GetLogById(LogId logId)
        {
            return logRepository.GetLogById(logId);
        }

        public void DeleteLog(Log log)
        {
            using (var scope = new TransactionScope())
            {
                logRepository.Delete(log);
                scope.Complete();
            }
        }
    }
}
