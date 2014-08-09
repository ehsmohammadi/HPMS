using System;
using System.Collections.Generic;
using System.Security.Claims;
using MITD.Core;
using MITD.PMSSecurity.Domain;
using MITD.PMSSecurity.Domain.Logs;

namespace MITD.PMSSecurity.Application.Contracts
{
    public interface ILogService : IService
    {
        Log AddEventLog(string code, LogLevel logLevel, User user, string className,
            string methodName, string title, string messages);
        Log AddExceptionLog(string code, LogLevel logLevel, User user, string className,
            string methodName, string title, string messages);
        void Delete(List<LogId> toList);
        IList<Log> GetAllLogs();
        Log GetLogById(LogId logId);

        void AddExceptionLog(Exception ex);
    }
}
