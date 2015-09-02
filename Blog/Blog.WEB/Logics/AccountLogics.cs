using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using AutoMapper;
using Blog.BLL.Abstract;
using Blog.BLL.DTO;
using Blog.WEB.Models;

namespace Blog.WEB.Logics
{
    public class AccountLogics
    {
        private IAuthService _service;

        public AccountLogics(IAuthService service)
        {
            _service = service;
        }
        public void Register(UserViewModel user)
        {
            Mapper.CreateMap<UserViewModel, UserDTO>();
            _service.RegisterUser(Mapper.Map<UserDTO>(user));
        }

        public LoginResponse Login(LoginViewModel model)
        {
            var userId = _service.ValidateUser(model.Login, model.Password);
            
            if (userId.HasValue)
            {
                var userInfo = _service.GetUserInfo(userId.Value);
                var ticket = new FormsAuthenticationTicket(1, userInfo.Nickname, DateTime.Now, DateTime.Now.AddDays(1),
                    model.Persistent, userId.Value.ToString());
                var ticketStr = FormsAuthentication.Encrypt(ticket);
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, ticketStr);

                return new LoginResponse { Name = userInfo.Nickname, Cookie = cookie, Role = userInfo.Role.Name };
            }
            return null;
        }
    }

    public class LoginResponse
    {
        public String Name { get; set; }
        public HttpCookie Cookie { get; set; }
        public String Role { get; set; }
    }
}