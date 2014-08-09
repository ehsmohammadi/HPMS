using System.Collections.Generic;
using MITD.Presentation;
using MITD.PMS.Presentation.Contracts;
using System;

namespace MITD.PMS.Presentation.Logic
{
    public interface ILogServiceWrapper : IServiceWrapper
    { 
        void GetLog(Action<LogDTO, Exception> action, Guid id);
        void AddLog(Action<LogDTO, Exception> action, LogDTO log);
        void GetAllLogs(Action<PageResultDTO<LogDTOWithActions>, Exception> action, int pageSize, int pagePost);
        void DeleteLog(Action<string, Exception> action, Guid id);
        void GetAllLogs(Action<List<LogDTO>, Exception> action);
    }
}
