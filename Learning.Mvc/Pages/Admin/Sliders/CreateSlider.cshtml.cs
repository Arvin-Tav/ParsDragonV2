using Learning.Application.Interfaces;
using Learning.Application.Security;
using Learning.Domain.DTO.Slider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace Learning.Mvc.Pages.Admin.Sliders
{
    [PermissionChecker("Banners")]
    public class CreateSliderModel : AdminBaseModel
    {
        private readonly ISliderService _sliderService;
        private readonly ICourseService _courseService;

        public CreateSliderModel(ISliderService sliderService, ICourseService courseService)
        {
            _sliderService = sliderService;
            _courseService = courseService;

        }
        [BindProperty]
        public CreateSliderDTO CreateSliderDTO { get; set; }
        public async Task OnGet()
        {
            var groups = await _courseService.GetGroupForManageCourse();
            ViewData["Groups"] = new SelectList(groups, "Value", "Text");

            var subGrous = await _courseService.GetSubGroupForManageCourse(int.Parse(groups.First().Value));
            ViewData["SubGroups"] = new SelectList(subGrous, "Value", "Text");
        }
        public async Task<IActionResult> OnPost(IFormFile imageSlider)
        {
            if (ModelState.IsValid)
            {
                var result = await _sliderService.CreateSLider(CreateSliderDTO, imageSlider);
                switch (result)
                {
                    case SliderResult.Error:
                        TempData[ErrorMessage] = "خطا در ارتباط";
                        break;
                    case SliderResult.NotImage:
                        TempData[ErrorMessage] = "عکس را آپلود کنید";
                        break;
                    case SliderResult.ErrorImage:
                        TempData[ErrorMessage] = "پسوند عکس مشکل دارد لطفا عکس انتخاب کنید";
                        break;
                    case SliderResult.Success:
                        TempData[SuccessMessage] = "عملبات با موفقیت انجام شد";
                        return Redirect("/Admin/Sliders/Index");
                }
            }
            var groups = await _courseService.GetGroupForManageCourse();
            ViewData["Groups"] = new SelectList(groups, "Value", "Text");

            var subGrous = await _courseService.GetSubGroupForManageCourse(int.Parse(groups.First().Value));
            ViewData["SubGroups"] = new SelectList(subGrous, "Value", "Text");
            return Page();

        }
    }
}
