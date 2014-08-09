using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.PMSSecurity.Domain.Logs;
using MITD.PMSSecurity.Exceptions;

namespace MITD.PMSSecurity.Domain.Service
{
    public class LogManagerService : ILogManagerService
    {
        private readonly ILoggerServiceFactory loggerFactory;

        private static string loggerServiceKeys = ConfigurationManager.AppSettings["LogServicesPriority"];
        private static string logLevelTypeNames = ConfigurationManager.AppSettings["LogLevelTypes"];

        private readonly string primaryLoggerKey = "";
        private readonly string secondaryLoggerKey = "";
        private readonly string thirdLoggerKey = "";
        private readonly List<LogLevel> logLevels = new List<LogLevel>();

        public LogManagerService(ILoggerServiceFactory loggerFactory)
        {
            this.loggerFactory = loggerFactory;
            var loggerKeys = (loggerServiceKeys != null) ? loggerServiceKeys.Split(',') : new string[] { };
            if (loggerKeys.Any())
                primaryLoggerKey = loggerKeys[0];

            if (loggerKeys.Length > 1)
                secondaryLoggerKey = loggerKeys[1];
            if (!string.IsNullOrWhiteSpace(logLevelTypeNames))
            {
                var levelList = logLevelTypeNames.Split(',');
                logLevels = levelList.Select(l => LogLevel.FromDisplayName<LogLevel>(l)).ToList();
            }
        }

        public Log AddLog(Log log)
        {
            var logger = loggerFactory.Create(primaryLoggerKey);
            try
            {
                if (logLevels.Contains(log.LogLevel))
                    logger.AddLog(log);
            }
            catch
            {
                log = AddLogWithSecondaryLogger(log);
            }
            finally
            {
                loggerFactory.Release(logger);
            }
            return log;
        }

        private Log AddLogWithSecondaryLogger(Log log)
        {
            if (String.IsNullOrWhiteSpace(secondaryLoggerKey))
                return log;

            var logger = loggerFactory.Create(secondaryLoggerKey);
            try
            {
                if (logLevels.Contains(log.LogLevel))
                    logger.AddLog(log);
            }
            catch
            { log = AddLogWithThridLogger(log); }
            finally
            {
                loggerFactory.Release(logger);
            }
            return log;
        }

        private Log AddLogWithThridLogger(Log log)
        {
            if (String.IsNullOrWhiteSpace(thirdLoggerKey))
                return log;

            var logger = loggerFactory.Create(thirdLoggerKey);
            try
            {
                if (logLevels.Contains(log.LogLevel))
                    logger.AddLog(log);
            }
            catch { }
            finally
            {
                loggerFactory.Release(logger);
            }
            return log;
        }

        public void DeleteLog(Log log)
        {
            var logger = loggerFactory.Create(primaryLoggerKey);
            try
            {
                logger.DeleteLog(log);
            }
            finally
            {
                loggerFactory.Release(logger);
            }
        }

        public Log GetLog(LogId logId)
        {
            var logger = loggerFactory.Create(primaryLoggerKey);
            Log log = null;
            try
            {
                log = logger.GetLogById(logId);
            }
            finally
            {
                loggerFactory.Release(logger);
            }
            return log;
        }

        public List<Log> GetAllLog()
        {
            var logger = loggerFactory.Create(primaryLoggerKey);
            var logList = new List<Log>();
            try
            {
                logList = logger.GetAll().ToList();
            }
            finally
            {
                loggerFactory.Release(logger);
            }
            return logList;
        }


    }
}
