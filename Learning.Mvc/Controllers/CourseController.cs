using Ganss.XSS;
using Learning.Application.Convertors;
using Learning.Application.Interfaces;
using Learning.Domain.DTO.Course;
using Learning.Domain.Models.Course;
using Learning.Mvc.PresentationExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learning.Mvc.Controllers
{
    public class CourseController : SiteBaseController
    {
        private readonly ICourseService _courseService;
        private readonly IOrderService _orderService;
        private readonly IEpisodeService _episodeService;
        private readonly IUserService _userService;
        private readonly IPermissionService _permissionService;
        public CourseController(ICourseService courseService, IPermissionService permissionService, IOrderService orderSevice, IEpisodeService episodeService, IUserService userService)
        {
            _courseService = courseService;
            _orderService = orderSevice;
            _episodeService = episodeService;
            _userService = userService;
            _permissionService = permissionService;
        }
        [HttpGet("courses")]
        public async Task<IActionResult> Index(FilterCourseDTO filter)
        {
            try
            {
                filter.TakeEntity = 9;
                ViewBag.Groups = await _courseService.GetAllGroup();
                return await Task.FromResult(View(await _courseService.FilterCourse(filter)));

            }
            catch
            {
                return await Task.FromResult(Redirect("/Home/Error404"));
            }
        }

        [HttpGet("courses_{id}/{name}/{episode?}")]
        public async Task<IActionResult> ShowCourse(int id, string name = "", int episode = 0)
        {
            try
            {
                var course = await _courseService.GetCourseForShow(id);
                if (course == null) return NotFound();

                if (episode != 0 && User.Identity.IsAuthenticated)
                {
                    var courseEpisode = await _courseService.GetCourseEpisodeForShow(id, episode, User.GetUserId());
                    ViewBag.NotSuccesEpisodes = courseEpisode.NotSuccesEpisodes;
                    ViewBag.NotSucces = courseEpisode.NotSucces;
                    if (courseEpisode.NotSuccesEpisodes || courseEpisode.NotSucces) return View(course);
                    ViewBag.Episode = courseEpisode.Episode;
                    ViewBag.filePath = courseEpisode.FilePath;
                }
                return await Task.FromResult(View(course));
            }
            catch
            {
                return await Task.FromResult(Redirect("/Home/Error404"));
            }

        }

        public async Task<IActionResult> ShowComment(FilterCourseCommentDTO filter)
        {
            return PartialView(await _courseService.FilterCourseComment(filter));
        }
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateComment(CourseComment comment)
        {
            var sanitizer = new HtmlSanitizer();
            comment.Comment = sanitizer.Sanitize(comment.Comment);
            comment.UserId = User.GetUserId();
            await _courseService.AddComment(comment);
            return PartialView("ShowComment", await _courseService.FilterCourseComment(new FilterCourseCommentDTO() { CourseId = comment.CourseId }));
        }

        public async Task<IActionResult> CourseVote(int id)
        {
            if (!await _courseService.IsFree(id) && User.Identity.IsAuthenticated)
            {
                if (!await _courseService.IsUserInCourse(User.GetUserId(), id))
                {
                    ViewBag.NotAccess = true;
                }
            }
            ViewBag.CourseId = id;
            return PartialView(await _courseService.GetCourseVotes(id));
        }
        [Authorize]
        public async Task<IActionResult> AddVote(int id, bool vote)
        {
            int userId = User.GetUserId();
            await _courseService.AddVote(userId, id, vote);
            return PartialView("CourseVote",await _courseService.GetCourseVotes(id));
        }
        [Authorize]
        public async Task<IActionResult> BuyCourse(int id)
        {
            try
            {
                int orderId =await _orderService.AddOrderCourse(User.GetUserId(), id);
                return await Task.FromResult(Redirect("/UserPanel/MyOrders/ShowOrder/" + orderId));
            }
            catch
            {
                return await Task.FromResult(Redirect("/Home/Error404"));
            }

        }
        [Route("DownloadFile/{episodeId}")]
        [Authorize]
        public async Task<IActionResult> DownloadFile(int episodeId)
        {
            var episode =await _episodeService.GetEpisodeById(episodeId);
            int userId = User.GetUserId();
            var episodeFile = await _episodeService.GetDowmloadFile(episodeId, userId);
            if (episodeFile != null)
            {
                byte[] file = System.IO.File.ReadAllBytes(episodeFile);
                return File(file, "application/force-download", episode.EpisodeFileName);
            }
            return Redirect("/ShowCourse/" + episode.CourseId + "/" + episode.Course.CourseTitle.FixedNameCourse());
        }
    }
}
