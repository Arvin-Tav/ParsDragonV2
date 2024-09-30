using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Learning.Domain.Models.Banner;
using Learning.Domain.Models.Common;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Learning.Domain.Models.Course
{
    public class CourseGroup : BaseEntity
    {
        [Key]
        public int GroupId { get; set; }

        [Display(Name = "عنوان گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string GroupTitle { get; set; }
        [Display(Name = "عنوان url")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string UrlName { get; set; }
        [Display(Name = "گروه اصلی")]
        public int? ParentId { get; set; }



        [ForeignKey("ParentId")]
        public virtual List<CourseGroup> CourseGroups { get; set; }

        [InverseProperty("CourseGroup")]
        public virtual List<Course> Courses { get; set; }

        [InverseProperty("Groupe")]
        public virtual List<Course> SubGroup { get; set; }

        [InverseProperty("SliderGroup")]
        public virtual List<Slider> SliderGroup { get; set; }

        [InverseProperty("SliderSubGroup")]
        public virtual List<Slider> SliderSubGroup { get; set; }


        [InverseProperty("OnlineClassGroup")]
        public virtual List<OnlineClass.OnlineClass> OnlineClassGroup { get; set; }

        [InverseProperty("OnlineClassSubGroup")]
        public virtual List<OnlineClass.OnlineClass> OnlineClassSubGroup { get; set; }

    }
}
