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
using Ninject.Modules;

namespace Blog.WEB.Filters
{

    
    public class CheckUserExistanceFilter : IActionFilter
    {

        private IAuthService _service;

        public CheckUserExistanceFilter()
        {
            _service = DependencyResolver.Current.GetService<IAuthService>();
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var authCookie = filterContext.HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (authCookie != null)
                {
                    var ticket = FormsAuthentication.Decrypt(authCookie.Value);
                    var id = Convert.ToInt32(ticket.UserData);
                    var user = _service.GetUserInfo(id);
                    if (user == null)
                    {
                        FormsAuthentication.SignOut();
                        filterContext.HttpContext.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);
                    }
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            
        }
    }
}