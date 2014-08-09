using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.PMSSecurity.Domain.Logs;

namespace MITD.PMSSecurity.Domain.Service
{
    public interface ILogManagerService
    {
        Log AddLog(Log log);
        void DeleteLog(Log log);
        Log GetLog(LogId logId);
        List<Log> GetAllLog();
    }
}
