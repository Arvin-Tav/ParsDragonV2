using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.DTO.Wallet
{
    public class ChangeWalletByAdminDTO: ChangeWalletByUserDTO
    {
        public string AdminName { get; set; }
        public string AdminMobile { get; set; }
        public bool IsCharge { get; set; }

    }
}
