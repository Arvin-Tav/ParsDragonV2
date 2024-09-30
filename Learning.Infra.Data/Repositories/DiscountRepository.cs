using Learning.Domain.DTO.Discount;
using Learning.Domain.DTO.Paging;
using Learning.Domain.Interfaces;
using Learning.Domain.Models.Account;
using Learning.Domain.Models.Order;
using Learning.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Infra.Data.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        #region constructor
        private readonly LearningContext _context;
        public DiscountRepository(LearningContext context)
        {
            _context = context;
        }

        #endregion
        #region discount
        public async Task AddDiscount(Discount discount)
        {
            await _context.Discounts.AddAsync(discount);
        }
        public void DeleteDiscount(Discount discount)
        {
            _context.Discounts.Remove(discount);
        }

        public async Task<FilterDiscountDTO> FilterDiscount(FilterDiscountDTO filter)
        {
            IQueryable<Discount> query = _context.Discounts.AsQueryable();

            #region state
            switch (filter.FilterDiscountOrder)
            {
                case FilterDiscountOrder.CreateDate_DES:
                    query = query.OrderByDescending(i => i.CreateDate);
                    break;
                case FilterDiscountOrder.CreateDate_ASC:
                    query = query.OrderBy(i => i.CreateDate);
                    break;
            }
            #endregion

            #region filter
            if (!string.IsNullOrEmpty(filter.Search))
                query = query.Where(i => EF.Functions.Like(i.DiscountCode, $"%{filter.Search}%"));
            #endregion
            #region paging
            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.HowManyShowPageAfterAndBefore);
            var allEntities = await query.Paging(pager).ToListAsync();
            #endregion
            return filter.SetPaging(pager).SetDiscount(allEntities);
        }
        public async Task<List<Discount>> GetAllDiscounts()
        {
            return await _context.Discounts.AsQueryable().ToListAsync();
        }
        public async Task<Discount> GetDiscountByDiscountCode(string discountCode)
        {
            return await _context.Discounts.AsQueryable()
                .FirstOrDefaultAsync(i => i.DiscountCode == discountCode);

        }
        public async Task<Discount> GetDiscountById(int discountId)
        {
            return await _context.Discounts.AsQueryable().FirstOrDefaultAsync(i => i.DiscountId == discountId);
        }
        public async Task<bool> IsExistCode(string code)
        {
            return await _context.Discounts.AnyAsync(u => u.DiscountCode == code);

        }
        public async Task<bool> IsExistEditCode(int discountId, string code)
        {
            return await _context.Discounts
                .AnyAsync(u => u.DiscountId != discountId && u.DiscountCode == code);

        }
        public void UpdateDiscount(Discount discount)
        {
            _context.Discounts.Update(discount);
        }
        #endregion

        #region user discount
        public async Task AddUserDiscountCode(UserDiscountCode userDiscountCode)
        {
            await _context.UserDiscountCodes.AddAsync(userDiscountCode);
        }
        public void RemoveUserDiscount(UserDiscountCode userDiscountCode)
        {
            _context.UserDiscountCodes.Remove(userDiscountCode);
        }
        public async Task<UserDiscountCode> GetUserDiscountByUserIdAndOrderId(int userId, int orderId, int discountId)
        {
            return await _context.UserDiscountCodes
                .AsQueryable()
                .FirstOrDefaultAsync(i => i.UserId == userId &&
                i.OrderId == orderId && i.DiscountId != discountId);

        }
        public async Task<UserDiscountCode> UserDiscountCodeByOrderId(int orderId)
        {
            return await _context.UserDiscountCodes
                .AsQueryable().FirstOrDefaultAsync(o => o.OrderId == orderId);
        }
        public async Task<bool> UserDiscountCodeByOrderId(int userId, int discountId)
        {
            return await _context.UserDiscountCodes.AnyAsync(o => o.UserId == userId && o.DiscountId == discountId);
        }

        #endregion
        #region save
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
        #endregion

    }
}
