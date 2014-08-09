using System;
using System.Collections.Generic;
using System.Linq;
using MITD.Domain.Model;
using MITD.PMSSecurity.Exceptions;

namespace MITD.PMSSecurity.Domain.Logs
{
    public class ExceptionLog : Log
    {
        #region Fields

       

        #endregion

        #region Properties

       
        


        #endregion

        #region Constructors
       
        protected ExceptionLog()
        {
           //For OR mapper
        }

        public ExceptionLog(LogId id, string code, LogLevel logLevel, User party, string className,
            string methodName, string title, string messages)
            : base(id, code, logLevel, party, className, methodName, title, messages)
        {
        }

        #endregion

        #region Public Methods

        
        #endregion

        #region IEntity Member



        #endregion

        #region Object override


        #endregion


    }
}