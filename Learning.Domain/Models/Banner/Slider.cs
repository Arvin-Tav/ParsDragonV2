using Learning.Domain.Models.Common;
using Learning.Domain.Models.Course;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.Models.Banner
{
    public class Slider : BaseEntity
    {
        [Key]
        public int SliderId { get; set; }
        [Required(ErrorMessage = "نام اسلایدر الزامی میباشد")]
        [StringLength(100, ErrorMessage = "نام اسلایدر مناسب وارد کنید")]
        [MaxLength(150)]
        public string Name { get; set; }
        [MaxLength(150)]
        public string SliderImageName { get; set; }
        [Required]
        public int GroupId { get; set; }

        public int? SubGroup { get; set; }

        public bool IsOnlineClass { get; set; }


        #region Relation
        [ForeignKey("GroupId")]
        public virtual CourseGroup SliderGroup { get; set; }

        [ForeignKey("SubGroup")]
        public virtual CourseGroup SliderSubGroup { get; set; }
        #endregion
    }
}
