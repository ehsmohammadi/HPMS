﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.Core
{
    public class DeleteExceptionConvertor :IExceptionConvertor<IDeleteException>
    {

        public Dictionary<string, string> Convert(IDeleteException exception)
        {
            var expData = new Dictionary<string, string>();
            expData.Add("Message", exception.Message);
            expData.Add("Type", typeof(IDeleteException).Name);
            expData.Add("Code", exception.Code.ToString());
            expData.Add("DomainObjectName", exception.DomainObjectName);
            expData.Add("RelatedObjectName", exception.RelatedObjectName);
            return expData;
        }



        public IDeleteException ConvertBack(Dictionary<string, string> expData)
        {
            if (!expData.Keys.Contains("Type") || !expData.Keys.Contains("Message"))
                return null;
            var exceptionType = expData["Type"];
            if (exceptionType != typeof(IDeleteException).Name)
                return null;
            var message = expData["Message"];
            var domainName = expData["DomainObjectName"];
            var relatedObjectName = expData["RelatedObjectName"];

            return new DeleteException(message, domainName, relatedObjectName);

        }


    }
}
