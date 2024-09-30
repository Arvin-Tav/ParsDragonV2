using Learning.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Learning.Domain.Models.Account
{
    public class UserRole : BaseEntity
    {
        public UserRole()
        {

        }



        [Key]
        public int UR_Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }

        #region Relations
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
        #endregion
    }
}
