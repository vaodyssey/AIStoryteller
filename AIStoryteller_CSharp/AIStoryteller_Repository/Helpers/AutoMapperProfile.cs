using AIStoryteller_Repository.Entities;
using AIStoryteller_Repository.Payload.Request;
using AIStoryteller_Repository.Payload.Response;
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
            CreateMap<NewBookRequest,Book>()
                .ForMember(dest => dest.Pages,opt=>opt.Ignore())
                .ReverseMap();               
        }
    }
}
