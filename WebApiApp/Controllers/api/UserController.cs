using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiApp.Interfaces;
using WebApiApp.Models.Domain;
using WebApiApp.Responses;
using WebApiApp.Services;

namespace WebApiApp.Controllers.api
{
    [RoutePrefix("api/users")]
    public class UserController : ApiController
    {
        private IAuthenticationService _user;
        UserService svc = new UserService();

        public UserController(IAuthenticationService user)
        {
            _user = user;
        }

        [Route("register"), HttpPost, AllowAnonymous]
        public HttpResponseMessage Post(RegisterUser model)
        {
            try
            {
                string lowerEmail = model.Email.ToLower();
                model.Email = lowerEmail;
                ItemResponse<int> resp = new ItemResponse<int>();
                int id = svc.Insert(model);
                resp.Item = id;

                return Request.CreateResponse(HttpStatusCode.OK, resp);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Route("login"), HttpPost, AllowAnonymous]
        public HttpResponseMessage Login(LoginUser model)
        {
            try
            {
                ItemResponse<bool> res = new ItemResponse<bool>();

                string lowerEmail = model.Email.ToLower();
                model.Email = lowerEmail;

                bool loggedIn = svc.LogIn(model);

                return Request.CreateResponse(HttpStatusCode.OK, res);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Route("logout"), HttpGet, AllowAnonymous]
        public HttpResponseMessage Logout()
        {
            _user.LogOut();

            return Request.CreateResponse(HttpStatusCode.OK, new SuccessResponse());
        }
    }
}