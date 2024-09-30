using Learning.Domain.DTO.Discount;
using Learning.Domain.Models.Account;
using Learning.Domain.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.Interfaces
{
    public interface IDiscountRepository
    {
        #region discount
        Task<FilterDiscountDTO> FilterDiscount(FilterDiscountDTO filter);
        void UpdateDiscount(Discount discount);
        Task<Discount> GetDiscountByDiscountCode(string discountCode);
        Task AddDiscount(Discount discount);
        Task<Discount> GetDiscountById(int discountId);
        Task<List<Discount>> GetAllDiscounts();
        Task<bool> IsExistCode(string code);
        Task<bool> IsExistEditCode(int discountId, string code);
        void DeleteDiscount(Discount discount);
        #endregion


        #region user discount

        Task AddUserDiscountCode(UserDiscountCode userDiscountCode);
        Task<UserDiscountCode> GetUserDiscountByUserIdAndOrderId(int userId, int orderId, int discountId);
        void RemoveUserDiscount(UserDiscountCode userDiscountCode);
        Task<UserDiscountCode> UserDiscountCodeByOrderId(int orderId);
        Task<bool> UserDiscountCodeByOrderId(int userId, int discountId);


        #endregion

        #region save
        Task Save();
        #endregion
    }
}
