using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Core.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        
        Task<T> GetByIdAsync(int id);
        //alttaki iqueryable direkt db gitmez, tolist vb. lazımdır. whereden sonra order by vs yapılacak olabilir
        IQueryable<T> Where(Expression<Func<T, bool>> expression);
        IQueryable<T> GetAll();
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity);

    }
}
