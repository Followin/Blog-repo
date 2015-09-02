using System.Web;
using System.Web.Mvc;
using Blog.WEB.Filters;

namespace Blog.WEB
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filter)
        {
<<<<<<< HEAD
            filter.Add(new HandleErrorAttribute());
            

=======
            filters.Add(new HandleErrorAttribute());
            //Something here
>>>>>>> master
        }
    }
}