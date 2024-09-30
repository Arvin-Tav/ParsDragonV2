using Learning.Application.Interfaces;
using Learning.Domain.DTO.Wallet;
using Learning.Domain.Interfaces;
using Learning.Domain.Models.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Application.Services
{
    public class WalletService : IWalletService
    {
        #region constructor
        private readonly IWalletRepository _walletRepository;
        public WalletService(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        #endregion
        public async Task<int> AddWallet(Wallet wallet)
        {
            await _walletRepository.AddWallet(wallet);
            await _walletRepository.Save();
            return wallet.WalletId;
        }

        public Task<int> BalanceUserWallet(int userId)
        {
            return _walletRepository.BalanceUserWallet(userId);
        }

        public async Task<int> ChargeWallet(int userId, int amount, string description, bool isPay = false, string ip = "")
        {
            Wallet wallet = new Wallet()
            {
                Description = description,
                Amount = amount,
                IsPay = isPay,
                TypeId = 1,
                UserId = userId,
                Ip = ip
            };
            await _walletRepository.AddWallet(wallet);
            await _walletRepository.Save();
            return wallet.WalletId;
        }

        

        public async Task<FilterWalletDTO> FilterWallet(FilterWalletDTO filter)
        {
            return await _walletRepository.FilterWallet(filter);
        }

        public async Task<Wallet> GetWalletById(int walletId)
        {
            return await _walletRepository.GetWalletById(walletId);
        }



        public async void UpdateWallet(Wallet wallet)
        {
            _walletRepository.UpdateWallet(wallet);
            await _walletRepository.Save();
        }


    }
}
