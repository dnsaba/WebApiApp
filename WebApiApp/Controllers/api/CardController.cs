using System;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebApiApp.Models.Domain;
using WebApiApp.Responses;
using WebApiApp.Services;

namespace WebApiApp.Controllers.api
{
    [RoutePrefix("api/create")]
    public class CreateController : ApiController
    {
        FileUploadService filesvc = new FileUploadService();
        CardService svc = new CardService();

        [Route("fileUpload"), HttpPost]
        public HttpResponseMessage FilePost(EncodedImage encodedImage)
        {
            try
            {
                byte[] newBytes = Convert.FromBase64String(encodedImage.EncodedImageFile);
                UserFile model = new UserFile();
                model.UserFileName = "appimg";
                model.ByteArray = newBytes;
                model.Extension = encodedImage.FileExtension;
                model.SaveLocation = "GalleryImages";
                model.UserId = 1;

                int fileId = filesvc.Insert(model);

                ItemResponse<int> resp = new ItemResponse<int>();
                resp.Item = fileId;

                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("newcard"), HttpPost]
        public HttpResponseMessage CardInsert(Card model)
        {
            try
            {
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

        [Route, HttpGet]
        public HttpResponseMessage GetAll()
        {
            try
            {
                ItemsResponse<CardWithFile> resp = new ItemsResponse<CardWithFile>();
                resp.Items = svc.SelectAll();

                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage GetById(int id)
        {
            try
            {
                ItemResponse<CardWithFile> resp = new ItemResponse<CardWithFile>();
                resp.Item = svc.SelectById(id);

                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage Put(int id, Card model)
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