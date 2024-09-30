using Learning.Application.Interfaces;
using Learning.Application.Security;
using Learning.Domain.DTO.Course;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learning.Mvc.Pages.Admin.Courses
{
    [PermissionChecker("Courses")]
    public class EditCourseModel : AdminBaseModel
    {
        private readonly ICourseService _courseService;

        public EditCourseModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [BindProperty]
        public EditCourseDTO EditCourseDTO { get; set; }
        public async Task OnGet(int id)
        {
            EditCourseDTO = await _courseService.GetCourseForEdit(id);

            var groups = await _courseService.GetGroupForManageCourse();
            ViewData["Groups"] = new SelectList(groups, "Value", "Text", EditCourseDTO.GroupId);

            List<SelectListItem> subgroups = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "انتخاب کنید",Value = ""}
            };
            subgroups.AddRange(await _courseService.GetSubGroupForManageCourse(EditCourseDTO.GroupId));
            string selectedSubGroup = null;
            if (EditCourseDTO.SubGroup != null)
            {
                selectedSubGroup = EditCourseDTO.SubGroup.ToString();
            }
            ViewData["SubGroups"] = new SelectList(subgroups, "Value", "Text", selectedSubGroup);

            var teachers = await _courseService.GetTeachers();
            ViewData["Teachers"] = new SelectList(teachers, "Value", "Text", EditCourseDTO.TeacherId);

            var levels = await _courseService.GetLevels();
            ViewData["Levels"] = new SelectList(levels, "Value", "Text", EditCourseDTO.LevelId);

            var statues = await _courseService.GetStatues();
            ViewData["Statues"] = new SelectList(statues, "Value", "Text", EditCourseDTO.StatusId);

        }

        public async Task<IActionResult> OnPost(IFormFile imgCourseUp, IFormFile demoUp)
        {
            if (ModelState.IsValid)
            {
                var result = await _courseService.EditCourse(EditCourseDTO, imgCourseUp, demoUp);
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
            return Page();
        }
    }
}