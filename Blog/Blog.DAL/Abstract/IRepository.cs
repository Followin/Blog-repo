using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DAL.Abstract
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T Get(Int32 id);
        IEnumerable<T> Find(Func<T, Boolean> predicate);
        void Create(T item);
        void Delete(Int32 id);
        void Update(T item);
    }
}
