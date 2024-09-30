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
    public class OnlineClassPrerequisiteModel : AdminBaseModel
    {
        private readonly IOnlineClassService _onlineClassService;
        public OnlineClassPrerequisiteModel(IOnlineClassService onlineClassService)
        {
            _onlineClassService = onlineClassService;
        }
        [BindProperty]
        public List<OnlineClassPrerequisite> OnlineClassPrerequisites { get; set; }
        public async Task OnGet(int id)
        {
            ViewData["id"] = id;
            OnlineClassPrerequisites = await _onlineClassService.GetAllPrerequisitesByOnlineId(id);
        }
        public async Task<IActionResult> OnPost(int id, List<string> names)
        {
            var result = await _onlineClassService.AddOnlineClassPrerequisitesByList(id, names);
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
