using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.DTO.UserPanel
{
    public class InformationUserDTO
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string UserName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public DateTime CreateDate { get; set; }
        public int Wallet { get; set; }
    }
}
