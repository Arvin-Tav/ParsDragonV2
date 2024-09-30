using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.DTO.Wallet
{
    public class ChangeWalletByUserDTO
    {
        public string UserName { get; set; }
        public string Mobile { get; set; }
        public int Amount { get; set; }
    }
}
