using Learning.Application.Interfaces;
using Learning.Domain.DTO.Discount;
using Learning.Domain.Interfaces;
using Learning.Domain.Models.Account;
using Learning.Domain.Models.Order;
using Learning.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Application.Services
{
    public class DiscountService : IDiscountService
    {
        #region constructor
        private readonly IDiscountRepository _discountRepository;
        private readonly IOrderRepository _orderRepository;
        public DiscountService(IDiscountRepository discountRepository, IOrderRepository orderRepository)
        {
            _discountRepository = discountRepository;
            _orderRepository = orderRepository;
        }
        #endregion
        #region discount


        public async Task<DiscountResult> CreateDiscount(CreateDiscountDTO createDiscount)
        {
            if (await _discountRepository.IsExistCode(createDiscount.DiscountCode)) return DiscountResult.ErrorDiscountName;
            if (createDiscount.UsableCount == null && (string.IsNullOrEmpty(createDiscount.StartDate) || string.IsNullOrEmpty(createDiscount.EndDate)))
                return DiscountResult.CountAndDateIsNull;
            Discount newDiscount = new Discount();
            newDiscount.DiscountCode = createDiscount.DiscountCode;
            newDiscount.DiscountPercent = createDiscount.DiscountPercent;
            newDiscount.UsableCount = createDiscount.UsableCount;
            if (!string.IsNullOrEmpty(createDiscount.StartDate) && !string.IsNullOrEmpty(createDiscount.EndDate))
            {
                string[] std = createDiscount.StartDate.Split('/');
                newDiscount.StartDate = new DateTime(int.Parse(std[0]),
                    int.Parse(std[1]),
                    int.Parse(std[2]),
                    new PersianCalendar()
                    );

                string[] edd = createDiscount.EndDate.Split('/');
                newDiscount.EndDate = new DateTime(int.Parse(edd[0]),
                    int.Parse(edd[1]),
                    int.Parse(edd[2]),
                    new PersianCalendar()
                );
            }

            await _discountRepository.AddDiscount(newDiscount);
            await _discountRepository.Save();

            return DiscountResult.Success;
        }
        public async Task<DiscountResult> EditDiscount(EditDiscountDTO editDiscount)
        {
            var discount = await _discountRepository.GetDiscountById(editDiscount.DiscountId);
            if (discount == null) return DiscountResult.NotFound;
            if (await _discountRepository.IsExistEditCode(editDiscount.DiscountId, editDiscount.DiscountCode)) return DiscountResult.ErrorDiscountName;
            if (editDiscount.UsableCount == null && (string.IsNullOrEmpty(editDiscount.StartDate) || string.IsNullOrEmpty(editDiscount.EndDate)))
                return DiscountResult.CountAndDateIsNull;
            discount.DiscountCode = editDiscount.DiscountCode;
            discount.DiscountPercent = editDiscount.DiscountPercent;
            discount.UsableCount = editDiscount.UsableCount;
            if (!string.IsNullOrEmpty(editDiscount.StartDate) && !string.IsNullOrEmpty(editDiscount.EndDate))
            {
                string[] std = editDiscount.StartDate.Split('/');
                discount.StartDate = new DateTime(int.Parse(std[0]),
                    int.Parse(std[1]),
                    int.Parse(std[2]),
                    new PersianCalendar()
                    );

                string[] edd = editDiscount.EndDate.Split('/');
                discount.EndDate = new DateTime(int.Parse(edd[0]),
                    int.Parse(edd[1]),
                    int.Parse(edd[2]),
                    new PersianCalendar()
                );
            }

            _discountRepository.UpdateDiscount(discount);
            await _discountRepository.Save();

            return DiscountResult.Success;
        }
        public async Task<bool> IsExistCode(string code)
        {
            return await _discountRepository.IsExistCode(code);
        }
        public async Task<EditDiscountDTO> GetDiscountForEdit(int discountId)
        {
            var discount = await _discountRepository.GetDiscountById(discountId);
            if (discount == null) return null;
            return new EditDiscountDTO()
            {
                DiscountCode = discount.DiscountCode,
                DiscountId = discount.DiscountId,
                DiscountPercent = discount.DiscountPercent,
                UsableCount = discount.UsableCount,
                EndDate = discount.EndDate.ToString(),
                StartDate = discount.StartDate.ToString(),
            };
        }
        public async Task<FilterDiscountDTO> FilterDiscount(FilterDiscountDTO filter)
        {
            return await _discountRepository.FilterDiscount(filter);
        }
        public async Task<List<Discount>> GetAllDiscounts()
        {
            return await _discountRepository.GetAllDiscounts();
        }

        public async Task<DeleteDiscountResult> DeleteDiscount(int discountId)
        {
            var discount = await _discountRepository.GetDiscountById(discountId);
            if (discount == null) return DeleteDiscountResult.NotFound;
            discount.IsDelete = true;
            _discountRepository.UpdateDiscount(discount);
            await _discountRepository.Save();
            return DeleteDiscountResult.Success;
        }










        public Task AddDiscount(Discount discount)
        {
            throw new NotImplementedException();
        }






        public Task<Discount> GetDiscountByDiscountCode(string discountCode)
        {
            throw new NotImplementedException();
        }

        public Task<Discount> GetDiscountById(int discountId)
        {
            throw new NotImplementedException();
        }




        public Task<bool> IsExistEditCode(int discountId, string code)
        {
            throw new NotImplementedException();
        }


        #endregion


        #region user discount
        public async Task<DiscountUseType> UseDiscount(int orderId, string code)
        {
            Discount discount = await _discountRepository.GetDiscountByDiscountCode(code);
            if (discount == null)
            {
                return DiscountUseType.NotFound;
            }
            if (discount.StartDate != null && discount.StartDate < DateTime.Now)
            {
                return DiscountUseType.ExpierDate;
            }
            if (discount.StartDate != null && discount.StartDate >= DateTime.Now)
            {
                return DiscountUseType.ExpierDate;
            }
            if (discount.UsableCount != null && discount.UsableCount < 1)
            {
                return DiscountUseType.Finished;
            }
            var order = await _orderRepository.GetOrderById(orderId);
            if (order.IsFinaly)
            {
                return DiscountUseType.IsFinaly;
            }
            if (await _discountRepository.UserDiscountCodeByOrderId(order.UserId ,discount.DiscountId))
            {
                return DiscountUseType.UserUsed;
            }
            UserDiscountCode userDiscountCode =await _discountRepository.GetUserDiscountByUserIdAndOrderId(order.UserId, orderId, discount.DiscountId);
            if (userDiscountCode != null)
            {
                var useDiscount = await _discountRepository.GetDiscountById(userDiscountCode.DiscountId);
                if (useDiscount.UsableCount != null)
                {
                    useDiscount.UsableCount += 1;
                }
                _discountRepository.UpdateDiscount(useDiscount);
                _discountRepository.RemoveUserDiscount(userDiscountCode);
            }
            int sumOrderDetails = await _orderRepository.GetSumOrderDetailInOrderByOrderId(orderId);
            int percent = (sumOrderDetails * discount.DiscountPercent) / 100;
            order.OrderSum = sumOrderDetails - percent;
            order.DiscountPrice = percent;
            order.DiscountPercent = discount.DiscountPercent;
           _orderRepository.UpdateOrder(order);
            if (discount.UsableCount != null)
            {
                discount.UsableCount -= 1;
            }
            _discountRepository.UpdateDiscount(discount);
           await _discountRepository.AddUserDiscountCode(new UserDiscountCode()
            {
                UserId = order.UserId,
                DiscountId = discount.DiscountId,
                OrderId = orderId,
            });
            await _discountRepository.Save();
            return DiscountUseType.Succes;
        }
        public Task AddUserDiscountCode(UserDiscountCode userDiscountCode)
        {
            throw new NotImplementedException();
        }
        public Task<UserDiscountCode> GetUserDiscountByUserIdAndOrderId(int userId, int orderId, int discountId)
        {
            throw new NotImplementedException();
        }

        public void RemoveUserDiscount(UserDiscountCode userDiscountCode)
        {
            throw new NotImplementedException();
        }


        public Task<UserDiscountCode> UserDiscountCodeByOrderId(int orderId)
        {
            throw new NotImplementedException();
        }
        #endregion




    }
}
