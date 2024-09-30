using Learning.Application.Interfaces;
using Learning.Application.Security;
using Learning.Domain.DTO.Course;
using Learning.Domain.DTO.CourseEpisode;
using Learning.Mvc.Controllers;
using Learning.Mvc.PresentationExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learning.Mvc.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    [PermissionChecker("Master")]
    public class MasterController : SiteBaseController
    {
        #region Ctor

        private ICourseService _courseService;
        private IUserService _userService;
        private IEpisodeService _episodeService;

        public MasterController(ICourseService courseService, IUserService userService, IEpisodeService episodeService)
        {
            _courseService = courseService;
            _userService = userService;
            _episodeService = episodeService;
        }

        #endregion

        [HttpGet("master-courses")]
        public async Task<IActionResult> MasterCoursesList()
        {
            return View(await _courseService.GetAllMasterCourses(User.GetUserId()));
        }

        [HttpGet("course-episodes/{courseId}")]
        public async Task<IActionResult> EpisodesList(int courseId)
        {
            var episodes = await _episodeService.GetCourseEpidoeForMaster(courseId, User.GetUserId());

            ViewBag.CourseId = courseId;

            return View(episodes);
        }

        [HttpGet("add-episode/{courseId}")]
        public async Task<IActionResult> AddEpisode(int courseId)
        {
            if (!await _courseService.IsMasterInCourse(User.GetUserId(), courseId)) return NotFound();

            var result = new CreateCourseEpisodeDTO
            {
                CourseId = courseId,
                IsFree = true
            };

            return View(result);
        }

        [HttpPost("add-episode/{courseId}")]
        public async Task<IActionResult> AddEpisode(CreateCourseEpisodeDTO episode)
        {
            if (ModelState.IsValid)
            {
                var result = await _episodeService.CreateEpisodeWithMaster(episode, User.GetUserId());
                switch (result)
                {
                    case Domain.DTO.Course.CourseEpisodeResut.NotFound:
                        TempData[ErrorMessage] = "موردی یافت نشد";
                        break;
                    case Domain.DTO.Course.CourseEpisodeResut.Success:
                        TempData[SuccessMessage] = "عملیات با موفقیت انجام شد";
                        return RedirectToAction("EpisodesList", "Master", new { courseId = episode.CourseId });
                }
            }
            return View(episode);
        }

        public async Task<IActionResult> DropzoneTarget(List<IFormFile> files, int courseId)
        {
            if (files != null && files.Any())
            {
                foreach (var file in files)
                {
                    var result = await _episodeService.UploadFileEpisode(file, file.FileName, courseId);
                    switch (result)
                    {
                        case UploadFileEpisodeResut.NotFile:
                            break;
                        case UploadFileEpisodeResut.ExistFile:
                            return new JsonResult(new { status = "ErrorFileName" });
                        case UploadFileEpisodeResut.ErrorFile:
                            break;
                        case UploadFileEpisodeResut.Success:
                            return new JsonResult(new { data = file.FileName, status = "Success" });
                    }
                }
            }
            return new JsonResult(new { status = "Error" });
        }
    }
}
