using System;
using System.Collections.Generic;
using System.Linq;
using MITD.Domain.Model;
using MITD.PMSSecurity.Exceptions;

namespace MITD.PMSSecurity.Domain.Logs
{
    public class Log : IEntity<Log>
    {
        #region Fields

        #endregion

        #region Properties

       
        private readonly LogId id;
        public virtual LogId Id { get { return id; } }

        private readonly string code;
        public virtual string Code { get { return code; } }

        private readonly LogLevel logLevel;
        public virtual LogLevel LogLevel { get { return logLevel; } }

        private readonly PartyId partyId;
        public virtual PartyId PartyId { get { return partyId; } }

        private readonly string className;
        public virtual string  ClassName { get { return className; } }

        private readonly string methodName;
        public virtual string MethodName { get { return methodName; } }

        private readonly DateTime logDate;
        public virtual DateTime LogDate { get { return logDate; } }

        private readonly string title;
        public virtual string Title { get { return title; } }

        private readonly string messages;
        public virtual string Messages { get { return messages; } }


        #endregion

        #region Constructors
       
        protected Log()
        {
           //For OR mapper
        }

        public Log(LogId id, string code, LogLevel logLevel, User user, string className,
            string methodName, string title, string messages)
        {
            if (id == null)
                throw new ArgumentNullException("id");
            this.id = id;
            this.code = code;
            this.logLevel = logLevel;
            if (user != null)
                this.partyId = user.Id;
            this.className = className;
            this.methodName = methodName;
            this.logDate = DateTime.Now;

            if (!string.IsNullOrEmpty(title) && title.Length > 200)
                this.title = title.Substring(0, 199);
            else
                this.title = title;
            
            if (!string.IsNullOrEmpty(messages) && messages.Length > 4000)
                this.messages = messages.Substring(0, 3999);
            else
                this.messages = messages;
        }

        #endregion

        #region Public Methods

       

        #endregion

        #region IEntity Member

        public virtual bool SameIdentityAs(Log other)
        {
            return (other != null) && Id.Equals(other.Id);
        }



        #endregion

        #region Object override

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (Log)obj;
            return SameIdentityAs(other);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return Id.ToString();
        }

        #endregion


    }
}