using System;
using System.Collections.Generic;
using System.Linq;
using MITD.Domain.Model;
using MITD.PMSSecurity.Exceptions;

namespace MITD.PMSSecurity.Domain.Logs
{
    public class EventLog : Log
    {
      

        #region Properties


        #endregion

      
       #region Constructors
       
        protected EventLog()
        {
           //For OR mapper
        }

        public EventLog(LogId id, string code, LogLevel logLevel, User party, string className,
            string methodName, string title, string messages)
            : base(id,code,logLevel,party,className,methodName,title,messages)
        {
        }

        #endregion

    }
}