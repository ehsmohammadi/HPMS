using System;
using System.Collections.Generic;
using System.Linq;
using MITD.Core;
using MITD.PMS.Presentation.Contracts;
using MITD.PMSSecurity.Application.Contracts;
using MITD.PMSSecurity.Domain;
using MITD.PMSSecurity.Domain.Logs;

namespace MITD.PMS.Interface
{
    public class LogServiceFacade : ILogServiceFacade
    {
        private readonly ILogService logService;
        private IMapper<Log, LogDTOWithActions> logDTOWithActionsMapper;
        private IMapper<Log, LogDTO> logDTOMapper;
        private readonly IUserRepository userRep;

        public LogServiceFacade(ILogService logService,
            IMapper<Log, LogDTOWithActions> logDTOWithActionsMapper,
            IMapper<Log, LogDTO> logDTOMapper  ,
            IUserRepository userRep
          )
        {
            this.logService = logService;
            this.logDTOWithActionsMapper = logDTOWithActionsMapper;
            this.logDTOMapper = logDTOMapper;
            this.userRep = userRep;
        }

        [RequiredPermission(ActionType.ShowLog)]
        public PageResultDTO<LogDTOWithActions> GetAllLogs(int pageSize, int pageIndex,  QueryStringConditions conditions )
        {
            var logs = logService.GetAllLogs();
            var res = new PageResultDTO<LogDTOWithActions>()
                {
                    Result = logs.Select(u => logDTOWithActionsMapper.MapToModel(u)).ToList(),
                    CurrentPage = 1,
                    PageSize = 20,
                    TotalCount = 10,
                    TotalPages = 1
                };

            return res;
        }

        [RequiredPermission(ActionType.ShowLog)]
        public LogDTO GetLog(Guid logId)
        {
            Log log = logService.GetLogById(new LogId(logId));
            var logDto = logDTOMapper.MapToModel(log);
            return logDto;
        }

        public LogDTO AddEventLog(EventLogDTO logDto)
        {
            User user = null;
            //this check must be move to domain servi
            if (!string.IsNullOrWhiteSpace(logDto.UserName))
                user = userRep.GetUserById(new PartyId(logDto.UserName));
            var log = logService.AddEventLog(logDto.Code,LogLevel.FromDisplayName<LogLevel>(logDto.LogLevel) ,
                user, logDto.ClassName, logDto.MethodName,logDto.Title, logDto.Messages);
            return logDTOMapper.MapToModel(log);
        }

        public LogDTO AddExceptionLog(ExceptionLogDTO logDto)
        {
            User user = null;
            //this check must be move to domain servi
            if (!string.IsNullOrWhiteSpace(logDto.UserName))
                user = userRep.GetUserById(new PartyId(logDto.UserName));
            var log = logService.AddExceptionLog(logDto.Code, LogLevel.FromDisplayName<LogLevel>(logDto.LogLevel),
                user, logDto.ClassName, logDto.MethodName, logDto.Title, logDto.Messages);
            return logDTOMapper.MapToModel(log);
        }

        public string DeleteLog(List<Guid> logIdList)
        {
            logService.Delete(logIdList.Select(id => new LogId(id)).ToList());
            return "logs deleted successfully ";
        }

        public LogDTO AddEventLog(string title, string code, string logLevel, string className, string methodName, string messages,
                                string userName)
        {
            User user = null;
            //this check must be move to domain servi
            if (!string.IsNullOrWhiteSpace(userName))
                user = userRep.GetUserById(new PartyId(userName));
            var log = logService.AddEventLog(code, LogLevel.FromDisplayName<LogLevel>(logLevel),
                user, className, methodName, title,messages);
            return logDTOMapper.MapToModel(log);
        }
    }
}
