using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Data;
using Blog.DAL.Abstract;
using Blog.DAL.EF;
using Blog.DAL.Entities;

namespace Blog.DAL.Repositories
{
    public class ArticleRepository : IRepository<Article>
    {
        private BlogContext _db ;

        public ArticleRepository(BlogContext db)
        {
            _db = db;
        }
        public IEnumerable<Article> GetAll()
        {
            return _db.Articles;
        }

        public Article Get(int id)
        {
            return _db.Articles.Find(id);
        }

        public IEnumerable<Article> Find(Func<Article, bool> predicate)
        {
            return _db.Articles.Where(predicate).ToList();
        }

        public void Create(Article item)
        {
            _db.Articles.Add(item);
        }

        public void Delete(int id)
        {
            var article = _db.Articles.Find(id);
            if (article != null)
                _db.Articles.Remove(article);
        }

        public void Update(Article item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }
    }
}
