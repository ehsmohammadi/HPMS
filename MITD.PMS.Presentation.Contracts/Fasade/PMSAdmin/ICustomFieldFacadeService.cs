using System.Collections.Generic;
using MITD.Core;

namespace MITD.PMS.Presentation.Contracts
{
    public interface ICustomFieldFacadeService:IFacadeService
    {
        PageResultDTO<CustomFieldDTOWithActions> GetAllCustomFieldes(int pageSize, int pageIndex, QueryStringConditions queryStringConditions);
        CustomFieldDTO AddCustomField(CustomFieldDTO customField);
        CustomFieldDTO UpdateCustomField(CustomFieldDTO customField);
        string DeleteCustomeField(long id);
        CustomFieldDTO GetCustomFieldById(long id);
        List<CustomFieldDTO> GetAllCustomFields(string entityType);
        List<AbstractCustomFieldDescriptionDTO> GetAllCustomFieldsDescription(string entityType);
        List<CustomFieldEntity> GetAllCustomFieldEntityType();
    }
}
