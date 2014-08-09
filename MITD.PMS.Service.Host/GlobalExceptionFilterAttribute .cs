using System.Web.Http.Filters;
using MITD.PMS.Service.Host;

public class GlobalExceptionFilterAttribute : ExceptionFilterAttribute
{

    public override void OnException(HttpActionExecutedContext context)
    {
        context.Response = PMSApiExceptionAdapter.ConverToHttpResponse(context);
    }

    
}





