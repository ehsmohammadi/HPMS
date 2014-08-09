using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.Core;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Interface
{
    public interface ILogServiceFacade : IFacadeService
    {
        PageResultDTO<LogDTOWithActions> GetAllLogs(int pageSize, int pageIndex, QueryStringConditions conditions);
        LogDTO GetLog(Guid logId);
        LogDTO AddEventLog(EventLogDTO logDto);
        LogDTO AddExceptionLog(ExceptionLogDTO log);
        string DeleteLog(List<Guid> logIdList);

        LogDTO AddEventLog(string title, string code, string logLevel, string className, string methodName, string messages, string userName);
    }
}
