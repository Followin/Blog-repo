using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DAL.Entities
{
    public class User
    {
        public Int32 Id { get; set; }
        public String Nickname { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public DateTime BirthDate { get; set; }
        public Genders Gender { get; set; }

        public DateTime CreationTime { get; set; }

        public Int32? RoleId { get; set; }
        public virtual Role Role { get; set; }

        public virtual ICollection<UserArticleMark> UserArticleMarks { get; set; }
        public virtual ICollection<Comment> Comments { get; set; } 
        
    }

    public enum Genders
    {
        Male,
        Female
    }
}
