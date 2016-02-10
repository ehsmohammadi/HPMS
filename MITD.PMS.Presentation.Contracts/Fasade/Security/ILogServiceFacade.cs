using System;
using System.Collections.Generic;
using MITD.Core;

namespace MITD.PMS.Presentation.Contracts
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
