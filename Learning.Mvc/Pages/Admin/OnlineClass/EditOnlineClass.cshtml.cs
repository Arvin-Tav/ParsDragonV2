using Learning.Application.Interfaces;
using Learning.Application.Security;
using Learning.Domain.DTO.OnlineClass;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learning.Mvc.Pages.Admin.OnlineClass
{
    [PermissionChecker("AccessToOnlineClass")]
    public class EditOnlineClassModel : AdminBaseModel
    {
        private readonly IOnlineClassService _onlineClassService;
        private readonly ICourseService _courseService;

        public EditOnlineClassModel(IOnlineClassService onlineClassService, ICourseService courseService)
        {
            _courseService = courseService;
            _onlineClassService = onlineClassService;
        }
        [BindProperty]
        public EditOnlineClassDTO EditOnlineClassDTO { get; set; }
        public async Task OnGet(int id)
        {
            EditOnlineClassDTO = await _onlineClassService.GetOnlineClassByIdForEdit(id);

            var groups = await _courseService.GetGroupForManageCourse();
            ViewData["Groups"] = new SelectList(groups, "Value", "Text", EditOnlineClassDTO.GroupId);

            List<SelectListItem> subgroups = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "انتخاب کنید",Value = ""}
            };
            subgroups.AddRange(await _courseService.GetSubGroupForManageCourse(EditOnlineClassDTO.GroupId));
            string selectedSubGroup = null;
            if (EditOnlineClassDTO.SubGroup != null)
            {
                selectedSubGroup = EditOnlineClassDTO.SubGroup.ToString();
            }
            ViewData["SubGroups"] = new SelectList(subgroups, "Value", "Text", selectedSubGroup);

            var teachers = await _courseService.GetTeachers();
            ViewData["Teachers"] = new SelectList(teachers, "Value", "Text", EditOnlineClassDTO.TeacherId);

        }

        public async Task<IActionResult> OnPost(IFormFile imageName)
        {

            if (ModelState.IsValid)
            {
                var result = await _onlineClassService.UpdateByOnlineClass(EditOnlineClassDTO, imageName);
                switch (result)
                {
                    case OnlineClassResult.Error:
                        TempData[ErrorMessage] = "خطا در ارتباط";
                        break;
                    case OnlineClassResult.NotImage:
                        TempData[WarningMessage] = "عکس را آپلود کنید";
                        break;
                    case OnlineClassResult.NotDate:
                        ModelState.AddModelError("CreateOnlineClassViewModel.StartClass", "تاریخ را درست انتخاب کنید");
                        TempData[WarningMessage] = "تاریخ را درست انتخاب کنید";
                        break;
                    case OnlineClassResult.ErrorImage:
                        TempData[WarningMessage] = "پسوند عکس مشکل دارد لطفا عکس انتخاب کنید";
                        break;
                    case OnlineClassResult.Success:
                        TempData[SuccessMessage] = "عملبات با موفقیت انجام شد";
                        return RedirectToPage("Index");
                }
            }
            var groups = await _courseService.GetGroupForManageCourse();
            ViewData["Groups"] = new SelectList(groups, "Value", "Text", EditOnlineClassDTO.GroupId);

            List<SelectListItem> subgroups = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "انتخاب کنید",Value = ""}
            };
            subgroups.AddRange(await _courseService.GetSubGroupForManageCourse(EditOnlineClassDTO.GroupId));
            string selectedSubGroup = null;
            if (EditOnlineClassDTO.SubGroup != null)
            {
                selectedSubGroup = EditOnlineClassDTO.SubGroup.ToString();
            }
            ViewData["SubGroups"] = new SelectList(subgroups, "Value", "Text", selectedSubGroup);

            var teachers = await _courseService.GetTeachers();
            ViewData["Teachers"] = new SelectList(teachers, "Value", "Text", EditOnlineClassDTO.TeacherId);
            return Page();

        }
    }
}
