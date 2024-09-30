using Learning.Application.Interfaces;
using Learning.Application.Security;
using Learning.Domain.DTO.OnlineClass;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Learning.Mvc.Pages.Admin.OnlineClassEpisode
{
    [PermissionChecker("AccessToOnlineClass")]
    public class CreateModel : AdminBaseModel
    {
        private readonly IOnlineClassService _onlineClassService;
        public CreateModel(IOnlineClassService onlineClassService)
        {
            _onlineClassService = onlineClassService;
        }
        [BindProperty]
        public CreateOnlineClassEpisodeDTO CreateOnlineClassEpisodeDTO { get; set; }
        public async Task OnGet(int id)
        {
            CreateOnlineClassEpisodeDTO = new CreateOnlineClassEpisodeDTO();
            CreateOnlineClassEpisodeDTO.OnlineClassId = id;
        }


        public async Task<IActionResult> OnPost(IFormFile fileEpisode)
        {
            if (ModelState.IsValid)
            {
                var result = await _onlineClassService.CreateOnlineClassEpisode(CreateOnlineClassEpisodeDTO, fileEpisode);
                switch (result)
                {
                    case OnlineClassEpisodeResult.Error:
                        TempData[ErrorMessage] = "خطا در ارتباط";
                        break;
                    case OnlineClassEpisodeResult.NotFile:
                        TempData[WarningMessage] = "فایل را آپلود کنید";
                        break;
                    case OnlineClassEpisodeResult.ErrorFile:
                        TempData[ErrorMessage] = "نام فایل معتبر نیست ، لطفا تغییر دهید";
                        break;
                    case OnlineClassEpisodeResult.Success:
                        TempData[SuccessMessage] = "عملبات با موفقیت انجام شد";
                        return Redirect("/Admin/OnlineClassEpisode/Index/" + CreateOnlineClassEpisodeDTO.OnlineClassId);
                }
            }
            return Page();
        }
    }
}
