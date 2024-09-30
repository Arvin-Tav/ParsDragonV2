using Learning.Domain.Models.Account;
using Learning.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Learning.Domain.Models.Order
{
    public class Order : BaseEntity
    {
        [Key]
        public int OrderId { get; set; }
        [Required]
        public int UserId { get; set; }
        public bool IsFinaly { get; set; }
        [Required]
        public int OrderSum { get; set; }
        public int DiscountPrice { get; set; }
        public int DiscountPercent { get; set; }
        public bool IsOnlineClass { get; set; }

        [StringLength(15)]
        [MaxLength(150)]
        public string Ip { get; set; }



        #region Relation
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }
        public virtual List<UserDiscountCode> UserDiscountCodes { get; set; }
        #endregion

    }
}
