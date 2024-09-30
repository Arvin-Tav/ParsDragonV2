using Learning.Domain.Models.Account;
using Learning.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.Models.Question
{
    public class Question : BaseEntity
    {
        [Key]
        public int QuestionId { get; set; }
        [Required]
        public int CourseId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Display(Name ="عنوان سوال")]
        [Required(ErrorMessage ="عنوان سوال را وارد کنید")]
        [MaxLength(400)]
        public string Title { get; set; }
        [Display(Name ="متن سوال")]
        [Required(ErrorMessage = "متن سوال را وارد کنید")]
        [MaxLength(150)]
        public string Body { get; set; }
        [Required]
        public DateTime ModifiedDate { get; set; }
        public bool IsAdminRead { get; set; }


        #region Relation
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        [ForeignKey("CourseId")]
        public virtual Course.Course Course { get; set; }

        public virtual List<Answer> Answers{ get; set; }
        #endregion
    }
}
