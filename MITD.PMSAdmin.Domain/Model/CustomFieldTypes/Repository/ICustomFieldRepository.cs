using System;
using System.Collections.Generic;
using MITD.Domain.Repository;
using MITD.PMSAdmin.Domain.Model.Jobs;
using MITD.PMSAdmin.Domain.Model.JobIndices;
using MITD.PMSAdmin.Domain.Model.Units;
using MITD.PMSAdmin.Domain.Model.UnitIndices;


namespace MITD.PMSAdmin.Domain.Model.CustomFieldTypes
{
    public interface  ICustomFieldRepository:IRepository
    { 
        CustomFieldTypeId GetNextId();
        void FindBy(EntityTypeEnum entityId, ListFetchStrategy<CustomFieldType> fs);
        void Add(CustomFieldType customFieldType);
        void UpdateCustomFieldType(CustomFieldType customFieldType);
        CustomFieldType GetById(CustomFieldTypeId customFieldTypeId);
        List<CustomFieldType> GetAllCustomField(JobId jobId);
        List<CustomFieldType> GetAllCustomField(UnitId unitId);
        List<CustomFieldType> GetAllCustomField(AbstractJobIndexId jobIndexId);
        void GetAll(EntityTypeEnum entityTypeEnum, ListFetchStrategy<CustomFieldType> fs);
        List<CustomFieldType> GetAll(EntityTypeEnum entityTypeEnum);
        IList<CustomFieldType> Find(System.Linq.Expressions.Expression<Func<CustomFieldType, bool>> predicate);
        void DeleteCustomField(CustomFieldType customField);

        CustomFieldTypeException ConvertException(Exception exp);
        CustomFieldTypeException TryConvertException(Exception exp);
        List<CustomFieldType> GetAllCustomField(AbstractUnitIndexId unitIndexId);
    }
}
