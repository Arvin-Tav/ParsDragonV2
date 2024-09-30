using Learning.Domain.DTO.Discount;
using Learning.Domain.Models.Account;
using Learning.Domain.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Application.Interfaces
{
    public interface IDiscountService
    {
        #region discount

        Task<DiscountResult> CreateDiscount(CreateDiscountDTO createDiscount);
        Task<bool> IsExistCode(string code);
        Task<EditDiscountDTO> GetDiscountForEdit(int discountId);
        Task<DiscountResult> EditDiscount(EditDiscountDTO editDiscount);
        Task<DeleteDiscountResult> DeleteDiscount(int discountId);





        Task<Discount> GetDiscountByDiscountCode(string discountCode);
        Task AddDiscount(Discount discount);
        Task<Discount> GetDiscountById(int discountId);
        Task<List<Discount>> GetAllDiscounts();
        Task<FilterDiscountDTO> FilterDiscount(FilterDiscountDTO filter);
        Task<bool> IsExistEditCode(int discountId, string code);
        #endregion


        #region user discount
        Task<DiscountUseType> UseDiscount(int orderId, string code);
        Task AddUserDiscountCode(UserDiscountCode userDiscountCode);
        Task<UserDiscountCode> GetUserDiscountByUserIdAndOrderId(int userId, int orderId, int discountId);
        void RemoveUserDiscount(UserDiscountCode userDiscountCode);
        Task<UserDiscountCode> UserDiscountCodeByOrderId(int orderId);

        #endregion
    }
}
