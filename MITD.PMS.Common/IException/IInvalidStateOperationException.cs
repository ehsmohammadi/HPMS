﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.Core
{
    public interface IInvalidStateOperationException:IException
    {
        string DomainObjectName { get; }
        string StateName { get; }
        string OperationName { get; }

    }

    public class InvalidStateOperationException : Exception, IInvalidStateOperationException
    {
        public InvalidStateOperationException(string message, string domainObjectName,
            string stateName, string operationName)
            : base(message)
        {
            Code = int.Parse(ApiExceptionCode.InvalidStateOperation.Value);
            DomainObjectName = domainObjectName;
            StateName = stateName;
            OperationName = operationName;
        }
        public int Code { get; private set; }
        public string DomainObjectName { get; private set; }
        public string StateName { get; private set; }
        public string OperationName { get; private set; }
    }
}
