using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.DTO
{
    public class UserArticleMarkDTO
    {
        public Int32 Id { get; set; }

        public Int32 UserId { get; set; }
        public virtual UserDTO User { get; set; }

        public Int32 ArticleId { get; set; }
        public virtual ArticleDTO Article { get; set; }

        public Int32 Mark { get; set; }
    }
}
