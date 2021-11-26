using AutoMapper;
using SamuraiLegend.Application.DTOs.Authentication;
using SamuraiLegend.Infrastructure.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiLegend.Infrastructure.Persistence.Helpers
{
    public class UserMap : Profile
    {
        public UserMap()
        {
            CreateMap<AppUser, RegisterRequest>();
        }
    }
}
