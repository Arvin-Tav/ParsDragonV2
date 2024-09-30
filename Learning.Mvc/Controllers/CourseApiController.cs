using Learning.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Learning.Mvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseApiController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseApiController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [Produces("application/json")]
        [HttpGet("search")]
        public async Task<IActionResult> Search()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var courseTitle =await _courseService.GetAllCourseTitle(term);
                return Ok(courseTitle);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
