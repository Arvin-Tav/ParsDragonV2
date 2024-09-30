using Learning.Domain.Models.Common;
using Learning.Domain.Models.Order;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Learning.Domain.Models.Account
{
    public class UserDiscountCode : BaseEntity
    {
        [Key]
        public int UD_Id { get; set; }
        public int UserId { get; set; }
        public int DiscountId { get; set; }
        public int OrderId { get; set; }

        #region Relation
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        [ForeignKey("DiscountId")]
        public virtual Discount Discount { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order.Order Order{ get; set; }
        #endregion
    }
}
