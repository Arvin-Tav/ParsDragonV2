using Learning.Domain.Models.Common;
using Learning.Domain.Models.Course;
using Learning.Domain.Models.OnlineClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Learning.Domain.Models.Account
{
    public class User: BaseEntity
    {
        public User()
        {
            UserRoles = new List<UserRole>();
            Wallets = new List<Wallet.Wallet>();
        }
        [Key]
        public int UserId { get; set; }
        [Display(Name = "نام ")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        public string? FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        public string? LastName { get; set; }
       
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا{0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        public string? UserName { get; set; }
        [Display(Name = "شماره تلفن")]
        [Required(ErrorMessage = "لطفا{0} را وارد کنید")]
        [MaxLength(20, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        public string? Mobile { get; set; }
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا{0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
        public string? Email { get; set; }
        [Display(Name = "بیوگرافی ")]
        [MaxLength(2000, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        public string? AboutMe { get; set; }
        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا{0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        public string? Password { get; set; }

        [Display(Name = "کد فعال سازی")]
        [MaxLength(50, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]

        public string? ActiveCodeEmail { get; set; }
        [Display(Name = "کد فعال سازی")]
        [MaxLength(50, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]

        public string? ActiveCodeMobile { get; set; }
        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }
        public bool IsEmailActive { get; set; }
        public bool IsBlocked { get; set; }

        [Display(Name = "آواتار")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        public string UserAvatar { get; set; }



        #region Relations
        public virtual List<UserRole> UserRoles { get; set; }
        public virtual List<Wallet.Wallet> Wallets { get; set; }
        public virtual List<Course.Course> Courses { get; set; }
        public virtual List<UserCourse> UserCourses { get; set; }
        public virtual List<UserOnlineClass> UserClassOnlines { get; set; }
        public virtual List<Order.Order> Orders{ get; set; }
        public virtual List<UserDiscountCode> UserDiscountCodes{ get; set; }
        public virtual List<CourseComment> CourseComments { get; set; }
        public virtual List<CourseVote> CourseVotes { get; set; }
        public virtual List<Question.Question> Questions { get; set; }
        public virtual List<Blog.Blog> Blogs { get; set; }
        public virtual List<OnlineClass.OnlineClass> OnlineClasses{ get; set; }
        public virtual List<Course.CourseDiscount> CourseDiscounts{ get; set; }
        #endregion
    }
}
