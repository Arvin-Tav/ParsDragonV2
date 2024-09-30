using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Learning.Domain.Models.Account;
using Learning.Domain.Models.Common;
using Learning.Domain.Models.Order;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Learning.Domain.Models.Course
{
    public class Course : BaseEntity
    {
        [Key]
        public int CourseId { get; set; }

        [Required]
        public int GroupId { get; set; }

        public int? SubGroup { get; set; }

        [Required]
        public int TeacherId { get; set; }

        [Required]
        public int StatusId { get; set; }

        [Required]
        public int LevelId { get; set; }

        [Display(Name = "عنوان دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(450, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string CourseTitle { get; set; }

        [Display(Name = "شرح دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string CourseDescription { get; set; }

        [Display(Name = "قیمت دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int CoursePrice { get; set; }

        [MaxLength(600)]
        public string Tags { get; set; }

        [MaxLength(50)]
        public string CourseImageName { get; set; }

        [MaxLength(100)]
        public string DemoFileName { get; set; }

        public bool IsSuggested { get; set; }
        public bool IsShow { get; set; }






        #region Relations

        [ForeignKey("TeacherId")]
        public virtual User User { get; set; }

        [ForeignKey("GroupId")]
        public virtual CourseGroup CourseGroup { get; set; }

        [ForeignKey("SubGroup")]
        public virtual CourseGroup Groupe { get; set; }
        [ForeignKey("StatusId")]
        public virtual CourseStatus CourseStatus { get; set; }
        [ForeignKey("LevelId")]
        public virtual CourseLevel CourseLevel { get; set; }
        public virtual List<CourseEpisode> CourseEpisodes { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }
        public virtual List<UserCourse> UserCourses { get; set; }
        public virtual List<CourseComment> CourseComments { get; set; }
        public virtual List<CourseVote> CourseVotes { get; set; }
        public virtual List<Question.Question> Questions { get; set; }

        #endregion
    }
}
