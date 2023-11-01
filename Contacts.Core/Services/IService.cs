using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Core.Services
{
    public interface IService<T> where T : class
    {
        //Igeneric repository'e göre dönüş tiplerimizde değişiklik olacak burada..ileride
        //ek olarak business logicler validationlar exceptionlar REPO katmanı değil SERVİCE katmanında olacağı için ayırdık
        //kodlar şimdilik igenericrepository ile aynı, ama ilerde repositoryleri ayırmak gerekirse diye şimdiden best practise için yaptık

        Task<T> GetByIdAsync(int id);

        //alttaki iqueryable direkt db gitmez, tolist vb. lazımdır. whereden sonra order by vs yapılacak olabilir
        IQueryable<T> Where(Expression<Func<T, bool>> expression);

        Task<IEnumerable<T>> GetAllAsync();


        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);

        Task<T> AddAsync(T entity);


        //Iservice'de veritabanına bu değişiklikleri yansıtacağımız için alttaki metodları async yapmamız gerekiyor bu katmanda
        Task UpdateAsync(T entity);

        Task RemoveAsync(T entity);



    }
}
