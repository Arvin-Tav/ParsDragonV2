using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Learning.Domain.DTO.UserPanel
{
    public class EditProfileDTO
    {
        [Display(Name = "نام ")]
        [Required(ErrorMessage = "لطفا{0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "لطفا{0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        public string LastName { get; set; }
        [Display(Name = "بیوگرافی ")]
        [MaxLength(2000, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        public string AboutMe { get; set; }
    }
}
