using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.PMSSecurity.Domain.Logs;
using MITD.PMSSecurity.Domain.Service;

namespace MITD.PMSSecurity.Application
{
    public class WindowsEventsLoggerService : ILoggerService
    {
        public WindowsEventsLoggerService()
        {
            
        }

        public void AddLog(Log log)
        {
            System.Diagnostics.EventLog weLog = new System.Diagnostics.EventLog("Application");
            weLog.Source = "MITD.PMS" ;
            string msg = logToString(log);
            weLog.WriteEntry(msg, getWindowsLogLevel(log.LogLevel));
        }

        private EventLogEntryType getWindowsLogLevel(LogLevel level )
        {
            if (level == LogLevel.Error)
                return EventLogEntryType.Error;
            if (level == LogLevel.AccessControl)
                return EventLogEntryType.SuccessAudit;
            if (level == LogLevel.Information)
                return EventLogEntryType.Information;
            if (level == LogLevel.Warning)
                return EventLogEntryType.Warning;
            return EventLogEntryType.Information ;
        }

        private string logToString(Log log)
        {
            if (log == null)
                return "";
            string lineFeedChar = "\r\n";
            string msg = "";
            msg += (log is MITD.PMSSecurity.Domain.Logs.EventLog) ? "<LogType>:Event" : "<LogType>:Exception\n";
            msg += lineFeedChar;
            msg += log.LogDate.ToString() + lineFeedChar;
            msg += log.LogLevel + lineFeedChar;
            msg += "Id:" + log.Id.Guid.ToString() + lineFeedChar;
            if (!string.IsNullOrEmpty(log.Code)) msg += "code:" + log.Code + lineFeedChar;
            if (!string.IsNullOrWhiteSpace(log.ClassName)) msg += "className:" + log.ClassName + lineFeedChar;
            if (!string.IsNullOrWhiteSpace(log.MethodName)) msg += "methodName:" + log.MethodName + lineFeedChar;
            if (!string.IsNullOrEmpty(log.Title)) msg += log.Title + lineFeedChar;
            if (log.PartyId != null) msg += "user:" + log.PartyId.PartyName + lineFeedChar;
            if (!string.IsNullOrEmpty(log.Messages)) msg += "message:" + lineFeedChar + log.Messages + lineFeedChar;
            msg += lineFeedChar + "=========================================" + lineFeedChar;
            return msg;
        }

        public IList<Log> GetAll()
        {
            throw new NotImplementedException();
        }

        public Log GetLogById(LogId logId)
        {
            throw new NotImplementedException();
        }

        public void DeleteLog(Log log)
        {
            throw new NotImplementedException();
        }
    }
}
