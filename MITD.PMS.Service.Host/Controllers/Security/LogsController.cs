using System;
using System.Collections.Generic;
using MITD.PMS.Interface;
using MITD.PMS.Presentation.Contracts;
using System.Web.Http;

namespace MITD.PMS.Service.Host.Controllers
{
    [Authorize]
    public class LogsController : ApiController
    {
        private readonly ILogServiceFacade logService;

        public LogsController(ILogServiceFacade logService)
        {
            this.logService = logService;
        } 

        public PageResultDTO<LogDTOWithActions> GetAllLogs(int pageSize, int pageIndex,string filter="",string sortBy="")
        {
            var queryStringCondition = new QueryStringConditions { SortBy = sortBy, Filter = filter };
            return logService.GetAllLogs(pageSize, pageIndex, queryStringCondition);
        }

        public LogDTO PostLog(LogDTO log)
        {
            if (log is EventLogDTO)
                return logService.AddEventLog(log as EventLogDTO);
            else
                return logService.AddExceptionLog(log as ExceptionLogDTO);
        }

        public LogDTO GetLog(Guid id)
        {
            return logService.GetLog(id);
        }

        public string DeleteLog(Guid id)
        {
            return logService.DeleteLog(new List<Guid>() { id });
        }
    }
}