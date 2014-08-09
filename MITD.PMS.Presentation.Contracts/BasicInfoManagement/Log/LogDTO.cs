using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using MITD.Presentation;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MITD.PMS.Presentation.Contracts
{
    [KnownType(typeof(ExceptionLogDTO))]
    [KnownType(typeof(EventLogDTO))]
    [KnownType(typeof(LogDTOWithActions))]
    [KnownType(typeof(ExceptionLogDTOWithActions))]
    [KnownType(typeof(EventLogDTOWithActions))]
    [JsonConverter(typeof(LogDtoConverter))]
    public partial class LogDTO
    {
        public string DTOTypeName
        {
            get { return GetType().Name; }
        }

        private Guid id;
        public Guid Id
        {
            get { return id; }
            set { this.SetField(p => p.Id, ref id, value); }
        }

        private string code;
        public string Code
        {
            get { return code; }
            set { this.SetField(p => p.Code, ref code, value); }
        }

        private string logLevel;
        public string LogLevel
        {
            get { return logLevel; }
            set { this.SetField(p => p.LogLevel, ref logLevel, value); }
        }

        private string userName;
        public string UserName
        {
            get { return userName; }
            set { this.SetField(p => p.UserName, ref userName, value); }
        }


        private string className;
        public string ClassName
        {
            get { return className; }
            set { this.SetField(p => p.ClassName, ref className, value); }
        }

        private string methodName;
        public string MethodName
        {
            get { return methodName; }
            set { this.SetField(p => p.MethodName, ref methodName, value); }
        }

        private DateTime logDate;
        public DateTime LogDate
        {
            get { return logDate; }
            set { this.SetField(p => p.LogDate, ref logDate, value); }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set { this.SetField(p => p.Title, ref title, value); }
        }

        private string messages;
        public string Messages
        {
            get { return messages; }
            set { this.SetField(p => p.Messages, ref messages, value); }
        }


    }

    public class LogDtoConverter : JsonCreationConverter<LogDTO>
    {

        protected override LogDTO Create(Type objectType,
          JObject jObject)
        {
            var dtoType = jObject.Value<string>("DTOTypeName");
            if (dtoType.Equals("EventLogDTO"))
                return new EventLogDTO();
            else if (dtoType.Equals("ExceptionLogDTO"))
                return new ExceptionLogDTO();
            else if (dtoType.Equals("EventLogDTOWithActions"))
                return new EventLogDTOWithActions();
            else if (dtoType.Equals("ExceptionLogDTOWithActions"))
                return new ExceptionLogDTOWithActions();
            else
                return new LogDTO();
        }
    }
}
