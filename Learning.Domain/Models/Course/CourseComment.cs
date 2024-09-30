using Learning.Domain.Models.Account;
using Learning.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Learning.Domain.Models.Course
{
    public class CourseComment : BaseEntity
    {
        [Key]
        public int CommentId { get; set; }
        public int CourseId { get; set; }
        public int UserId { get; set; }

        [MaxLength(700)]
        public string Comment { get; set; }
        public bool IsAdminRead { get; set; }


        #region Relation
        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        #endregion
    }
}
