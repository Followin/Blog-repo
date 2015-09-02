using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.ClientServices;
using System.Web.Mvc;
using System.Web.Security;
using Blog.BLL.Abstract;
using Blog.BLL.Services;
using Ninject;
using Ninject.Modules;

namespace Blog.WEB.Filters
{

    
    public class ProvideRolesFilter : IAuthorizationFilter
    {

        private IAuthService _service;

        public ProvideRolesFilter()
        {
            _service = DependencyResolver.Current.GetService<IAuthService>();
        }

        

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var request = filterContext.RequestContext.HttpContext;

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
                        request.User = new GenericPrincipal(identity, new[] { user.Role.Name });
                    }
                }
            }
        }
    }
}