using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.DAL.Abstract;
using Blog.DAL.EF;
using Blog.DAL.Entities;

namespace Blog.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private BlogContext _db;
        private CommentRepository _commentRepository;
        private ArticleRepository _articleRepository;
        private UserRepository userRepository;
        private RoleRepository _roleRepository;
        private UserArticleMarkRepository _userArticleMarkRepository;

        public EFUnitOfWork(String connectionString)
        {
            _db = new BlogContext(connectionString);
        }


        public IRepository<Comment> Comments
        {
            get { return _commentRepository ?? (_commentRepository = new CommentRepository(_db)); }
        }

        public IRepository<Article> Articles
        {
            get { return _articleRepository ?? (_articleRepository = new ArticleRepository(_db)); }
        }


        public IRepository<User> Users
        {
            get { return userRepository ?? (userRepository = new UserRepository(_db)); }
        }

        public IRepository<Role> Roles
        {
            get { return _roleRepository ?? (_roleRepository = new RoleRepository(_db)); }
        }

        public IRepository<UserArticleMark> UserArticleMarks
        {
            get
            {
                return _userArticleMarkRepository ?? (_userArticleMarkRepository = new UserArticleMarkRepository(_db));
            }
        }

        public void Save()
        {
            _db.SaveChanges();
        }


        private Boolean _disposed = false;
        public void Dispose(Boolean disposing)
        {
            if(!_disposed && disposing)
                _db.Dispose();
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
