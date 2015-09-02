using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DAL.Entities
{
    public class Article
    {
        public Int32 Id { get; set; }
        public String Author { get; set; }
        public String Theme { get; set; }
        public String Text { get; set; }
        public DateTime DateTime { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<UserArticleMark> UserArticleMarks { get; set; } 

    }
}
