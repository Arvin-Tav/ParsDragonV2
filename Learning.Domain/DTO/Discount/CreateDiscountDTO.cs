using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.DTO.Discount
{
    public class CreateDiscountDTO
    {
        [Display(Name = "کد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(150)]
        public string DiscountCode { get; set; }

        [Display(Name = "درصد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int DiscountPercent { get; set; }
        [Display(Name = "حذف شده ؟")]
        public int? UsableCount { get; set; }

        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
