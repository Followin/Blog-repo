using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.BLL.Abstract;
using Blog.BLL.Models;
using Blog.BLL.Services;
using Blog.WEB.Filters;
using Ninject;
using Ninject.Web.Mvc.FilterBindingSyntax;

namespace Blog.WEB.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<ICommentFilter>().To<CommentFilter>().WithConstructorArgument("words", new String[] {"fuck"});
            kernel.Bind<IBlogService>().To<BlogService>();
            kernel.Bind<IAuthService>().To<AuthService>();
        }
    }
}