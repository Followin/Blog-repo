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
    public class CommentRepository : IRepository<Comment>
    {
        private BlogContext _db;

        public CommentRepository(BlogContext db)
        {
            _db = db;
        }
        public IEnumerable<Comment> GetAll()
        {
            return _db.Comments.Include(x => x.User);
        }

        public Comment Get(int id)
        {
            return _db.Comments.Find(id);
        }

        public IEnumerable<Comment> Find(Func<Comment, bool> predicate)
        {
            return _db.Comments.Where(predicate).ToList();
        }

        public void Create(Comment item)
        {
            _db.Comments.Add(item);
        }

        public void Delete(int id)
        {
            var item = _db.Comments.Find(id);
            if (item != null)
                _db.Comments.Remove(item);
        }

        public void Update(Comment item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }
    }
}
