using Learning.Application.Interfaces;
using Learning.Application.Security;
using Learning.Domain.DTO.Course;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Learning.Mvc.Pages.Admin.Courses
{
    [PermissionChecker("Courses")]
    public class IndexModel : PageModel
    {
        private readonly IEpisodeService _episodeService;
        private ICourseService _courseService;

        public IndexModel(ICourseService courseService, IEpisodeService episodeService)
        {
            _episodeService = episodeService;
            _courseService = courseService;
        }

        public FilterCourseDTO FilterCourseDTO { get; set; }

        public async Task OnGet(FilterCourseDTO filter)
        {
            FilterCourseDTO =await _courseService.FilterCourse(filter);
        }

        public async Task<IActionResult> OnGetIsSuggested(int id)
        {
           await  _courseService.IsSuggestedCourse(id);
            return RedirectToPage("Index");
        }
        public async Task<IActionResult> OnGetIsShow(int id)
        {
          await  _courseService.IsShowCourse(id);
            return RedirectToPage("Index");
        }
        public async Task<IActionResult> OnGetIsShowEpisode(int courseId, int eposideId)
        {
            await _episodeService.IsShowCourseEpisode(eposideId);
            return Redirect("/Admin/Courses/IndexEpisode/" + courseId);
        }
        public async Task<IActionResult> OnGetIsFreeEpisode(int courseId, int eposideId)
        {
            await _episodeService.IsFreeCourseEpisode(eposideId);
            return Redirect("/Admin/Courses/IndexEpisode/" + courseId);
        }
        public async Task<IActionResult> OnGetDeleteCourse(int code)
        {
            var result = await _courseService.DeleteCourse(code);
            switch (result)
            {
                case DeleteResult.NotFound:
                    break;
                case DeleteResult.Success:
                    return Content("true");
            }
            return Content("false");

        }
        public async Task<IActionResult> OnGetDeleteEpisodeCourse(int code)
        {
            var result = await _episodeService.DeleteCourseEpisode(code);
            switch (result)
            {
                case DeleteResult.NotFound:
                    break;
                case DeleteResult.Success:
                    return Content("true");
            }
            return Content("false");

        }
    }
}