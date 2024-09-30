using Learning.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Learning.Domain.Models.Course
{
    public class CourseStatus : BaseEntity
    {
        [Key]
        public int StatusId { get; set; }

        [Required]
        [MaxLength(150)]
        public string StatusTitle { get; set; }


        #region Relation
        public virtual List<Course> Courses { get; set; }
        #endregion

    }
}
