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
    public class CreateGroupModel : AdminBaseModel
    {
        private ICourseService _courseService;

        public CreateGroupModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [BindProperty]
        public CreateCourseGroupDTO CourseGroups { get; set; }

        public async Task OnGet(int? id)
        {
            CourseGroups = new CreateCourseGroupDTO()
            {
                ParentId = id
            };
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var result = await _courseService.CreateGroup(CourseGroups);
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