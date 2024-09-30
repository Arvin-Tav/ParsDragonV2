using Learning.Domain.DTO.Order;
using Learning.Domain.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Application.Interfaces
{
    public interface IOrderService
    {
        #region order
        Task AddOrder(Order order);
        Task<int> AddOrderCourse(int userId, int courseId);
        Task<int> AddOrderOnlineClass(int userId, int onlineClassId);
        Task UpdatePriceOrder(int orderId);
        Task<FilterOrderDTO> FilterOrder(FilterOrderDTO filter);
        Task<Order> GetOrderById(int orderId);
        Task<Order> ShowOrderDetails(int orderId);
        Task UpdateOrder(Order order);


        #endregion

        #region userPanel
        Task<Order> GetOrderCourseForUserPanel(int userId, int orderId);
        Task<Order> GetOrderOnlineClassForUserPanel(int userId, int orderId);
        Task<Order> GetOrderByIdAndUserId(int userId, int orderId);
        Task RemoveOrderDetail(int orderId, int detailId);
        Task<FinalyOrderResult> FinalyOrder(int userId, int orderId, string ip);
        Task<List<Order>> GetUserOrders(int userId);
        Task<List<Order>> GetUserOrdersOnlineClass(int userId);
        #endregion
    }
}
