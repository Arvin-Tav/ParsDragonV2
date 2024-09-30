using Learning.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Learning.Domain.Models.Course
{
    public class CourseEpisode : BaseEntity
    {
        [Key]
        public int EpisodeId { get; set; }

        public int CourseId { get; set; }

        [Display(Name = "عنوان بخش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(400, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string EpisodeTitle { get; set; }

        [Display(Name = "زمان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public TimeSpan EpisodeTime { get; set; }

        [Display(Name = "فایل")]
        [MaxLength(150)]
        public string EpisodeFileName { get; set; }

        [Display(Name = "رایگان")]
        public bool IsFree { get; set; }
        [Display(Name = "نمایش")]
        public bool IsShow { get; set; }


        #region Relation
        public virtual Course Course { get; set; }
        #endregion
    }
}
