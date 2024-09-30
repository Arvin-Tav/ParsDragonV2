using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Learning.Application.Interfaces;
using Learning.Application.Security;
using Learning.Domain.DTO.Discount;
using Learning.Domain.Models.Order;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Learning.Mvc.Pages.Admin.Discounts
{
    [PermissionChecker("Courses")]
    public class IndexModel : AdminBaseModel
    {
        private readonly IDiscountService _discountService;
        public IndexModel(IDiscountService discountService)
        {
            _discountService = discountService;
        }
        public FilterDiscountDTO FilterDiscountDTO { get; set; }
        public async Task OnGet(FilterDiscountDTO filter)
        {
            FilterDiscountDTO = await _discountService.FilterDiscount(filter);
        }


        public async Task<IActionResult> OnGetDeleteDiscounts(int code)
        {
            try
            {
                var result = await _discountService.DeleteDiscount(code);
                switch (result)
                {
                    case DeleteDiscountResult.NotFound:
                        break;
                    case DeleteDiscountResult.Success:
                        return Content("true");
                }
                return Content("false");

            }
            catch
            {
                return Content("false");

            }

        }
    }
}
