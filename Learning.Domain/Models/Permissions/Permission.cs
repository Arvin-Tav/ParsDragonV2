using Learning.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Learning.Domain.Models.Permissions
{
    public class Permission : BaseEntity
    {
        [Key]
        public int PermissionId { get; set; }

        [Display(Name = "عنوان نقش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string PermissionTitle { get; set; }
        [MaxLength(150)]
        public string PermissionName { get; set; }
        public int? ParentID { get; set; }


        [ForeignKey("ParentID")]
        public virtual List<Permission> Permissions { get; set; }

        public virtual List<RolePermission> RolePermissions { get; set; }

    }
}
