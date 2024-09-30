using Learning.Application.Interfaces;
using Learning.Application.Security;
using Learning.Domain.DTO.Banner;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Learning.Mvc.Pages.Admin.Banners
{
    [PermissionChecker("Banners")]
    public class EditBannerModel : AdminBaseModel
    {
        private readonly IBannerService _bannerService;
        public EditBannerModel(IBannerService bannerService)
        {
            _bannerService = bannerService;
        }
        [BindProperty]
        public EditBannerDTO EditBannerDTO { get; set; }
        public async Task OnGet(int id)
        {
            EditBannerDTO = await _bannerService.GetBannerForEdit(id);
        }
        public async Task<IActionResult> OnPost(IFormFile imageBanner)
        {
            if (ModelState.IsValid)
            {
                var result = await _bannerService.EditeBaneer(EditBannerDTO, imageBanner);
                switch (result)
                {
                    case BannerResult.Error:
                        TempData[ErrorMessage] = "خطا در ارتباط";
                        break;
                    case BannerResult.NotImage:
                        TempData[ErrorMessage] = "عکس را آپلود کنید";
                        break;
                    case BannerResult.ErrorImage:
                        TempData[ErrorMessage] = "پسوند عکس مشکل دارد لطفا عکس انتخاب کنید";
                        break;
                    case BannerResult.Success:
                        TempData[SuccessMessage] = "عملبات با موفقیت انجام شد";
                        return Redirect("/Admin/Banners/Index");
                }
            }

            return Page();

        }
    }
}
