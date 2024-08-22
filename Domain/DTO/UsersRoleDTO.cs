using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class UsersRoleDTO
    {
        public string Username {  get; set; }
        public RoleEnum IdRole { get; set; }
        public string RoleName { get {
                return IdRole switch
                {
                    RoleEnum.Pengguna => "Pengguna",
                    RoleEnum.Administrator => "Administrator",
                    RoleEnum.Manajer => "Manajer"
                };
            } }
    }
}
