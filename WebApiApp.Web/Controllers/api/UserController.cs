using Microsoft.AspNet.Identity;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApiApp.Models;
using WebApiApp.Models.Domain;
using WebApiApp.Responses;
using WebApiApp.Services;
using WebApiApp.Services.Interfaces;

namespace WebApiApp.Controllers.api
{
    [RoutePrefix("api/users")]
    public class UserController : ApiController
    {
        private AuthRepository _repo = null;

        UserService svc = new UserService();

        public UserController()
        {
            _repo = new AuthRepository();
        }
        
        //public UserController(IAuthenticationService user)
        //{
        //    _user = user;
        //}

        [Route("register"), HttpPost, AllowAnonymous]
        public async Task<IHttpActionResult> Post(RegisterUser model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IdentityResult result = await _repo.RegisterUser(model);
            IHttpActionResult errorResult = GetErrorResult(result);
            if (errorResult != null)
            {
                return errorResult;
            }

            return Ok();
            //try
            //{
            //    string lowerEmail = model.Email.ToLower();
            //    model.Email = lowerEmail;
            //    ItemResponse<int> resp = new ItemResponse<int>();
            //    int id = svc.Insert(model);
            //    resp.Item = id;

            //    return Request.CreateResponse(HttpStatusCode.OK, resp);

            //}
            //catch (Exception ex)
            //{
            //    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            //}
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repo.Dispose();
            }

            base.Dispose(disposing);
        }

        [Route("login"), HttpPost, AllowAnonymous]
        public HttpResponseMessage Login(LoginUser model)
        {
            try
            {
                ItemResponse<bool> res = new ItemResponse<bool>();

                string email = model.Email.ToLower();
                model.Email = email;
                bool loggedIn = svc.LogIn(model.Email, model.Password);
                res.Item = loggedIn;

                return Request.CreateResponse(HttpStatusCode.OK, res);
            }
            catch (System.Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        //[Route("logout"), HttpGet, AllowAnonymous]
        //public HttpResponseMessage Logout()
        //{
        //    _user.LogOut();

        //    return Request.CreateResponse(HttpStatusCode.OK, new SuccessResponse());
        //}

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}