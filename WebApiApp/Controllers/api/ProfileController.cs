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
    [RoutePrefix("api/profile")]
    public class ProfileController : ApiController
    {
        FileUploadService filesvc = new FileUploadService();

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
    }
}