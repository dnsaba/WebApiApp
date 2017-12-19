using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiApp.Models.Domain;
using WebApiApp.Responses;
using WebApiApp.Services;

namespace WebApiApp.Controllers.api
{
    [RoutePrefix("api/urlData")]
    public class UrlDataController : ApiController
    {
        UrlDataService svc = new UrlDataService();

        [Route("get2"), HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Get2(Urls model)
        {
            try
            {
                ItemResponse<UrlData> resp = new ItemResponse<UrlData>();
                resp.Item = svc.GetCards(model.Url);
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}