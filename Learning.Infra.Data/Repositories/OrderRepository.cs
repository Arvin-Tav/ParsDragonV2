using Learning.Domain.DTO.Order;
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
    public class OrderRepository : IOrderRepository
    {

        #region constructor
        private readonly LearningContext _context;
        public OrderRepository(LearningContext context)
        {
            _context = context;
        }

        #endregion

        public async Task AddOrder(Order order)
        {
            await _context.Orders.AddAsync(order);
        }

        public async Task AddOrderDetail(OrderDetail orderDetail)
        {
            await _context.OrderDetails.AddAsync(orderDetail);
        }


        public async Task<FilterOrderDTO> FilterOrder(FilterOrderDTO filter)
        {
            IQueryable<Order> query = _context.Orders.AsQueryable()
                .Include(u => u.User)
                .Include(i => i.OrderDetails).ThenInclude(i => i.OnlineClass)
                .Include(i => i.OrderDetails).ThenInclude(i => i.OnlineClass);

            #region state
            switch (filter.FilterOrderType)
            {
                case FilterOrderType.All:
                    query = query.Include(i => i.OrderDetails).ThenInclude(i => i.OnlineClass)
                .Include(i => i.OrderDetails).ThenInclude(i => i.OnlineClass);
                    break;
                case FilterOrderType.Course:
                    query = query.Where(i => !i.IsOnlineClass)
                        .Include(i => i.OrderDetails).ThenInclude(i => i.OnlineClass);
                    break;
                case FilterOrderType.OnlineClass:
                    query = query.Where(i => i.IsOnlineClass)
                        .Include(i => i.OrderDetails).ThenInclude(i => i.OnlineClass);
                    break;
            }
            switch (filter.FilterOrderState)
            {
                case FilterOrderState.All:
                    break;
                case FilterOrderState.Finaly:
                    query = query.Where(i => i.IsFinaly);
                    break;
                case FilterOrderState.NotFinaly:
                    query = query.Where(i => i.IsFinaly);
                    break;
            }
            switch (filter.FilterAsnwerOrder)
            {
                case FilterAsnwerOrder.CreateDate_DES:
                    query = query.OrderByDescending(i => i.CreateDate);
                    break;
                case FilterAsnwerOrder.CreateDate_ASC:
                    query = query.OrderBy(i => i.CreateDate);
                    break;
            }
            #endregion
            #region filter
            if (filter.OrderId != null && filter.OrderId != 0)
                query = query.Where(i => i.OrderId == filter.OrderId);
            if (filter.UserId != null && filter.UserId != 0)
                query = query.Where(i => i.UserId == filter.UserId);
            if (!string.IsNullOrEmpty(filter.Search))
                query = query.Where(i => EF.Functions.Like(i.User.UserName, $"%{filter.Search}%") ||
                EF.Functions.Like(i.User.Email, $"%{filter.Search}%") ||
                EF.Functions.Like(i.User.Mobile, $"%{filter.Search}%"));
            #endregion
            filter.OrderSum = query.Sum(i => i.OrderSum);
            filter.DiscountPrice = query.Sum(i => i.DiscountPrice);
            #region paging
            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.HowManyShowPageAfterAndBefore);
            var allEntities = await query.Paging(pager).ToListAsync();
            #endregion
            return filter.SetPaging(pager).SetOrders(allEntities);
        }

        public async Task<Order> GetOrderById(int orderId)
        {
            return await _context.Orders
                .AsQueryable()
                .FirstOrDefaultAsync(i => i.OrderId == orderId);
        }
        public async Task<Order> GetOrderCourseNotFinalyByUserId(int userId)
        {
            return await _context.Orders
                .AsQueryable()
                .FirstOrDefaultAsync(i => i.UserId == userId && !i.IsFinaly && !i.IsOnlineClass);
        }
        public async Task<Order> GetOrderOnlineClassNotFinalyByUserId(int userId)
        {
            return await _context.Orders
                .AsQueryable()
                .FirstOrDefaultAsync(i => i.UserId == userId && !i.IsFinaly && i.IsOnlineClass);
        }

        public async Task<Order> GetOrderForUserPanel(int userId, int orderId)
        {
            return await _context.Orders.Include(c => c.User).Include(c => c.OrderDetails).ThenInclude(c => c.Course).ThenInclude(c => c.User)
                .FirstOrDefaultAsync(o => o.UserId == userId && o.OrderId == orderId && !o.IsOnlineClass);
        }

        public async Task<Order> GetOrderOnlineClassForUserPanel(int userId, int orderId)
        {
            return await _context.Orders.Include(c => c.User).Include(c => c.OrderDetails).ThenInclude(c => c.OnlineClass).ThenInclude(c => c.User)
                .FirstOrDefaultAsync(o => o.UserId == userId && o.OrderId == orderId && o.IsOnlineClass);
        }

        public async Task<List<Order>> GetUserOrders(int userId)
        {
            return await _context.Orders.Include(i => i.OrderDetails).ThenInclude(i => i.Course)
             .Where(i => i.UserId == userId && !i.IsOnlineClass).ToListAsync();
        }

        public async Task<List<Order>> GetUserOrdersOnlineClass(int userId)
        {
            return await _context.Orders.Include(i => i.OrderDetails).ThenInclude(i => i.OnlineClass).ThenInclude(i => i.OnlineClassOfDays)
              .Where(i => i.UserId == userId && i.IsOnlineClass).ToListAsync();
        }
        public async Task<Order> GetOrderByIdAndUserId(int userId, int orderId)
        {
            return await _context.Orders.AsQueryable()
                .Include(o => o.OrderDetails).ThenInclude(od => od.Course)
                .Include(o => o.OrderDetails).ThenInclude(od => od.OnlineClass)
                .FirstOrDefaultAsync(o => o.OrderId == orderId && o.UserId == userId);
        }
        public async Task<bool> IsMasterInCourse(int userId, int courseId)
        {
            return await _context.Courses
                .AnyAsync(c => c.TeacherId == userId && c.CourseId == courseId);
        }

        public async Task<bool> IsMasterInOnlineClass(int userId, int onlineClassId)
        {
            return await _context.OnlineClasses
                .AnyAsync(c => c.TeacherId == userId && c.OnlineClassId == onlineClassId);
        }



        public void RemoveOrderDetail(OrderDetail orderDetail)
        {
            _context.OrderDetails.Remove(orderDetail);
        }
        public void RemoveOrder(Order order)
        {
            _context.Orders.Remove(order);
        }

        public async Task<Order> ShowOrderDetails(int orderId)
        {
            return await _context.Orders.AsQueryable()
                .Include(u => u.OrderDetails).ThenInclude(u => u.Course)
                .Include(i => i.User)
                .FirstOrDefaultAsync(i => i.OrderId == orderId);
        }
        public async Task<bool> IsExistOrderInOrderDetail(int orderId)
        {
            return await _context.Orders.AsQueryable().AnyAsync(i => i.OrderId == orderId);
        }

        public void UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
        }
        public async Task<OrderDetail> GetOrderDetailByOrderIdAndCourseId(int orderId, int courseId)
        {
            return await _context.OrderDetails
                    .FirstOrDefaultAsync(d => d.OrderId == orderId && d.CourseId == courseId);
        }
        public async Task<OrderDetail> GetOrderDetailById(int orderDetailId)
        {
            return await _context.OrderDetails.AsQueryable()
                .FirstOrDefaultAsync(i => i.DetailId == orderDetailId);
        }

        public async Task<OrderDetail> GetOrderDetailByIdAndOrderId(int orderDetailId, int orderId)
        {
            return await _context.OrderDetails.AsQueryable()
                 .FirstOrDefaultAsync(i => i.DetailId == orderDetailId && i.OrderId == orderId);
        }

        public async Task<int> GetSumOrderDetailInOrderByOrderId(int orderId)
        {
            return await _context.OrderDetails.AsQueryable()
                .Where(d => d.OrderId == orderId).SumAsync(d => d.Price * d.Count);
        }

        public void UpdateOrderDetail(OrderDetail orderDetail)
        {
            _context.OrderDetails.Update(orderDetail);
        }
      
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }


    }
}
