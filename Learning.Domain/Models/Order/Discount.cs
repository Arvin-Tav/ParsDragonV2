using Learning.Domain.Models.Account;
using Learning.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Learning.Domain.Models.Order
{
    public class Discount : BaseEntity
    {
        [Key]
        public int DiscountId { get; set; }


        [Display(Name = "کد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(150)]
        public string DiscountCode { get; set; }

        [Display(Name = "درصد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int DiscountPercent { get; set; }
        [Display(Name = "حذف شده ؟")]
        public int? UsableCount { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }


        #region Relation
        public virtual List<UserDiscountCode> UserDiscountCodes { get; set; }

        #endregion
    }
}
