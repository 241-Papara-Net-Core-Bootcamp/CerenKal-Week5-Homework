using AutoMapper;
using AutoMapper.Execution;
using Papara.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Core.Map
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<MemberDTO, Member>().ReverseMap();
        }
    }
}
