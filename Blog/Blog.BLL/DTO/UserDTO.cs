using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.DTO
{
    public class UserDTO
    {
        public Int32 Id { get; set; }
        public String Nickname { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public DateTime BirthDate { get; set; }
        public Genders Gender { get; set; }

        public DateTime CreationTime { get; set; }

        public Int32? RoleId { get; set; }
        public virtual RoleDTO Role { get; set; }

        public virtual ICollection<CommentDTO> Comments { get; set; } 
    }

    public enum Genders
    {
        Male,
        Female
    }
}
