using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Learning.Application.Security;
using Learning.Application.Interfaces;
using Learning.Domain.Models.Course;
using Learning.Domain.DTO.Course;

namespace Learning.Mvc.Pages.Admin.CourseGroups
{
    [PermissionChecker("Courses")]
    public class EditGroupModel : AdminBaseModel
    {
        private ICourseService _courseService;

        public EditGroupModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [BindProperty]
        public EditCourseGroupDTO CourseGroups { get; set; }

        public async void OnGet(int id)
        {
            CourseGroups = await _courseService.GetGroupForEdit(id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var result = await _courseService.UpdateGroup(CourseGroups);
                switch (result)
                {
                    case CourseGroupResult.NotFound:
                        TempData[ErrorMessage] = "موردی یافت نشد  ";
                        break;
                    case CourseGroupResult.ErrorUrlName:
                        ModelState.AddModelError("CourseGroups.UrlName", "نام url تکراریست");
                        TempData[ErrorMessage] = "نام url تکراریست";
                        break;
                    case CourseGroupResult.Success:
                        TempData[SuccessMessage] = "عملبات با موفقیت انجام شد";
                        return RedirectToPage("Index");
                }
            }
            return Page();
        }
    }
}