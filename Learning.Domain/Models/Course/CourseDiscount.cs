using Learning.Domain.Models.Account;
using Learning.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.Models.Course
{
    public class CourseDiscount : BaseEntity
    {
        [Key]
        public int CourseDiscountId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int Discount { get; set; }


        #region Relations
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        #endregion
    }
}
