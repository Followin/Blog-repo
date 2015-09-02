using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.DTO
{
    public class RoleDTO
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }

        public virtual ICollection<UserDTO> Users { get; set; }
    }
}
