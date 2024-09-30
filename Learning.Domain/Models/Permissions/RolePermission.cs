using Learning.Domain.Models.Account;
using Learning.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Learning.Domain.Models.Permissions
{
    public class RolePermission : BaseEntity
    {
        [Key]
        public int RP_Id { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
        [ForeignKey("PermissionId")]
        public virtual Permission Permission { get; set; }

    }
}
