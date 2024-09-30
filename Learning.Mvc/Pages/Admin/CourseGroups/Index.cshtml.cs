using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Learning.Application.Interfaces;
using Learning.Application.Security;
using Learning.Domain.DTO.Course;
using Learning.Domain.Models.Course;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Learning.Mvc.Pages.Admin.CourseGroups
{
    [PermissionChecker("Courses")]
    public class IndexModel : PageModel
    {
        private ICourseService _courseService;

        public IndexModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public List<CourseGroup> CourseGroups { get; set; }
        public async Task OnGet()
        {
            CourseGroups = await _courseService.GetAllGroup();
        }

        public async Task<IActionResult> OnGetDeleteCourseGroups(int code)
        {
            try
            {
                var result = await _courseService.DeleteCourseGroup(code);
                switch (result)
                {
                    case DeleteCourseGroupResult.NotFound:
                        break;
                    case DeleteCourseGroupResult.SetCourse:
                        break;
                    case DeleteCourseGroupResult.Success:
                        return Content("true");
                }
                return Content("false");
            }
            catch
            {
                return Content("false");
            }

        }
    }
}