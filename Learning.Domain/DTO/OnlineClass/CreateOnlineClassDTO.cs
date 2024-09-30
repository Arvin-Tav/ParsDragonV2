using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.DTO.OnlineClass
{
    public class CreateOnlineClassDTO
    {
        [Required]
        public int TeacherId { get; set; }

        [Required]
        public int GroupId { get; set; }

        public int? SubGroup { get; set; }

        [Display(Name = "نام یا عنوان کلاس ")]
        [Required(ErrorMessage = "لطفا{0} را وارد کنید")]
        [StringLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        public string Title { get; set; }

        [Display(Name = "توضیح مختصر ")]
        [Required(ErrorMessage = "لطفا{0} را وارد کنید")]
        [StringLength(500, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        [MaxLength(500, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        public string Description { get; set; }
        [Display(Name = "شرح")]
        [Required(ErrorMessage = "لطفا{0} را وارد کنید")]
        public string BodyHtml { get; set; }

        [Display(Name = "زمان شروع کلاس ")]
        [Required(ErrorMessage = "لطفا{0} را وارد کنید")]
        public string StartClass { get; set; }

        [Display(Name = "قیمت ")]
        [Required(ErrorMessage = "لطفا{0} را وارد کنید")]
        public int? Price { get; set; }

        [Display(Name = "تعداد جلسات ")]
        [Required(ErrorMessage = "لطفا{0} را وارد کنید")]
        [Range(0, 9999, ErrorMessage = "{0} نمیتواند بیشتر از {2} باشد")]
        public int? NumberSessions { get; set; }

        [Display(Name = "طول دوره")]
        [Required(ErrorMessage = "لطفا{0} را وارد کنید")]
        [Range(0, 9999, ErrorMessage = "{0} نمیتواند بیشتر از {2} باشد")]
        public int? TimeSessions { get; set; }

        [Display(Name = "شماره تلفن")]
        [Required(ErrorMessage = "لطفا{0} را وارد کنید")]
        [StringLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        public string Tell { get; set; }
        [Display(Name = "آدرس ")]
        [Required(ErrorMessage = "لطفا{0} را وارد کنید")]
        [StringLength(300, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        [MaxLength(300, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        public string Address { get; set; }
        [Display(Name = "کلمات کلیدی ")]
        [Required(ErrorMessage = "لطفا{0} را وارد کنید")]
        [StringLength(300, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        [MaxLength(300, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        public string Tags { get; set; }

        [Display(Name = "لینک کلاس ")]
        [Required(ErrorMessage = "لطفا{0} را وارد کنید")]
        [StringLength(300, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        [MaxLength(300, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        public string Link { get; set; }

        public string AvatarOnlineClassName { get; set; }
    }
}
