using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Providers.Entities;

namespace Blog.WEB.Models
{
    public class CommentViewModel
    {
        public Int32 Id { get; set; }

        public Int32 UserId { get; set; }
        public virtual UserViewModel User { get; set; }

        public Int32? ArticleId { get; set; }
        public ArticleViewModel Article { get; set; }

        [DataType(DataType.MultilineText)]
        [Required]
        public String Text { get; set; }
    }
}