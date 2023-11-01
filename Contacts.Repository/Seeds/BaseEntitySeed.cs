using Contacts.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Repository.Seeds
{
    //tablo olustururken kayıt atacağız burada
    internal class BaseEntitySeed : IEntityTypeConfiguration<BaseEntity>
    {
        public void Configure(EntityTypeBuilder<BaseEntity> builder)
        {
            builder.HasData(
                new BaseEntity {Id = 1 , Email = "gorkeme@monster.com", FullName = "görkem görkem", Phone = 1235465, Address ="bağdat caddesi no 41" },
                new BaseEntity { Id = 2, Email = "johndoe@monster.com", FullName = "john doe", Phone = 1995544, Address = "kosuyolu caddesi no 41" }


                );
        }
    }
}
