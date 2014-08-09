using System.Collections.Generic;
using MITD.Presentation;
using MITD.PMS.Presentation.Contracts;
using System;

namespace MITD.PMS.Presentation.Logic
{
    public interface ICustomFieldServiceWrapper : IServiceWrapper
    {
        void GetCustomField(Action<CustomFieldDTO, Exception> action, long id);
        void AddCustomField(Action<CustomFieldDTO, Exception> action, CustomFieldDTO jobIndex);
        void UpdateCustomField(Action<CustomFieldDTO, Exception> action, CustomFieldDTO jobIndex);
        void DeleteCustomField(Action<string, Exception> action, long id);
        void GetAllCustomFieldes(Action<PageResultDTO<CustomFieldDTOWithActions>, Exception> action, int pageSize,
                                 int pageIndex, Dictionary<string, string> sortBy, CustomFieldCriteria criteria);

        void GetCustomFieldEntityList(Action<List<CustomFieldEntity>, Exception> action);

        void GetAllCustomFields(Action<List<CustomFieldDTO>, Exception> action,string entityType );
        void GetAllCustomFieldsDescription(Action<List<AbstractCustomFieldDescriptionDTO>, Exception> action, string entityType);
    }
}
