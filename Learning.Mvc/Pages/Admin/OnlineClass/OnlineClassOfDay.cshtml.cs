using Learning.Application.Interfaces;
using Learning.Application.Security;
using Learning.Domain.DTO.OnlineClass;
using Learning.Domain.Models.OnlineClass;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learning.Mvc.Pages.Admin.OnlineClass
{
    [PermissionChecker("AccessToOnlineClass")]
    public class OnlineClassOfDayModel : AdminBaseModel
    {
        private readonly IOnlineClassService _onlineClassService;
        public OnlineClassOfDayModel(IOnlineClassService onlineClassService)
        {
            _onlineClassService = onlineClassService;
        }
        [BindProperty]
        public List<OnlineClassOfDay> OnlineClassOfDays { get; set; }
        public async Task OnGet(int id)
        {
            ViewData["id"] = id;
            OnlineClassOfDays =await _onlineClassService.GetAllDaysByOnlineId(id);
        }
        public async Task<IActionResult> OnPost(int id, List<string> day, List<string> startTime, List<string> endTime)
        {
            var result =await _onlineClassService.AddOnlineClassDaysByList(id, day, startTime, endTime);
            switch (result)
            {
                case OnlineClassInfoResult.NotFound:
                    TempData[ErrorMessage] = "موردی یافت نشد";
                    break;
                case OnlineClassInfoResult.Success:
                    TempData[SuccessMessage] = "عملبات با موفقیت انجام شد";
                    return RedirectToPage("Index");

            }
            return Page();
        }
    }
}
