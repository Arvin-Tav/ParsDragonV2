
using Learning.Application.Interfaces;
using Learning.Application.Security;
using Learning.Domain.DTO.Config;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Learning.Mvc.Pages.Admin.Config
{
    [PermissionChecker("Config")]
    public class IndexModel : AdminBaseModel
    {
        private readonly IConfigService _configService;
        public IndexModel(IConfigService utilities)
        {
            _configService = utilities;
        }
        [BindProperty]
        public ConfigDTO ConfigDTO { get; set; }
        public async Task OnGet()
        {
            ConfigDTO = await _configService.GetAllCongigs();
        }
        public async Task<IActionResult> OnPost(IFormFile akseSafeyeAsly, IFormFile aksTamasBama)
        {
            if (ModelState.IsValid)
            {
                var result = await _configService.EditConfig(ConfigDTO, akseSafeyeAsly, aksTamasBama);
                switch (result)
                {
                    case ConfigReult.Error:
                        TempData[ErrorMessage] = "خطا در ارتباط";
                        break;
                    case ConfigReult.ErrorImage:
                        TempData[ErrorMessage] = "پسوند عکس مشکل دارد لطفا عکس انتخاب کنید";
                        break;
                    case ConfigReult.Success:
                        TempData[SuccessMessage] = "عملبات با موفقیت انجام شد";
                        break;
                }
            }
            return Page();
        }
    }
}
