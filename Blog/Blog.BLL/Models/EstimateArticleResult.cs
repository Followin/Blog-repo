using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.Models
{
    public class EstimateArticleResult
    {
        public EstimateStatuses Status { get; set; }
        public Int32 Mark { get; set; }
    }

    public enum EstimateStatuses
    {
        Success,
        Existing,
        Error
    }
}
