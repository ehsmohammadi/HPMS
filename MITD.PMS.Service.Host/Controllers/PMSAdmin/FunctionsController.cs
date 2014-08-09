using MITD.PMS.Presentation.Contracts;
using System.Web.Http;

namespace MITD.PMS.Service.Host.Controllers
{
    [Authorize]
    public class FunctionsController : ApiController
    {
        private readonly IFunctionFacadeService functionService;

        public FunctionsController(IFunctionFacadeService functionService)
        {
            this.functionService = functionService;
        } 

        public FunctionDTO PostFunction(FunctionDTO function)
        {
            return functionService.AddFunction(function);
        }
        public FunctionDTO PutFunction(FunctionDTO function)
        {
            return functionService.UpdateFunction(function);
        }

        public FunctionDTO GetFunction(long id)
        {
            return functionService.GetFunctionById(id);
        }


    }
}