using Contacts.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        //db ile ilgili işlem yapabilmek için dbcontext tanımlıyoruz, protected olma nedeni ilerde miras alırsak erişebilelim diye
        protected readonly AppDbContext _context;
        //dbset veritabanındaki tablomuza karşılık geliyor:
        private readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>(); // cok anlamadım set olayını..
        }

        //readonly: değer atayamayalım sonra diye..


        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.AnyAsync(expression);
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsNoTracking().AsQueryable();
                //as no tracking cektıgı datayı memorye almasın performanslı calıssın dıye.
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression);
        }
    }
}
