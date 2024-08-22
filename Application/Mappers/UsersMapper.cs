using AutoMapper;
using Domain.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers
{
    public class UsersMapper : Profile
    {
        public UsersMapper()
        {
            CreateMap<UsersParamsDTO, Users>();
            CreateMap<Users, UsersDTO>();

            CreateMap<UsersRoleParamsDTO, UsersRole>();
            CreateMap<UsersRoleParamsDTO, UsersRoleDTO>();
            CreateMap<UsersRole, UsersRoleDTO>();
        }
    }
}
