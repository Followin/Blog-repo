using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Blog.BLL.Abstract;
using Blog.BLL.Services;
using Ninject;

namespace Blog.WEB.Filters
{
    

    public class MyAuthorizeAttribute : AuthorizeAttribute
    {
        [Inject]
        public IAuthService _service { get; set; }

        

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.User.Identity.IsAuthenticated)
            {
                var authCookie = httpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (authCookie != null)
                {
                    var ticket = FormsAuthentication.Decrypt(authCookie.Value);
                    var id = Convert.ToInt32(ticket.UserData);
                    var user = _service.GetUserInfo(id);
                    if (user != null)
                    {
                        var identity = new GenericIdentity(ticket.Name);
                        httpContext.User = new GenericPrincipal(identity, new[] { user.Role.Name });
                    }
                }
            }
            return base.AuthorizeCore(httpContext);
        }


        protected override void HandleUnauthorizedRequest(AuthorizationContext context)
        {
            if (context.HttpContext.Request.IsAjaxRequest())
            {
                var urlHelper = new UrlHelper(context.RequestContext);
                context.HttpContext.Response.StatusCode = 302;
                context.Result = new JsonResult
                {
                    Data = new
                    {
                        Error = "NotAuthorized",
                        LoginUrl = urlHelper.Action("Login", "Account")
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                base.HandleUnauthorizedRequest(context);
            }
        }
    }
}