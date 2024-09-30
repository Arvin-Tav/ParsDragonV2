using Learning.Domain.Models.Account;
using Learning.Domain.Models.Common;
using Learning.Domain.Models.Course;
using Learning.Domain.Models.Order;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.Models.OnlineClass
{
    public class OnlineClass : BaseEntity
    {
        public int OnlineClassId { get; set; }
        [Required]
        public int TeacherId { get; set; }

        [Required]
        public int GroupId { get; set; }

        public int? SubGroup { get; set; }
        [StringLength(200)]
        [MaxLength(200)]
        public string OnlineClassImageName { get; set; }
        [Display(Name = "موضوع ")]
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
        [MaxLength(150)]
        public string BodyHtml { get; set; }
        [Display(Name = "زمان شروع کلاس ")]
        [Required(ErrorMessage = "لطفا{0} را وارد کنید")]
        [DataType(DataType.Date)]
        public DateTime StartClass { get; set; }


        [Display(Name = "قیمت ")]
        [Required(ErrorMessage = "لطفا{0} را وارد کنید")]
        public int Price { get; set; }

        [Display(Name = "تعداد جلسات ")]
        [Required(ErrorMessage = "لطفا{0} را وارد کنید")]
        public int NumberSessions { get; set; }

        [Display(Name = "طول دوره")]
        [Required(ErrorMessage = "لطفا{0} را وارد کنید")]
        [Range(0, 999, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        public int TimeSessions { get; set; }

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

        public bool IsSlider { get; set; }

        public bool IsShow { get; set; }





        #region Relation
        [ForeignKey("GroupId")]
        public virtual CourseGroup OnlineClassGroup { get; set; }

        [ForeignKey("SubGroup")]
        public virtual CourseGroup OnlineClassSubGroup { get; set; }
        [ForeignKey("TeacherId")]
        public virtual User User { get; set; }
        public virtual List<OnlineClassContact> OnlineClassContacts { get; set; }
        public virtual List<OnlineClassGoal> OnlineClassGoals { get; set; }
        public virtual List<OnlineClassHeading> OnlineClassHeadings { get; set; }
        public virtual List<OnlineClassOfDay> OnlineClassOfDays { get; set; }
        public virtual List<OnlineClassPrerequisite> OnlineClassPrerequisites { get; set; }
        public virtual List<UserOnlineClass> UserClassOnlines { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }
        public virtual List<OnlineClassEpisode> OnlineClassEpisodes { get; set; }

        #endregion
    }
}
