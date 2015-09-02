using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.BLL.Abstract;
using Blog.BLL.Models;
using Blog.DAL.Abstract;
using Blog.DAL.Repositories;
using Ninject.Modules;

namespace Blog.BLL.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        private String connectionString;

        public ServiceModule(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public override void Load()
        {
            Bind<IUnitOfWork>().To<EFUnitOfWork>().WithConstructorArgument("connectionString", connectionString);
            
        }
    }
}
