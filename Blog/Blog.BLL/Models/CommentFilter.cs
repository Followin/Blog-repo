using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.BLL.Abstract;

namespace Blog.BLL.Models
{
    public class CommentFilter : ICommentFilter
    {
        private IEnumerable<String> words;

        public CommentFilter(params String[] words)
        {
            this.words = words;
        }

        public String Filter(String comment)
        {
            return words.Aggregate(comment, (current, word) => current.Replace(word, "###"));
        }
    }
}
