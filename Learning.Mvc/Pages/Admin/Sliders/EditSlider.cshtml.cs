using Learning.Application.Interfaces;
using Learning.Application.Security;
using Learning.Domain.DTO.Slider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learning.Mvc.Pages.Admin.Sliders
{
    [PermissionChecker("Banners")]
    public class EditSliderModel : AdminBaseModel
    {
        private readonly ICourseService _courseService;
        private readonly ISliderService _sliderService;
        public EditSliderModel(ISliderService sliderService, ICourseService courseService)
        {
            _courseService = courseService;
            _sliderService = sliderService;
        }
        [BindProperty]
        public EditSliderDTO EditSliderDTO { get; set; }
        public async Task OnGet(int id)
        {
            EditSliderDTO = await _sliderService.EditSlider(id);
            var groups = await _courseService.GetGroupForManageCourse();
            ViewData["Groups"] = new SelectList(groups, "Value", "Text", EditSliderDTO.GroupId);

            List<SelectListItem> subgroups = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "انتخاب کنید",Value = ""}
            };
            subgroups.AddRange(await _courseService.GetSubGroupForManageCourse(EditSliderDTO.GroupId));
            string selectedSubGroup = null;
            if (EditSliderDTO.SubGroup != null)
            {
                selectedSubGroup = EditSliderDTO.SubGroup.ToString();
            }
            ViewData["SubGroups"] = new SelectList(subgroups, "Value", "Text", selectedSubGroup);

        }
        public async Task<IActionResult> OnPost(IFormFile imageSlider)
        {
            if (ModelState.IsValid)
            {
                var result = await _sliderService.GetSliderForEdit(EditSliderDTO, imageSlider);
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

            return Page();

        }
    }
}
