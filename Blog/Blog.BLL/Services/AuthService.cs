using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using AutoMapper;
using Blog.BLL.Abstract;
using Blog.BLL.DTO;
using Blog.DAL.Abstract;
using Blog.DAL.Entities;

namespace Blog.BLL.Services
{
    public class AuthService : IAuthService
    {
        private IUnitOfWork _db;

        public AuthService(IUnitOfWork db)
        {
            _db = db;
        }


        public void RegisterUser(UserDTO userDTO)
        {
            Mapper.CreateMap<UserDTO, User>();
            userDTO.Password = Crypto.HashPassword(userDTO.Password);
            userDTO.CreationTime = DateTime.Now;
            if (userDTO.RoleId == null && userDTO.Role == null)
            {
                if (userDTO.Nickname == "Admin")
                {
                    var role = _db.Roles.GetAll().FirstOrDefault(x => x.Name == "Admin");
                    if (role != null)
                    {
                        userDTO.RoleId = role.Id;
                    }
                }
                else
                {
                    var role = _db.Roles.GetAll().FirstOrDefault(x => x.Name == "Member");
                    if (role != null)
                    {
                        userDTO.RoleId = role.Id;
                    }
                }
            }
            _db.Users.Create(Mapper.Map<User>(userDTO));
            _db.Save();

        }

        public int? ValidateUser(string username, string password)
        {
            
            var user = _db.Users.Find(_ => _.Nickname == username).FirstOrDefault();
            if (user != null)
            {
                var isValid = Crypto.VerifyHashedPassword(user.Password, password);
                if (isValid) return user.Id;
            }
            return null;
        }

        

        public UserDTO GetUserInfo(int id)
        {
            var user = _db.Users.Get(id);
            if (user != null)
            {
                Mapper.CreateMap<User, UserDTO>();
                Mapper.CreateMap<Role, RoleDTO>();
                return Mapper.Map<UserDTO>(user);
            }
            return null;
        }
    }
}
