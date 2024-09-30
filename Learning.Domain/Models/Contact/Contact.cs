using Learning.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.Models.Contact
{
    public class Contact : BaseEntity
    {
        [Key]
        public int ContactId { get; set; }
        [Display(Name = " نام و نام خانوادگی")]
        [StringLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        [Required(ErrorMessage = "لطفا{0} را وارد کنید")]

        public string Name { get; set; }
        [Display(Name = " تلفن")]
        [StringLength(15, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        [MaxLength(15, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        [Required(ErrorMessage = "لطفا{0} را وارد کنید")]
        public string TellNo { get; set; }
        [Display(Name = " ایمیل")]
        [StringLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        [Required(ErrorMessage = "لطفا{0} را وارد کنید")]
        [EmailAddress(ErrorMessage ="لطفا ایمیل خود را وارد کنید")]
        public string Email { get; set; }

        [Display(Name = " پیام ")]
        [StringLength(800, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        [MaxLength(800, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        [Required(ErrorMessage = "لطفا{0} را وارد کنید")]
        public string Message { get; set; }
        public bool IsSee { get; set; }
    }
}
