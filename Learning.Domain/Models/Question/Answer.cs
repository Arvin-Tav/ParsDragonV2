using Learning.Domain.Models.Account;
using Learning.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.Models.Question
{
    public class Answer : BaseEntity
    {
        [Key]
        public int AnswerId { get; set; }
        [Required]
        public int QuestionId { get; set; }
        [Required]
        public int UserId { get; set; }
        [MaxLength(150)]
        public string BodyAnswer { get; set; }
        public bool IsTrue { get; set; } = false;
        public bool IsAdminRead { get; set; }



        #region Relation
        public virtual Question Question { get; set; }
        public virtual User User { get; set; }
        #endregion
    }
}
