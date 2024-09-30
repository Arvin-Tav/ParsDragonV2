using Learning.Application.Interfaces;
using Learning.Domain.DTO.Order;
using Learning.Domain.Interfaces;
using Learning.Domain.Models.Course;
using Learning.Domain.Models.OnlineClass;
using Learning.Domain.Models.Order;
using Learning.Domain.Models.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Application.Services
{
    public class OrderService : IOrderService
    {
        #region constructor
        private readonly IOrderRepository _orderRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IOnlineClassRepository _onlineClassRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly IWalletRepository _walletRepository;
        public OrderService(IOrderRepository orderRepository, ICourseRepository courseRepository, IOnlineClassRepository onlineClassRepository, IDiscountRepository discountRepository, IWalletRepository walletRepository)
        {
            _orderRepository = orderRepository;
            _courseRepository = courseRepository;
            _onlineClassRepository = onlineClassRepository;
            _discountRepository = discountRepository;
            _walletRepository = walletRepository;
        }

        #endregion
        #region order
        public async Task<int> AddOrderCourse(int userId, int courseId)
        {
            Order order = await _orderRepository.GetOrderCourseNotFinalyByUserId(userId);
            Course course = await _courseRepository.GetCourseById(courseId);
            if (order == null)
            {
                order = new Order()
                {
                    IsFinaly = false,
                    UserId = userId,
                    CreateDate = DateTime.Now,
                    OrderSum = course.CoursePrice,
                    OrderDetails = new List<OrderDetail>()
                    {
                        new OrderDetail()
                        {
                            CourseId=courseId,
                            Count=1,
                            Price=course.CoursePrice,
                        }
                    }
                };
                await AddOrder(order);
            }
            else
            {
                OrderDetail detail = await _orderRepository
                    .GetOrderDetailByOrderIdAndCourseId(order.OrderId, courseId);
                if (detail != null)
                {
                    //Baraye foroshgah bayad faal bashad
                    //detail.Count += 1;
                    //_context.OrderDetails.Update(detail);
                }
                else
                {
                    detail = new OrderDetail()
                    {
                        OrderId = order.OrderId,
                        Count = 1,
                        CourseId = courseId,
                        Price = course.CoursePrice,
                    };
                    await AddOrderDetail(detail);
                }
            }
            await UpdatePriceOrder(order.OrderId);
            return order.OrderId;
        }
        public async Task AddOrderDetail(OrderDetail orderDetail)
        {
            await _orderRepository.AddOrderDetail(orderDetail);
            await _orderRepository.Save();
        }
        public async Task AddOrder(Order order)
        {
            await _orderRepository.AddOrder(order);
            await _orderRepository.Save();
        }
        public async Task<int> AddOrderOnlineClass(int userId, int onlineClassId)
        {
            Order order = await _orderRepository.GetOrderOnlineClassNotFinalyByUserId(userId);
            OnlineClass online = await _onlineClassRepository.GetOnlineClassById(onlineClassId);
            if (order == null)
            {
                order = new Order()
                {
                    IsFinaly = false,
                    UserId = userId,
                    CreateDate = DateTime.Now,
                    OrderSum = online.Price,
                    IsOnlineClass = true,
                    OrderDetails = new List<OrderDetail>()
                    {
                        new OrderDetail()
                        {
                            OnlineClassId=onlineClassId,
                            Count=1,
                            Price=online.Price,
                        }
                    }
                };
                await AddOrder(order);

            }
            return order.OrderId;
        }
        public async Task<FilterOrderDTO> FilterOrder(FilterOrderDTO filter)
        {
            return await _orderRepository.FilterOrder(filter);
        }

        public async Task<Order> GetOrderById(int orderId)
        {
            return await _orderRepository.GetOrderById(orderId);
        }


        public Task<Order> ShowOrderDetails(int orderId)
        {
            return _orderRepository.ShowOrderDetails(orderId);
        }

        public async Task UpdateOrder(Order order)
        {
            _orderRepository.UpdateOrder(order);
            await _orderRepository.Save();
        }

        public async Task UpdatePriceOrder(int orderId)
        {
            var order = await GetOrderById(orderId);
            order.OrderSum = await _orderRepository.GetSumOrderDetailInOrderByOrderId(orderId);
            await UpdateOrder(order);
        }






        #endregion

        #region userPanel

        public Task<Order> GetOrderCourseForUserPanel(int userId, int orderId)
        {
            return _orderRepository.GetOrderForUserPanel(userId, orderId);
        }

        public async Task<Order> GetOrderOnlineClassForUserPanel(int userId, int orderId)
        {
            return await _orderRepository.GetOrderOnlineClassForUserPanel(userId, orderId);
        }
        public async Task<Order> GetOrderByIdAndUserId(int userId, int orderId)
        {
            return await _orderRepository.GetOrderByIdAndUserId(userId, orderId);
        }

        public async Task RemoveOrderDetail(int orderId, int detailId)
        {
            var detail = await _orderRepository.GetOrderDetailByIdAndOrderId(orderId, detailId);
            if (detail != null)
            {
                _orderRepository.RemoveOrderDetail(detail);
                await _orderRepository.Save();

                var order = await GetOrderById(orderId);
                if (await _orderRepository.IsExistOrderInOrderDetail(orderId))
                {
                    int sumOrderDetails = await _orderRepository.GetSumOrderDetailInOrderByOrderId(orderId);
                    order.DiscountPercent = 0;
                    order.DiscountPercent = 0;
                    order.OrderSum = sumOrderDetails;
                    await UpdateOrder(order);

                }
                else
                {
                    var userDiscount = await _discountRepository.UserDiscountCodeByOrderId(orderId);
                    if (userDiscount != null)
                    {
                        _discountRepository.RemoveUserDiscount(userDiscount);
                        await _orderRepository.Save();

                    }
                    _orderRepository.RemoveOrder(order);
                }
                await _orderRepository.Save();
            }
        }

        public async Task<FinalyOrderResult> FinalyOrder(int userId, int orderId, string ip)
        {
            var order = await GetOrderByIdAndUserId(userId, orderId);

            if (order == null || order.IsFinaly)
            {
                return FinalyOrderResult.NotFound;
            }

            if (await _walletRepository.BalanceUserWallet(userId) >= order.OrderSum)
            {
                order.IsFinaly = true;
                order.Ip = ip;
                await _walletRepository.AddWallet(new Wallet()
                {
                    Amount = order.OrderSum,
                    IsPay = true,
                    Description = order.IsOnlineClass ? " ثبت نام در کلاس "
                     + order.OrderDetails.FirstOrDefault().OnlineClass.Title : "فاکتور شما #" + order.OrderId,
                    UserId = userId,
                    TypeId = 2,
                    IsOnlineClass = order.IsOnlineClass ? true : false,
                    Ip = ip
                });
                await UpdateOrder(order);
                if (order.IsOnlineClass)
                {
                    foreach (OrderDetail detail in order.OrderDetails)
                    {
                        await _onlineClassRepository.AddUserOnlineClass(new UserOnlineClass()
                        {
                            OnlineClassId = (int)detail.OnlineClassId,
                            UserId = userId
                        });
                    }

                }
                else
                {
                    foreach (OrderDetail detail in order.OrderDetails)
                    {
                        await _courseRepository.AddUserCourse(new UserCourse()
                        {
                            CourseId = (int)detail.CourseId,
                            UserId = userId
                        });
                    }
                }

                await _orderRepository.Save();
                return FinalyOrderResult.Success;
            }

            return FinalyOrderResult.Error;
        }

        public async Task<List<Order>> GetUserOrders(int userId)
        {
            return await _orderRepository.GetUserOrders(userId);
        }

        public async Task<List<Order>> GetUserOrdersOnlineClass(int userId)
        {
            return await _orderRepository.GetUserOrdersOnlineClass(userId);
        }

        #endregion




    }
}
