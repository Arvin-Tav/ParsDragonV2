using Learning.Domain.DTO.Wallet;
using Learning.Domain.Models.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Application.Interfaces
{
    public interface IWalletService
    {
        Task<int> BalanceUserWallet(int userId);
        Task<int> ChargeWallet(int userId, int amount, string description, bool isPay = false, string ip = "");
        Task<int> AddWallet(Wallet wallet);
        Task<Wallet> GetWalletById(int walletId);
        void UpdateWallet(Wallet wallet);
        Task<FilterWalletDTO> FilterWallet(FilterWalletDTO filter);
    }
}
