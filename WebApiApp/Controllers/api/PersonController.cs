using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiApp.Models.Domain;
using WebApiApp.Responses;
using WebApiApp.Services;

namespace WebApiApp.Controllers.api
{
    [RoutePrefix("api/people")]
    public class PersonController : ApiController
    {
        PersonService svc = new PersonService();

        [Route, HttpPost]
        public HttpResponseMessage Post(PersonInsertRequest model)
        {
            try
            {
                //var user = _authService.GetCurrentUser();
                //model.ModifiedBy = user.Id;
                int id = svc.Insert(model);

                ItemResponse<int> resp = new ItemResponse<int>();
                resp.Item = id;

                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route, HttpGet]
        public HttpResponseMessage GetAll()
        {
            try
            {
                ItemsResponse<Person> resp = new ItemsResponse<Person>();
                resp.Items = svc.SelectAll();

                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                //log.Error("Failed to get coupon by id", ex);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage GetById(int id)
        {
            try
            {
                ItemResponse<Person> resp = new ItemResponse<Person>();
                resp.Item = svc.SelectById(id);

                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage Put(int id, Person model)
        {
            try
            {
                //var user = _authService.GetCurrentUser();
                //model.ModifiedBy = user.Id;
                svc.Update(model);
                SuccessResponse resp = new SuccessResponse();
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                svc.Delete(id);
                SuccessResponse resp = new SuccessResponse();
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}