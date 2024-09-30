
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
    public class CreateBannerModel : AdminBaseModel
    {
        private readonly IBannerService _bannerService;
        public CreateBannerModel(IBannerService bannerService)
        {
            _bannerService = bannerService;
        }
        [BindProperty]
        public CreateBannerDTO CreateBannerDTO { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost(IFormFile imageBanner)
        {
            if (ModelState.IsValid)
            {
                var result = await _bannerService.CreateBanner(CreateBannerDTO, imageBanner);
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
