using Learning.Application.Interfaces;
using Learning.Application.Security;
using Learning.Domain.DTO.OnlineClass;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace Learning.Mvc.Pages.Admin.OnlineClass
{
    [PermissionChecker("AccessToOnlineClass")]
    public class CreateOnlineClassModel : AdminBaseModel
    {
        private readonly IOnlineClassService _onlineClassService;
        private readonly ICourseService _courseService;

        public CreateOnlineClassModel(IOnlineClassService onlineClassService, ICourseService courseService)
        {
            _courseService = courseService;
            _onlineClassService = onlineClassService;
        }
        [BindProperty]
        public CreateOnlineClassDTO CreateOnlineClassDTO { get; set; }
        public async Task OnGet()
        {
            var groups = await _courseService.GetGroupForManageCourse();
            ViewData["Groups"] = new SelectList(groups, "Value", "Text");

            var subGrous = await _courseService.GetSubGroupForManageCourse(int.Parse(groups.First().Value));
            ViewData["SubGroups"] = new SelectList(subGrous, "Value", "Text");

            var teachers = await _courseService.GetTeachers();
            ViewData["Teachers"] = new SelectList(teachers, "Value", "Text");
        }
        public async Task<IActionResult> OnPost(IFormFile imageName)
        {

            if (ModelState.IsValid)
            {
                var result = await _onlineClassService.CreateOnlineClass(CreateOnlineClassDTO, imageName);
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
            ViewData["Groups"] = new SelectList(groups, "Value", "Text");

            var subGrous = await _courseService.GetSubGroupForManageCourse(int.Parse(groups.First().Value));
            ViewData["SubGroups"] = new SelectList(subGrous, "Value", "Text");

            var teachers = await _courseService.GetTeachers();
            ViewData["Teachers"] = new SelectList(teachers, "Value", "Text");


            return Page();


        }
    }
}
