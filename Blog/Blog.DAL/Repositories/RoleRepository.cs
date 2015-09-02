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
    public class RoleRepository : IRepository<Role>
    {
        private BlogContext _db;

        public RoleRepository(BlogContext db)
        {
            _db = db;
        }
        public IEnumerable<Role> GetAll()
        {
            return _db.Roles;
        }

        public Role Get(int id)
        {
            return _db.Roles.Find(id);
        }

        public IEnumerable<Role> Find(Func<Role, bool> predicate)
        {
            return _db.Roles.Where(predicate).ToList();
        }

        public void Create(Role item)
        {
            _db.Roles.Add(item);
        }

        public void Delete(int id)
        {
            var item = _db.Roles.Find(id);
            if (item != null)
                _db.Roles.Remove(item);
        }

        public void Update(Role item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }
    }
}
