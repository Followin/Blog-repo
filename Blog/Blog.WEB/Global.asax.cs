using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using Blog.BLL.Abstract;
using Blog.WEB.Infrastructure;

namespace Blog.WEB
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {


        private IAuthService _service;

        public MvcApplication()
        {
            _service = DependencyResolver.Current.GetService<IAuthService>();
        }


        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ModelMetadataProviders.Current = new MyMetadataProvider();
            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;


        }

        protected void Session_Start()
        {
            var httpContext = HttpContext.Current;

            if (httpContext.User.Identity.IsAuthenticated)
            {
                var authCookie = httpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (authCookie != null)
                {
                    var ticket = FormsAuthentication.Decrypt(authCookie.Value);
                    var id = Convert.ToInt32(ticket.UserData);
                    var user = _service.GetUserInfo(id);
                    if (user == null)
                    {
                        FormsAuthentication.SignOut();
                        httpContext.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);
                    }
                }
            }
        }

        protected void Application_AuthenticateRequest()
        {
            var request = HttpContext.Current;

            if (request.User != null)
            {
                if (request.User.Identity.IsAuthenticated)
                {
                    var authCookie = request.Request.Cookies[FormsAuthentication.FormsCookieName];
                    if (authCookie != null)
                    {
                        var ticket = FormsAuthentication.Decrypt(authCookie.Value);
                        var id = Convert.ToInt32(ticket.UserData);
                        var user = _service.GetUserInfo(id);
                        var a = request.User.IsInRole("Admin");
                        if (user != null)
                        {
                            var identity = new GenericIdentity(ticket.Name);
                            request.User = new GenericPrincipal(identity, new[] {user.Role.Name});
                        }
                    }
                }
            }
        }
    }
}