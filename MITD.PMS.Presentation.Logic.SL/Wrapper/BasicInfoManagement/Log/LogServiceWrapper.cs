using System;
using System.Windows;
using MITD.PMS.Presentation.Contracts;
using System.Collections.Generic;
using System.Linq;
using MITD.Presentation;


namespace MITD.PMS.Presentation.Logic.Wrapper
{
    public class LogServiceWrapper : ILogServiceWrapper
    {
        private readonly IUserProvider userProvider;

        private string baseAddress = PMSClientConfig.BaseApiAddress + "Logs";

        public LogServiceWrapper(IUserProvider userProvider)
        {
            this.userProvider = userProvider;
        }

        public void GetAllLogs(Action<PageResultDTO<LogDTOWithActions>, Exception> action, int pageSize, int pageIndex)
        {

            var url = string.Format(baseAddress + "?PageSize=" + pageSize + "&PageIndex=" + pageIndex);
            WebClientHelper.Get(new Uri(url, UriKind.Absolute),
                action, WebClientHelper.MessageFormat.Json, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void DeleteLog(Action<string, Exception> action, Guid id)
        {
            var url = string.Format(baseAddress + "?Id=" + id);
            WebClientHelper.Delete(new Uri(url, UriKind.Absolute), action, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetAllLogs(Action<List<LogDTO>, Exception> action)
        {
            var url = string.Format(baseAddress );
            WebClientHelper.Get(new Uri(url, UriKind.Absolute),
                action, WebClientHelper.MessageFormat.Json, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetLog(Action<LogDTO, Exception> action, Guid id)
        {
            var url = string.Format(baseAddress + "?Id=" + id);
            WebClientHelper.Get(new Uri(url, UriKind.Absolute),
                action, WebClientHelper.MessageFormat.Json, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void AddLog(Action<LogDTO, Exception> action, LogDTO log)
        {
            var url = string.Format(baseAddress);
            WebClientHelper.Post(new Uri(url, UriKind.Absolute),
                action, log, WebClientHelper.MessageFormat.Json, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void UpdateLog(Action<LogDTO, Exception> action, LogDTO log)
        {
            var url = string.Format(baseAddress);
            WebClientHelper.Put(new Uri(url, UriKind.Absolute),
                action,log,
                WebClientHelper.MessageFormat.Json, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

    }
}
