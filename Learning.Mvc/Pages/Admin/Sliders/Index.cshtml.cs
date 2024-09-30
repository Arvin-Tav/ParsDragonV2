using Learning.Application.Interfaces;
using Learning.Application.Security;
using Learning.Domain.DTO.Slider;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Learning.Mvc.Pages.Admin.Sliders
{
    [PermissionChecker("Banners")]
    public class IndexModel : PageModel
    {
        private readonly ISliderService _sliderService;

        public IndexModel(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }
        public FilterSliderDTO FilterSliderDTO { get; set; }
        public async Task OnGet(FilterSliderDTO filter)
        {
            FilterSliderDTO =await _sliderService.FilterSlider(filter);
        }
        public async Task<IActionResult> OnGetDeleteSlider(int code)
        {
            var result = await _sliderService.DeleteSlider(code);
            switch (result)
            {
                case DeleteSliderResult.NotFound:
                    break;
                case DeleteSliderResult.Success:
                    return Content("true");
            }
            return Content("false");

        }
    }
}
