using Learning.Domain.Models.Common;
using Learning.Domain.Models.Permissions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Learning.Domain.Models.Account
{
    public class Role: BaseEntity
    {
        public Role()
        {
            UserRoles = new List<UserRole>();
        }
        [Key]
        public int RoleId { get; set; }
        [Display(Name ="عنوان نقش")]
        [Required(ErrorMessage ="لطفا{0} را وارد کنید")]
        [MaxLength(200,ErrorMessage ="{0} نمیتواند بیشتر از {1} باشد")]
        public string RoleTitle { get; set; }

        #region Relations
        public virtual List<UserRole> UserRoles { get; set; }
        public virtual List<RolePermission> RolePermissions { get; set; }
        #endregion
    }
}
