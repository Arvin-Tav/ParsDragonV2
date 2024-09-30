using Learning.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Learning.Domain.Models.Course
{
   public class CourseLevel : BaseEntity
    {
        [Key]
        public int LevelId { get; set; }

        [MaxLength(150)]
        [Required]
        public string LevelTitle { get; set; }

        public virtual List<Course> Courses { get; set; }
    }
}
