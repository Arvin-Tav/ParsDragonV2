using Learning.Domain.Models.Account;
using Learning.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.Models.OnlineClass
{
    public class UserOnlineClass
    {
        [Key]
        public int UCO_Id { get; set; }
        public int UserId { get; set; }
        public int OnlineClassId { get; set; }


        #region Raltion
        public virtual User User { get; set; }
        public virtual OnlineClass OnlineClass { get; set; }
        #endregion
    }
}
