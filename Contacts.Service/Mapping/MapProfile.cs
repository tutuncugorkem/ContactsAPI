using AutoMapper;
using Contacts.Core.DTOs;
using Contacts.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Service.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<BaseEntity, BaseDto>().ReverseMap();
            CreateMap<BaseUpdateDto, BaseEntity>(); //reverse gerek yok burada


        }
    }
}
