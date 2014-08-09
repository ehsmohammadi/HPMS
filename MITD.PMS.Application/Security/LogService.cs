using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Claims;
using System.Transactions;
using MITD.PMSSecurity.Application.Contracts;
using MITD.PMSSecurity.Domain;
using MITD.PMSSecurity.Domain.Logs;
using MITD.PMSSecurity.Domain.Service;

namespace MITD.PMSSecurity.Application
{
    public class LogService : ILogService
    {
        private ILogManagerService logManagerService;

        public LogService(ILogManagerService logManagerService)
        {
            this.logManagerService = logManagerService;
        }

        public Log AddEventLog(string code, LogLevel logLevel, User user, string className,
                          string methodName, string title, string messages)
        {
            var log = new EventLog(new LogId(Guid.NewGuid()), code, logLevel, user, className, methodName, title, messages);
            return logManagerService.AddLog(log);
        }

        public Log AddExceptionLog(string code, LogLevel logLevel, User user, string className,
                          string methodName, string title, string messages)
        {
            var log = new EventLog(new LogId(Guid.NewGuid()), code, logLevel, user, className, methodName, title, messages);
            return logManagerService.AddLog(log);
        }

        public void Delete(List<LogId> toList)
        {
            foreach (LogId logId in toList)
            {
                var log = logManagerService.GetLog(logId);
                logManagerService.DeleteLog(log);
            }
        }

        public IList<Log> GetAllLogs()
        {
            return logManagerService.GetAllLog();
        }

        public Log GetLogById(LogId logId)
        {
            return logManagerService.GetLog(logId);
        }

        public void AddExceptionLog(Exception ex)
        {
        }
    }
}
