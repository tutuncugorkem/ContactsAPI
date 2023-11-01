using Contacts.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Repository
{
    public class AppDbContext:DbContext
    {
        //Options alma sebebi: bu options ile veri tabanı yolunu startup dosyasından vereceğiz-program cs mi demek istdi?
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) 
        {
            
        }

        //her bir entitiye karşı bir dbset oluşturuluyor, bende tek entity var şimdilik
        public DbSet<BaseEntity> BaseEntities { get; set; }


        //configurations;
        //fluentAPI

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //alttaki metod bu interface'i implemente eden tüm configurationları görür ve okur (IEntityTypeConfiguration )
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());



            base.OnModelCreating(modelBuilder);
        }
    }
}
