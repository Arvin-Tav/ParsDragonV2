using Learning.Application.Interfaces;
using Learning.Application.Security;
using Learning.Domain.DTO.Banner;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Learning.Mvc.Pages.Admin.Banners
{
    [PermissionChecker("Banners")]
    public class IndexModel : AdminBaseModel
    {
        private readonly IBannerService _bannerService;

        public IndexModel(IBannerService bannerService)
        {
            _bannerService = bannerService;
        }
        public FilterBannerDTO FilterBannerDTO { get; set; }
        public async Task OnGet(FilterBannerDTO filter)
        {
            FilterBannerDTO = await _bannerService.FilterBanner(filter);
            if (FilterBannerDTO == null)
            {
                RedirectToPage("Index");
            }
        }
        public async Task<IActionResult> OnGetDeleteBanner(int code)
        {
            var result =await _bannerService.DeleteBanner(code);
            switch (result)
            {
                case DeleteBannerResult.NotFound:
                    break;
                case DeleteBannerResult.Success:
                    return Content("true");
            }
            return Content("false");

        }
    }
}
