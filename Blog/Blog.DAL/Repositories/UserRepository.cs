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
    public class UserRepository : IRepository<User>
    {
        private BlogContext _db;

        public UserRepository(BlogContext db)
        {
            _db = db;
        }

        public IEnumerable<User> GetAll()
        {
            return _db.Users;
        }

        public User Get(int id)
        {
            return _db.Users.Find(id);
        }

        public IEnumerable<User> Find(Func<User, bool> predicate)
        {
            return _db.Users.Where(predicate).ToList();
        }

        public void Create(User item)
        {
            _db.Users.Add(item);
        }

        public void Delete(int id)
        {
            var visitor = _db.Users.Find(id);
            if (visitor != null)
                _db.Users.Remove(visitor);
        }

        public void Update(User item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }
    }
}
