using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.Core;
using MITD.PMS.Presentation.Contracts;
using MITD.PMSSecurity.Domain;
using MITD.PMSSecurity.Domain.Logs;


namespace MITD.PMSSecurity.Interface.Mappers
{
    public class LogDtoMapper : BaseMapper<Log,LogDTO>, IMapper<Log,LogDTO>
    { 
        public override LogDTO MapToModel(Log entity)
        {
            LogDTO res;
            if (entity is ExceptionLog)
                res = new ExceptionLogDTO();
            else res = new EventLogDTO();

            res.Id = entity.Id.Guid;
            res.ClassName = entity.ClassName;
            res.Code = entity.Code;
            res.LogDate = entity.LogDate;
            res.LogLevel = entity.LogLevel.ToString();
            res.Messages = entity.Messages;
            res.Title = entity.Title;
            res.MethodName = entity.MethodName;
            res.UserName = (entity.PartyId != null) ? entity.PartyId.PartyName : "";
            return res;
        }

        public override Log MapToEntity(LogDTO model)
        {
            throw new System.NotImplementedException();
        }

        
    }


    public class LogDTOWithActionsMapper : BaseMapper<Log, LogDTOWithActions>, IMapper<Log, LogDTOWithActions>
    {
        public override LogDTOWithActions MapToModel(Log entity)
        {
            LogDTOWithActions res;
            if (entity is ExceptionLog)
                res = new ExceptionLogDTOWithActions();
            else res = new EventLogDTOWithActions();

            res.Id = entity.Id.Guid;
            res.ClassName = entity.ClassName;
            res.Code = entity.Code;
            res.LogDate = entity.LogDate;
            res.LogLevel = entity.LogLevel.ToString();
            res.Messages = entity.Messages;
            res.Title = entity.Title;
            res.MethodName = entity.MethodName;
            res.UserName = (entity.PartyId != null) ? entity.PartyId.PartyName : "";
            res.ActionCodes = new List<int>(){ (int) ActionType.ShowLog, (int) ActionType.DeleteLog};
            return res;
        }

        public override Log MapToEntity(LogDTOWithActions model)
        {
            throw new System.NotImplementedException();
        }


    }
}
