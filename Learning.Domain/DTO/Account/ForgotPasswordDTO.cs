using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.DTO.Account
{
    public class ForgotPasswordDTO
    {
        [Display(Name = "موبایل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Mobile { get; set; }
    }
    public enum ForgotPasswordResult
    {
        Error,
        NotFound,
        IsBlocked,
        Success,
    }
}
