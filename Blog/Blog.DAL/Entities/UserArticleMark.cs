using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DAL.Entities
{
    public class UserArticleMark
    {
        public Int32 Id { get; set; }

        public Int32 UserId { get; set; }
        public virtual User User { get; set; }

        public Int32 ArticleId { get; set; }
        public virtual Article Article { get; set; }

        public Int32 Mark { get; set; }
    }
}
