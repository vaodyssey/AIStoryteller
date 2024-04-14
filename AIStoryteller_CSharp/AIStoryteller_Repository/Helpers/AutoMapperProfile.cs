using AIStoryteller_Repository.DTO;
using AIStoryteller_Repository.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIStoryteller_Repository.Helpers
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<NewBookDto,Book>()
                .ForMember(dest => dest.Pages,opt=>opt.Ignore())
                .ReverseMap();
        }
    }
}
