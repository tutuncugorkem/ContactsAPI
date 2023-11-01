using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Core.UnitOfWorks
{
    public interface IUnitOfWork
    {
        //savechange metdonu kontrol altına almak için bu yapı kullanılır, sorgulamalarda çoklu işlermleri tek transactionda kontrol etmek ve veri çakışmasını engellemek için


        //dbcontext'in savechange ve savechangeasync metodlarını çağırıyor olacağız aşağıdakiler ile:
        Task CommitAsync();
        void Commit();
    }
}
