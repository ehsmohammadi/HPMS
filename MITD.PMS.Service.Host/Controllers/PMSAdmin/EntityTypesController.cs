using System.Collections.Generic;
using MITD.PMS.Presentation.Contracts;
using System.Web.Http;

namespace MITD.PMS.Service.Host.Controllers
{
    [Authorize]
    public class EntityTypesController : ApiController
    {
        private readonly ICustomFieldFacadeService customFieldService;

        public EntityTypesController(ICustomFieldFacadeService customFieldService)
        {
            this.customFieldService = customFieldService;
        }

        public List<CustomFieldEntity> GetAllCustomFieldEntityType()
        {

            return customFieldService.GetAllCustomFieldEntityType();
        }
    }
}