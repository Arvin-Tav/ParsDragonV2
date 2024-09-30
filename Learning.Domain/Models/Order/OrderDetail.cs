using Learning.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Learning.Domain.Models.Order
{
    public class OrderDetail : BaseEntity
    {
        [Key]
        public int DetailId { get; set; }
        [Required]
        public int OrderId { get; set; }
        public int? OnlineClassId { get; set; }
        public int? CourseId { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        public int Price { get; set; }

        #region Relation
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
        [ForeignKey("CourseId")]
        public virtual Course.Course Course { get; set; }
        [ForeignKey("OnlineClassId")]
        public virtual OnlineClass.OnlineClass OnlineClass { get; set; }
        #endregion
    }
}
