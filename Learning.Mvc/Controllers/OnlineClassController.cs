using Learning.Application.Interfaces;
using Learning.Domain.DTO.OnlineClass;
using Learning.Mvc.PresentationExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Learning.Mvc.Controllers
{
    public class OnlineClassController : SiteBaseController
    {
        private readonly IOnlineClassService _onlineClassService;
        private readonly ICourseService _courseService;
        private readonly IOrderService _orderService;
        private readonly IPermissionService _permissionService;

        public OnlineClassController(IOnlineClassService onlineClassService, ICourseService courseService, IOrderService orderService, IPermissionService permissionService)
        {
            _onlineClassService = onlineClassService;
            _courseService = courseService;
            _orderService = orderService;
            _permissionService = permissionService;
        }
        [HttpGet("onlineClasses")]
        public async Task<IActionResult> Index(FilterOnlineClassDTO filter)
        {
            try
            {
                ViewBag.Groups =await _courseService.GetAllGroup();
                return await Task.FromResult(View(await _onlineClassService.FilterOnlineClass(filter)));
            }
            catch
            {
                return await Task.FromResult(Redirect("/Home/Error404"));
            }
        }

        [HttpGet("onlineClass_{id}/{name?}")]
        public async Task<IActionResult> ShowOnlineClass(int id, string name = "")
        {
            try
            {
                return await Task.FromResult(View(await _onlineClassService.GetOnlineClassById(id)));
            }
            catch
            {
                return await Task.FromResult(Redirect("/Home/Error404"));
            }

        }
        [Authorize]
        public async Task<IActionResult> BuyOnlineClass(int id)
        {
            try
            {
                int orderId =await _orderService.AddOrderOnlineClass(User.GetUserId(), id);
                return await Task.FromResult(Redirect("/UserPanel/OnlineClass/Index/" + orderId));
            }
            catch
            {
                return await Task.FromResult(Redirect("/Home/Error404"));
            }

        }

        //[Authorize]
        //public IActionResult DownloadOnlineClassFile(int episodeId)
        //{
        //    var episode = _onlineClassService.GetOnlineClassEpisodeById(episodeId);

        //    string filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/FilesLearning/OnlineClass/OnlineClassEpisode/" + episode.OnlineClassId,
        //        episode.EpisodeFileName);
        //    string fileName = episode.EpisodeFileName;
        //    if (System.IO.File.Exists(filepath))
        //    {
        //        if (User.Identity.IsAuthenticated)
        //        {


        //            bool isUserInCourse = _orderService.IsUserInOnlineClass(User.Identity.Name, episode.OnlineClassId);
        //            bool isMasterInCourse = _orderService.IsMasterInOnlineClass(User.Identity.Name, episode.OnlineClassId);
        //            bool isAdmin = _permissionService.CheckPermission("AccessToOnlineClass", User.Identity.Name);
        //            if (isMasterInCourse || isAdmin || isUserInCourse)
        //            {
        //                byte[] file = System.IO.File.ReadAllBytes(filepath);
        //                return File(file, "application/force-download", fileName);
        //            }
        //        }

        //        //if (_orderService.IsUserInCourse(User.Identity.Name, episode.CourseId))
        //        //{
        //        //    byte[] file = System.IO.File.ReadAllBytes(filepath);
        //        //    return File(file, "application/force-download", fileName);
        //        //}
        //    }

        //    return Redirect("/");
        //}
    }
}
