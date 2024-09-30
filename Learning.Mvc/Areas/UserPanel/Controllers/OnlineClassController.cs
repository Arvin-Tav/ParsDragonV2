using Learning.Application.Interfaces;
using Learning.Domain.DTO.Order;
using Learning.Domain.Models.OnlineClass;
using Learning.Mvc.Controllers;
using Learning.Mvc.PresentationExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharpCompress.Archives;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Learning.Mvc.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class OnlineClassController : SiteBaseController
    {
        private readonly IOrderService _orderService;
        private readonly IOnlineClassService _onlineClassService;
        public OnlineClassController(IOrderService orderService, IOnlineClassService onlineClassService)
        {
            _onlineClassService = onlineClassService;
            _orderService = orderService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _orderService.GetUserOrdersOnlineClass(User.GetUserId()));
        }
        public async Task<IActionResult> ShowOnlineClass(int id, bool finaly = false, string type = "")
        {
            var order =await _orderService.GetOrderOnlineClassForUserPanel(User.GetUserId(), id);
            if (order == null || order.IsFinaly || !order.IsOnlineClass)
            {
                return Redirect("/UserPanel/OnlineClass/Index");
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
                    return Redirect("/UserPanel/OnlineClass/Index/?finaly=true");
            }
            return BadRequest();

        }
        public async Task<IActionResult> OnlineClassInfo(int id)
        {
            if (await _onlineClassService.IsUserInOnlineClass(User.GetUserId(), id))
            {
                return View(await _onlineClassService.GetInfoOnlineClassForUser(id));
            }
            return Redirect("/UserPanel/OnlineClass/Index");
        }
        public async Task<IActionResult> SessionsList(int id, int episodeId = 0)
        {

            if (await _onlineClassService.IsUserInOnlineClass(User.GetUserId(), id))
            {
               var onlineClass = await _onlineClassService.ShowOnlineClassEpisode(id,episodeId);
                if (episodeId != 0)
                {
                    ViewBag.filePath = onlineClass.FilePath;
                }
                return View(onlineClass.Episodes);
            }
            return Redirect("/UserPanel/OnlineClass/Index");
        }
        public async Task<IActionResult> RemoveOnlineClassFromOrder(int orderId, int detailId)
        {
            await _orderService.RemoveOrderDetail(orderId, detailId);
            return Redirect("/UserPanel/OnlineClass/Index");
        }
    }
}
