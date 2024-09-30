using Learning.Application.Interfaces;
using Learning.Application.Security;
using Learning.Domain.Models.Course;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Learning.Mvc.Pages.Admin.Comment
{
    [PermissionChecker("Courses")]
    public class ShowCommentModel : PageModel
    {
        private readonly ICourseService _courseService;

        public ShowCommentModel(ICourseService courseService)
        {
            _courseService = courseService;
        }
        public CourseComment CourseComment { get; set; }
        public async Task OnGet(int id)
        {
            await _courseService.IsAdminReadComment(id);
            CourseComment = await _courseService.ShowCommentById(id);
        }
    }
}
