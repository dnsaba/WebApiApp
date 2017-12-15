using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.ApplicationServices;
using System.Web.Http;
using WebApiApp.Models.Domain;
using WebApiApp.Responses;
using WebApiApp.Services;

namespace WebApiApp.Controllers.api
{
    [RoutePrefix("api/people")]
    public class ProfileController : ApiController
    {
        PeopleService svc = new PeopleService();

        [Route, HttpGet]
        public HttpResponseMessage GetAll()
        {
            try
            {
                ItemsResponse<People> resp = new ItemsResponse<People>();
                resp.Items = svc.SelectAll();

                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                //log.Error("Failed to get coupon by id", ex);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}