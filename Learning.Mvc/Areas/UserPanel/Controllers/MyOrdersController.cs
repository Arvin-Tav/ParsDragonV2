using Learning.Application.Interfaces;
using Learning.Domain.DTO.Discount;
using Learning.Domain.DTO.Order;
using Learning.Mvc.Controllers;
using Learning.Mvc.PresentationExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace Learning.Mvc.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class MyOrdersController : SiteBaseController
    {
        private readonly IOrderService _orderService;
        private readonly IDiscountService _discountService;
        public MyOrdersController(IOrderService orderService, IDiscountService discountService)
        {
            _orderService = orderService;
            _discountService = discountService;
        }
        public async Task<IActionResult> Index(bool finaly = false)
        {
            ViewBag.finaly = finaly;
            return View(await _orderService.GetUserOrders(User.GetUserId()));
        }
        public async Task<IActionResult> ShowOrder(int id, bool finaly = false, string type = "")
        {
            var order =await _orderService.GetOrderCourseForUserPanel(User.GetUserId(), id);
            if (order == null || order.IsFinaly || order.IsOnlineClass)
            {
                return Redirect("/UserPanel/MyOrders/Index");
            }
            ViewBag.typeDiscount = type;
            ViewBag.finaly = finaly;
            return View(order);
        }
        public async Task<IActionResult> FinalyOrder(int id)
        {
            IPAddress ipAddres = Request.HttpContext.Connection.RemoteIpAddress;
            string ip = ipAddres.ToString();
            var result = await _orderService.FinalyOrder(User.GetUserId(), id, ip);
            switch (result)
            {
                case FinalyOrderResult.Error:
                    break;
                case FinalyOrderResult.NotFound:
                    break;
                case FinalyOrderResult.Success:
                    return Redirect("/UserPanel/MyOrders/Index/?finaly=true");
            }
            return BadRequest();
        }

        public async Task<IActionResult> UseDiscount(int orderId, string code)
        {
            DiscountUseType type =await _discountService.UseDiscount(orderId, code);
            return Redirect("/UserPanel/MyOrders/ShowOrder/" + orderId + "?type=" + type.ToString());
        }
        public async Task<IActionResult> RemoveCourseFromOrder(int orderId, int detailId)
        {
           await _orderService.RemoveOrderDetail(orderId, detailId);
            return Redirect("/UserPanel/MyOrders/ShowOrder/" + orderId);
        }
    }
}
