using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blog.WEB.Models
{
    public class ArticleViewModel
    {
        public Int32 Id { get; set; }

        [Required]
        public String Author { get; set; }

        [Required]
        public String Theme { get; set; }

        [Required]
        public String Text { get; set; }

        public Int32 Mark { get; set; }
        public DateTime DateTime { get; set; }

        public ICollection<CommentViewModel> Comments { get; set; }
    }
}