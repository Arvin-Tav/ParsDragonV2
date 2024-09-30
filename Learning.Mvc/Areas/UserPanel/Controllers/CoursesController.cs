using Learning.Application.Interfaces;
using Learning.Mvc.Controllers;
using Learning.Mvc.PresentationExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Learning.Mvc.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class CoursesController : SiteBaseController
    {
        private readonly ICourseService _courseService;
        private readonly IUserService _userService;

        public CoursesController(ICourseService courseService, IUserService userService)
        {
            _courseService = courseService;
            _userService = userService;
        }
        [Route("/UserPanel/MyCourses")]
        public async Task<IActionResult> MyCourses()
        {
            return View(await _courseService.ShowCoursesUser(User.GetUserId()));
        }
    }
}
