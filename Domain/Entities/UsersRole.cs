using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UsersRole : BaseEntity
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public int IdRole { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public enum RoleEnum
    {
        Pengguna,
        Administrator,
        Manajer
    }
}
