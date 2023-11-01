using Contacts.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Repository.Configurations
{
    internal class BaseEntityConfiguration : IEntityTypeConfiguration<BaseEntity>
    {
        //entitiy veya appdbcontext içinde kirlilik oluşmasın diye entity bazlı configuration classları olusturacagız
        public void Configure(EntityTypeBuilder<BaseEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.FullName).IsRequired().HasMaxLength(20);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(30);
            builder.Property(x => x.Address).IsRequired().HasMaxLength(50);

            builder.ToTable("Kayıtlar"); //tablo ismi verilebilir burada
        }
    }
}
