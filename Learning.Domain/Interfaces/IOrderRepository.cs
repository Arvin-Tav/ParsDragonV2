using Learning.Domain.DTO.Order;
using Learning.Domain.Models.Account;
using Learning.Domain.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task<FilterOrderDTO> FilterOrder(FilterOrderDTO filter);
        Task AddOrder(Order order);
        Task<Order> GetOrderCourseNotFinalyByUserId(int userId);
        Task<Order> GetOrderOnlineClassNotFinalyByUserId(int userId);
        Task AddOrderDetail(OrderDetail orderDetail);
        Task<Order> GetOrderById(int orderId);
        Task<OrderDetail> GetOrderDetailByOrderIdAndCourseId(int orderId,int courseId);
        Task<OrderDetail> GetOrderDetailById(int orderDetailId);
        Task<OrderDetail> GetOrderDetailByIdAndOrderId(int orderDetailId, int orderId);
        Task<int> GetSumOrderDetailInOrderByOrderId(int orderId);
        Task<Order> ShowOrderDetails(int orderId);
        Task<bool> IsExistOrderInOrderDetail(int orderId);
        void UpdateOrder(Order order);
        void UpdateOrderDetail(OrderDetail orderDetail);
        Task<Order> GetOrderByIdAndUserId(int userId, int orderId);
        Task<Order> GetOrderForUserPanel(int userId, int orderId);
        Task<Order> GetOrderOnlineClassForUserPanel(int userId, int orderId);
        void RemoveOrderDetail(OrderDetail orderDetail);
        void RemoveOrder(Order order);

        Task<List<Order>> GetUserOrders(int userId);
        Task<List<Order>> GetUserOrdersOnlineClass(int userId);

        Task Save();
    }
}
