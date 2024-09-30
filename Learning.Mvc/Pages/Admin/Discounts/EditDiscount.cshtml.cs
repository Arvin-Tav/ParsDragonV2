using Learning.Application.Interfaces;
using Learning.Application.Security;
using Learning.Domain.DTO.Discount;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Learning.Mvc.Pages.Admin.Discounts
{
    [PermissionChecker("Courses")]
    public class EditDiscountModel : AdminBaseModel
    {
        private IDiscountService _discountService;

        public EditDiscountModel(IDiscountService discountService)
        {
            _discountService = discountService;
        }
        [BindProperty]
        public EditDiscountDTO EditDiscountDTO { get; set; }

        public async void OnGet(int id)
        {
            EditDiscountDTO =await _discountService.GetDiscountForEdit(id);
        }

        public async Task<IActionResult> OnPost(string stDate = "", string edDate = "")
        {
            if (ModelState.IsValid)
            {
                var result = await _discountService.EditDiscount(EditDiscountDTO);
                switch (result)
                {
                    case DiscountResult.NotFound:
                        TempData[ErrorMessage] = "موردی بافت نشد";
                        break;
                    case DiscountResult.CountAndDateIsNull:
                        ModelState.AddModelError("Discount.UsableCount", "تاریخ یا تعداد یکی باید پر شود");
                        TempData[ErrorMessage] = "تاریخ یا تعداد یکی باید پر شود";
                        break;
                    case DiscountResult.ErrorDiscountName:
                        ModelState.AddModelError("Discount.DiscountCode", "کد تخفیف تکراریست");
                        TempData[ErrorMessage] = "کد تخفیف تکراریست";
                        break;
                    case DiscountResult.Success:
                        return RedirectToPage("Index");
                }
            }
            return Page();
        }
    }
}
