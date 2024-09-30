using Learning.Domain.Models.Account;
using Learning.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Learning.Domain.Models.Course
{
    public class UserCourse 
    {
        [Key]
        public int UC_Id { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }


        #region Raltion
        public virtual User User { get; set; }
        public virtual Course Course { get; set; }
        #endregion
    }
}
