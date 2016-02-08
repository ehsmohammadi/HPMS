using System;
using System.Collections.Generic;
using System.Linq;
using MITD.Core;
using MITD.Domain.Model;
using MITD.Domain.Repository;
using MITD.PMS.Domain.Model.Jobs;
using MITD.PMSAdmin.Application.Contracts;
using MITD.PMSAdmin.Domain.Model.CustomFieldTypes;
using System.Transactions;
using MITD.PMSAdmin.Exceptions;

namespace MITD.PMSAdmin.Application
{
    public class CustomFieldService : ICustomFieldService
    {
        private readonly ICustomFieldRepository customFieldRep;

        public CustomFieldService(ICustomFieldRepository customFieldRep)
        {
            this.customFieldRep = customFieldRep;
        }


        public CustomFieldType AddCustomFieldType(string name, string dictionaryName,
                             long minValue, long maxValue, int entityId, string typeId)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var id = customFieldRep.GetNextId();
                    var customFieldType = new CustomFieldType(id, name, dictionaryName, minValue, maxValue, Enumeration.FromValue<EntityTypeEnum>(entityId.ToString()), typeId);
                    customFieldRep.Add(customFieldType);
                    scope.Complete();
                    return customFieldType;
                }
            }
            catch (Exception exp)
            {
                var res = customFieldRep.TryConvertException(exp);
                if (res == null)
                    throw;
                throw res;
            }
        }

        public CustomFieldType UppdateCustomFieldType(CustomFieldType customFieldType)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    customFieldRep.UpdateCustomFieldType(customFieldType);
                    scope.Complete();
                    return customFieldType;
                }
            }
            catch (Exception exp)
            {
                var res = customFieldRep.TryConvertException(exp);
                if (res == null)
                    throw;
                throw res;
            }
        }

        public List<CustomFieldType> GetBy(List<CustomFieldTypeId> customFieldIdList)
        {
            using (var scope = new TransactionScope())
            {
                var res = customFieldRep.Find(customFieldIdList).ToList();
                scope.Complete();
                return res;
            }

        }

        public void DeleteCustomField(CustomFieldType customFieldType)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    customFieldRep.DeleteCustomField(customFieldType);
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                var res = customFieldRep.TryConvertException(exp);
                if (res == null)
                    throw;
                throw res;
            }
        }
    }
}
