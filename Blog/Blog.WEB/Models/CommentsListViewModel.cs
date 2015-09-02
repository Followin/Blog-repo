using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.WEB.Models
{
    public class CommentsListViewModel
    {
        public Int32 ArticleId { get; set; }
        public IEnumerable<CommentViewModel> Comments { get; set; }
        public PagingInfo Info { get; set; }
    }
}