using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.DAL.Abstract;
using Blog.DAL.EF;
using Blog.DAL.Entities;

namespace Blog.DAL.Repositories
{
    public class UserArticleMarkRepository : IRepository<UserArticleMark>
    {
        private BlogContext _db;

        public UserArticleMarkRepository(BlogContext db)
        {
            _db = db;
        }
        public IEnumerable<UserArticleMark> GetAll()
        {
            return _db.UserArticleMarks;
        }

        public UserArticleMark Get(int id)
        {
            return _db.UserArticleMarks.Find(id);
        }

        public IEnumerable<UserArticleMark> Find(Func<UserArticleMark, bool> predicate)
        {
            return _db.UserArticleMarks.Where(predicate);
        }

        public void Create(UserArticleMark item)
        {
            _db.UserArticleMarks.Add(item);
        }

        public void Delete(int id)
        {
            var item = _db.UserArticleMarks.Find(id);
            if (item != null)
                _db.UserArticleMarks.Remove(item);
        }

        public void Update(UserArticleMark item)
        {
            _db.Entry(item).State = EntityState.Modified;
            
        }
    }
}
