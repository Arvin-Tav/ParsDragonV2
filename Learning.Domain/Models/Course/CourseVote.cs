using Learning.Domain.Models.Account;
using Learning.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Learning.Domain.Models.Course
{
    public class CourseVote : BaseEntity
    {
        [Key]
        public int VoteId { get; set; }
        [Required]
        public int CourseId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public bool Vote { get; set; }


        #region Relation
        public virtual User User { get; set; }
        public virtual Course Course { get; set; }
        #endregion
    }
}
