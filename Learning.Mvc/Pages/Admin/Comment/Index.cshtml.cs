using Learning.Application.Interfaces;
using Learning.Application.Security;
using Learning.Domain.DTO.Course;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Learning.Mvc.Pages.Admin.Comment
{
    [PermissionChecker("Courses")]
    public class IndexModel : PageModel
    {
        private readonly ICourseService _courseService;

        public IndexModel(ICourseService courseService)
        {
            _courseService = courseService;
        }
        public FilterCourseCommentDTO FilterCourseCommentDTO { get; set; }
        public async Task OnGet(FilterCourseCommentDTO filter)
        {
            FilterCourseCommentDTO = await _courseService.FilterCourseComment(filter);
        }

        public async Task<IActionResult> OnGetDeleteComment(int code)
        {
            var result = await _courseService.DeleteComment(code);
            switch (result)
            {
                case DeleteCommentResult.NotFound:
                    break;
                case DeleteCommentResult.Success:
                    return Content("true");
            }
            return Content("false");
        }
    }
}
