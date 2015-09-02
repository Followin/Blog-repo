using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Blog.BLL.Abstract;
using Blog.WEB.Logics;
using Blog.WEB.Models;

namespace Blog.WEB.Controllers
{
    
    public class AccountController : Controller
    {
        private AccountLogics _logics;

        public AccountController(IAuthService service)
        {
            _logics = new AccountLogics(service);
        }
        
        //
        // GET: /Account/

        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, String returnUrl)
        {
            if (!User.Identity.IsAuthenticated && ModelState.IsValid)
            {
                var responseObj = _logics.Login(model);
                if (responseObj != null)
                {
                    Response.Cookies.Add(responseObj.Cookie);
                    var identity = new GenericIdentity(responseObj.Name);
                    HttpContext.User = new GenericPrincipal(identity, new[] { responseObj.Role });
                    var a = User.IsInRole("Admin");
                }
                else
                {
                    ModelState.AddModelError("login/password", "Login or password is incorrent");
                    return View();
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
                HttpContext.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                _logics.Register(model);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

    }
}
