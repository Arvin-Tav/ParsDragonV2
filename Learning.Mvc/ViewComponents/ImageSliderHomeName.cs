using Learning.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Learning.Mvc.ViewComponents
{
    public class ImageSliderHomeName : ViewComponent
    {
        private IConfigService _configService;

        public ImageSliderHomeName(IConfigService configService)
        {
            _configService = configService;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)Content(await _configService.GetSliderHomeImage()));
        }
    }
}
