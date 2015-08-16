using System;
using System.Collections.Generic;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Integration.PMS.API
{
    public interface ICustomFieldServiceWrapper
    {
        void GetCustomField(Action<CustomFieldDTO, Exception> action, long id);
        void AddCustomField(Action<CustomFieldDTO, Exception> action, CustomFieldDTO jobIndex);
        void UpdateCustomField(Action<CustomFieldDTO, Exception> action, CustomFieldDTO jobIndex);
        void DeleteCustomField(Action<string, Exception> action, long id);
        //void GetAllCustomFieldes(Action<PageResultDTO<CustomFieldDTOWithActions>, Exception> action, int pageSize,
        //                         int pageIndex, Dictionary<string, string> sortBy, CustomFieldCriteria criteria);

        void GetCustomFieldEntityList(Action<List<CustomFieldEntity>, Exception> action);

        void GetAllCustomFields(Action<List<CustomFieldDTO>, Exception> action, string entityType);
        void GetAllCustomFieldsDescription(Action<List<AbstractCustomFieldDescriptionDTO>, Exception> action, string entityType);
    }
}
