using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Service.Host.Controllers.Security
{
    [Authorize]
    public class UserSecurityController : ApiController
    {
        #region Fields
        private readonly IUserServiceFacade userService;

        #endregion

        #region Constructors
        public UserSecurityController(IUserServiceFacade userService)
        {
            this.userService = userService;
        }

        #endregion

        #region Methods

        [AllowAnonymous]
        public HttpResponseMessage GetEmailVerificationSuccess(string veriCode)
        {
            var response = new HttpResponseMessage();
            if (userService.VerifyEmail(veriCode))
                response.Content = new StringContent(File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "/Response.html"));
            else
                response.Content = response.Content = new StringContent(File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "/FailedEmailResponse.html"));

            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;
        }

        public string PutUser([FromBody]ChangePasswordDTO changePassword)
        {
            return userService.ChangePassword(changePassword);
        }

        public string PutUser([FromBody]EmailDTO email, bool isEmail)
        {
            return userService.UpdateEmail(email);
        }

        #endregion
    }
}