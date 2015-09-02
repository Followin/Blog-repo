using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.BLL.DTO;

namespace Blog.BLL.Abstract
{
    public interface IAuthService
    {
        void RegisterUser(UserDTO userDTO);
        Int32? ValidateUser(String username, String password);
        UserDTO GetUserInfo(Int32 id);
    }
}
