using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.DAL.Entities;

namespace Blog.DAL.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        
        IRepository<Article> Articles { get; }
        IRepository<User> Users { get; }
        IRepository<Role> Roles { get; }
        IRepository<Comment> Comments { get; }
        IRepository<UserArticleMark> UserArticleMarks { get; }
        void Save();
    }
}
