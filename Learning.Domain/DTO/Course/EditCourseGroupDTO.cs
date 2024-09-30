using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.DTO.Course
{
    public class EditCourseGroupDTO
    {
        public int GroupId { get; set; }

        [Display(Name = "عنوان گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string GroupTitle { get; set; }
        [Display(Name = "عنوان url")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "لطفا url را انگلیسی تایپ کنید")]

        public string UrlName { get; set; }
        [Display(Name = "گروه اصلی")]
        public int? ParentId { get; set; }
    }
}
