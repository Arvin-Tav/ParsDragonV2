using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Learning.Application.Interfaces;
using Learning.Application.Security;
using Learning.Domain.DTO.Discount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Learning.Mvc.Pages.Admin.Discounts
{
    [PermissionChecker("Courses")]
    public class CreateDiscountModel : AdminBaseModel
    {
        private readonly IDiscountService _discountService;
        public CreateDiscountModel(IDiscountService discountService)
        {
            _discountService = discountService;
        }
        [BindProperty]
        public CreateDiscountDTO CreateDiscountDTO { get; set; }
        public void OnGet()
        {

        }


        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var result = await _discountService.CreateDiscount(CreateDiscountDTO);
                switch (result)
                {
                    case DiscountResult.NotFound:
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
                        TempData[SuccessMessage] = "عملبات با موفقیت انجام شد";
                        return RedirectToPage("Index");
                }
            }
            return Page();
        }

        //admin/Discounts/CreateDiscount?handler=checkcode
        //admin/Discounts/CreateDiscount/checkcode
        public IActionResult OnGetCheckCode(string code)
        {
            return Content(_discountService.IsExistCode(code).ToString());
        }
    }
}
