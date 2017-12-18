﻿using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Web;
using WebApiApp.Models.Interfaces;

namespace WebApiApp.Services
{
    public class OwinAuthenticationService : IAuthenticationService
    {
        private static string _title = null;

        static OwinAuthenticationService()
        {
            _title = GetApplicationName();
        }

        public OwinAuthenticationService()
        {

        }

        public void LogIn(IUserAuthData user, params Claim[] extraClaims)
        {
            ClaimsIdentity identity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie
                                                            , ClaimsIdentity.DefaultNameClaimType
                                                            , ClaimsIdentity.DefaultRoleClaimType);

            identity.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider"
                                , _title
                                , ClaimValueTypes.String));

            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(), ClaimValueTypes.String));

            identity.AddClaim(new Claim(ClaimTypes.Email, user.Email, ClaimValueTypes.String));

            identity.AddClaims(extraClaims);

            AuthenticationProperties props;


            props = new AuthenticationProperties
            {
                IsPersistent = true,
                IssuedUtc = DateTime.UtcNow,
                AllowRefresh = true
            };


            HttpContext.Current.GetOwinContext().Authentication.SignIn(props, identity);
        }

        public void LogOut()
        {
            Microsoft.Owin.IOwinContext owinContext = System.Web.HttpContext.Current.Request.GetOwinContext();
            IEnumerable<AuthenticationDescription> authenticationTypes = owinContext.Authentication.GetAuthenticationTypes();
            owinContext.Authentication.SignOut(authenticationTypes.Select(o => o.AuthenticationType).ToArray());

            //HttpContext.Current.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }


        public IUserAuthData GetCurrentUser()
        {
            return HttpContext.Current.User.Identity.GetCurrentUser();
        }

        private static string GetApplicationName()
        {
            var entryAssembly = Assembly.GetExecutingAssembly();

            var titleAttribute = entryAssembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false).FirstOrDefault() as AssemblyTitleAttribute;
            //if (string.IsNullOrWhiteSpace(applicationTitle))
            //{
            //    applicationTitle = entryAssembly.GetName().Name;
            //}
            return titleAttribute == null ? entryAssembly.GetName().Name : titleAttribute.Title;

        }

    }
}
