using Learning.Domain.DTO.Paging;
using Learning.Domain.DTO.Wallet;
using Learning.Domain.Interfaces;
using Learning.Domain.Models.Wallet;
using Learning.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Infra.Data.Repositories
{
    public class WalletRepository : IWalletRepository
    {
        #region constructor
        private readonly LearningContext _context;
        public WalletRepository(LearningContext context)
        {
            _context = context;
        }

        #endregion

        public async Task AddWallet(Wallet wallet)
        {
            await _context.Wallets.AddAsync(wallet);
        }

        public async Task<int> BalanceUserWallet(int userId)
        {
            var enter = await _context.Wallets
                .Where(w => w.UserId == userId && w.IsPay && w.TypeId == 1)
                .Select(w => w.Amount).ToListAsync();
            var exit = await _context.Wallets
                .Where(w => w.UserId == userId && w.TypeId == 2)
                .Select(w => w.Amount).ToListAsync();

            return (enter.Sum() - exit.Sum());
        }

     
        public async Task<FilterWalletDTO> FilterWallet(FilterWalletDTO filter)
        {
            IQueryable<Wallet> query = _context.Wallets.AsQueryable().Include(i => i.User);

            #region state
            switch (filter.FilterCourseOrder)
            {
                case FilterCourseOrder.CreateDate_DES:
                    query = query.OrderByDescending(i => i.CreateDate);
                    break;
                case FilterCourseOrder.CreateDate_ASC:
                    query = query.OrderBy(i => i.CreateDate);
                    break;
            }
            switch (filter.FilterWalletType)
            {
                case FilterWalletType.All:
                    break;
                case FilterWalletType.Error:
                    query = query.Where(u => u.TypeId == 1 && u.IsPay);
                    break;
                case FilterWalletType.Deposit:
                    query = query.Where(u => u.TypeId == 2 && u.IsPay);
                    break;
                case FilterWalletType.Withdraw:
                    query = query.Where(u => u.TypeId == 1 && !u.IsPay);
                    break;
                case FilterWalletType.IsPay:
                    query = query.Where(u => u.IsPay);
                    break;
            }
            #endregion

            #region filter
            if (filter.UserId != 0 && filter.UserId != null)
                query = query.Where(u => u.UserId == filter.UserId);
            if (!string.IsNullOrEmpty(filter.Search))
                query = query.Where(i => EF.Functions.Like(i.User.UserName, $"%{filter.Search}%") ||
                 EF.Functions.Like(i.User.Email, $"%{filter.Search}%")||
                 EF.Functions.Like(i.Description, $"%{filter.Search}%"));
            #endregion
            #region paging

            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.HowManyShowPageAfterAndBefore);
            var allEntities = await query.Paging(pager).ToListAsync();

            #endregion

            filter.Charge = query.Where(i => i.TypeId == 1 && i.IsPay).Sum(i => i.Amount);
            filter.Withdraw = query.Where(i => i.TypeId == 2 && i.IsPay).Sum(i => i.Amount);

            return filter.SetPaging(pager).SetWallets(allEntities);
        }

        public async Task<Wallet> GetWalletById(int walletId)
        {
            return await _context.Wallets.AsQueryable()
                .Include(i => i.User).FirstOrDefaultAsync(i => i.WalletId == walletId);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public void UpdateWallet(Wallet wallet)
        {
            _context.Wallets.Update(wallet);
        }

    }
}
