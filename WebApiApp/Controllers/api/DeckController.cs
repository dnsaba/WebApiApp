using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiApp.Models.Domain;
using WebApiApp.Responses;
using WebApiApp.Services;

namespace WebApiApp.Controllers.api
{
    //[RoutePrefix("api/decks")]
    //public class DeckController : ApiController
    //{
    //    Deckervice svc = new DeckService();

    //    [Route, HttpPost]
    //    public HttpResponseMessage Post(List<Card> model)
    //    {
    //        try
    //        {
    //            int id = svc.Insert(model);

    //            ItemResponse<int> resp = new ItemResponse<int>();
    //            resp.Item = id;

    //            return Request.CreateResponse(HttpStatusCode.OK, resp);
    //        }
    //        catch (Exception ex)
    //        {
    //            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
    //        }
    //    }
    //}
}