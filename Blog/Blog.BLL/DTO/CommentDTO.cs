using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.DTO
{
    public class CommentDTO
    {
        public Int32 Id { get; set; }
        public String Author { get; set; }
        public String Text { get; set; }

        public Int32? ArticleId { get; set; }
        public ArticleDTO Article { get; set; }

        public Int32 UserId { get; set; }
        public UserDTO User { get; set; }
    }
}
