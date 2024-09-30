﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.DTO.Account
{
    public class CreateUserByAdminDTO
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string UserName { get; set; }

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

        [Display(Name = "کیف پول ")]
        public int? Wallet { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
        public string Email { get; set; }
        [Display(Name = "شماره تلفن")]
        [Required(ErrorMessage = "لطفا{0} را وارد کنید")]
        [MaxLength(15, ErrorMessage = "تعداد کاراکتر بیشتر است")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("[0]{1}[9]{1}[0-9]{9}", ErrorMessage = "شماره تلفن وارد شده معتبر نمی باشد")]
        [StringLength(15)]
        public string Mobile { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Password { get; set; }
        public int AdminId { get; set; }
    }

    public enum CreateUserByAdminResut
    {
        Error,
        ErrorImage,
        MobileExists,
        EmailExists,
        UsernameExists,
        Success,
    }
}
