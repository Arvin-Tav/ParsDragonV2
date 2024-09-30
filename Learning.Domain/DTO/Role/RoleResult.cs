using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.DTO.Role
{
    public enum RoleResult
    {
        NotFound,
        ErrorRoleTitleName,
        Success,
    }
    public enum DeleteRoleResult
    {
        NotFound,
        Success
    }
}
