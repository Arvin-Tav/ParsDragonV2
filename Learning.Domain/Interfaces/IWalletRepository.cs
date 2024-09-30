using Learning.Domain.DTO.Wallet;
using Learning.Domain.Models.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.Interfaces
{
    public interface IWalletRepository
    {
        Task<FilterWalletDTO> FilterWallet(FilterWalletDTO filter);
        Task<int> BalanceUserWallet(int userId);
        Task AddWallet(Wallet wallet);
        Task<Wallet> GetWalletById(int walletId);
        void UpdateWallet(Wallet wallet);
        Task Save();
    }
}
