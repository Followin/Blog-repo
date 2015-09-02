using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.Abstract
{
    public interface ICommentFilter
    {
        String Filter(String message);
    }
}
