using Learning.Application.Interfaces;
using Learning.Application.Security;
using Learning.Domain.DTO.Course;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace Learning.Mvc.Pages.Admin.Courses
{
    [PermissionChecker("Courses")]
    public class CreateCourseModel : AdminBaseModel
    {
        private readonly ICourseService _courseService;

        public CreateCourseModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [BindProperty]
        public CreateCourseDTO CreateCourseDTO { get; set; }

        public async Task OnGet()
        {
            var groups = await _courseService.GetGroupForManageCourse();
            ViewData["Groups"] = new SelectList(groups, "Value", "Text");

            var subGrous = await _courseService.GetSubGroupForManageCourse(int.Parse(groups.First().Value));
            ViewData["SubGroups"] = new SelectList(subGrous, "Value", "Text");

            var teachers = await _courseService.GetTeachers();
            ViewData["Teachers"] = new SelectList(teachers, "Value", "Text");

            var levels = await _courseService.GetLevels();
            ViewData["Levels"] = new SelectList(levels, "Value", "Text");

            var statues = await _courseService.GetStatues();
            ViewData["Statues"] = new SelectList(statues, "Value", "Text");
        }

        public async Task<IActionResult> OnPost(IFormFile imgCourseUp, IFormFile demoUp)
        {
            if (ModelState.IsValid)
            {
                var result = await _courseService.CreateCourse(CreateCourseDTO, imgCourseUp, demoUp);
                switch (result)
                {
                    case CourseResut.Error:
                        TempData[ErrorMessage] = "خطا در ارتباط";
                        break;
                    case CourseResut.NotImage:
                        TempData[ErrorMessage] = "عکس را آپلود کنید";
                        break;
                    case CourseResut.ErrorImage:
                        TempData[ErrorMessage] = "پسوند عکس مشکل دارد لطفا عکس انتخاب کنید";
                        break;
                    case CourseResut.ErrorDemo:
                        TempData[ErrorMessage] = "دمو مشکل دارد لطفا دقت کنید";
                        break;
                    case CourseResut.Success:
                        TempData[SuccessMessage] = "عملبات با موفقیت انجام شد";
                        return RedirectToPage("Index");
                }
            }
            var groups = await _courseService.GetGroupForManageCourse();
            ViewData["Groups"] = new SelectList(groups, "Value", "Text");

            var subGrous = await _courseService.GetSubGroupForManageCourse(int.Parse(groups.First().Value));
            ViewData["SubGroups"] = new SelectList(subGrous, "Value", "Text");

            var teachers = await _courseService.GetTeachers();
            ViewData["Teachers"] = new SelectList(teachers, "Value", "Text");

            var levels = await _courseService.GetLevels();
            ViewData["Levels"] = new SelectList(levels, "Value", "Text");

            var statues = await _courseService.GetStatues();
            ViewData["Statues"] = new SelectList(statues, "Value", "Text");


            return Page();

        }
    }
}