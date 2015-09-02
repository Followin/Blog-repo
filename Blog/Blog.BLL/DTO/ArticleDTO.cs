using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.DTO
{
    public class ArticleDTO
    {
        public Int32 Id { get; set; }
        public String Author { get; set; }
        public String Theme { get; set; }

        public Int32 Mark
        {
            get { return UserArticleMarks.Sum(x => x.Mark); }
        }

        public String Text { get; set; }
        public DateTime DateTime { get; set; }

        public ICollection<CommentDTO> Comments { get; set; }
        public ICollection<UserArticleMarkDTO> UserArticleMarks { get; set; }
    }
}
