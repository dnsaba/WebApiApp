using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebApiApp.Models.Domain;

namespace WebApiApp.Controllers.api
{
    public class ProfileController : Controller
    {
        private ICouponService _couponService;
        private IAuthenticationService _authService;
        public CouponController(ICouponService couponService, IAuthenticationService authService)
        {
            _authService = authService;
            this._couponService = couponService;
        }

        [Route, HttpPost]
        public HttpResponseMessage Post(Profile model)
        {
            try
            {
                var user = _authService.GetCurrentUser();
                model.ModifiedBy = user.Id;
                int id = _couponService.Insert(model);

                ItemResponse<int> resp = new ItemResponse<int>();
                resp.Item = id;

                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                log.Error("An error occurred when attempting to insert new coupon in to table", ex);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}