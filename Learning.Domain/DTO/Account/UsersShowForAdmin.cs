using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.DTO.Account
{
    public class UsersShowForAdmin
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public bool IsBlocked { get; set; }
        public DateTime CreateDate { get; set; }
        public int Wallet { get; set; }
        public bool IsDelete { get; set; }

    }
}
