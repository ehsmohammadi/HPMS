using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.Core;
using MITD.PMSSecurity.Domain.Logs;

namespace MITD.PMSSecurity.Domain.Service
{
    public interface ILoggerService:IService
    {
        void AddLog(Log log);
        IList<Log> GetAll();
        Log GetLogById(LogId logId);
        void DeleteLog(Log log);
    }
}
