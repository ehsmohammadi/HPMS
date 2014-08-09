using System.Collections.Generic;
using MITD.PMS.Presentation.Contracts;
using System.Web.Http;

namespace MITD.PMS.Service.Host.Controllers
{
    [Authorize]
    public class CustomFieldsController : ApiController
    { 
        private readonly ICustomFieldFacadeService customFieldService;

        public CustomFieldsController(ICustomFieldFacadeService customFieldService)
        {
            this.customFieldService = customFieldService;
        }

        public PageResultDTO<CustomFieldDTOWithActions> GetAllCustomFieldes(int pageSize, int pageIndex,string filter="",string sortBy="")
        {
            var queryStringCondition = new QueryStringConditions { SortBy = sortBy, Filter = filter };
            return customFieldService.GetAllCustomFieldes(pageSize, pageIndex, queryStringCondition);
        }

        public List<AbstractCustomFieldDescriptionDTO> GetAllCustomFieldes(string entityType,int  returnAbstractCustomField)
        {

            return customFieldService.GetAllCustomFieldsDescription(entityType);
        }

        public List<CustomFieldDTO> GetAllCustomFieldes(string entityType)
        {

            return customFieldService.GetAllCustomFields(entityType);
        }

        public CustomFieldDTO PostCustomField(CustomFieldDTO customField)
        {
            return customFieldService.AddCustomField(customField);
        }
        public CustomFieldDTO PutCustomField(CustomFieldDTO customField)
        {
            return customFieldService.UpdateCustomField(customField);
        }

        public CustomFieldDTO GetCustomField(long id)
        {
            return customFieldService.GetCustomFieldById(id);
        }

        public string DeleteCustomField(long id)
        {
            return customFieldService.DeleteCustomeField(id);
        }
    }
}