using Learning.Domain.DTO.Master;
using Learning.Domain.DTO.Permission;
using Learning.Domain.DTO.Role;
using Learning.Domain.Models.Account;
using Learning.Domain.Models.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Application.Interfaces
{
    public interface IPermissionService
    {
        #region roles
        Task<List<User>> GetAllowMasterUser();
        Task<List<User>> GetAllowRolesUser();
        Task<List<int>> GetAllowRolesUserId();
        Task<List<int>> GetAllowMasterUserId();
        Task<FilterMasterDTO> FilterMaster(FilterMasterDTO filter);

        Task<InfoMasterDTO> GetInfoMaster(string userName);

        Task<EditRoleDTO> GetRoleForEdit(int roleId);
        Task<List<Role>> GetRoles();
        Task<List<Role>> GetRolesJustAdmin();
        Task<int> AddRole(Role role);
        Task<RoleResult> CreateRolePermission(CreateRoleDTO createRole);
        Task<RoleResult> EditRolePermission(EditRoleDTO editRole);
        Task AddUserRole(List<int> roleIds, int userId);
        Task EditUserRole(List<int> roleIds, int userId);
        Task<List<int>> GetRoleIdByUserId(int userId);
        Task DeleteUserRole(int userId);
        Task<DeleteRoleResult> DeleteRole(int roleId);
        Task<Role> GetRoleById(int roleId);
        Task UpdateRole(Role role);
        Task<bool> IsExistRole(string name);
        Task<bool> IsExistEditRole(int roleId, string name);
        #endregion


        #region permission
        Task<List<Permission>> GetAllPermission();
        Task CretePermissions(int roleId, List<int> permission);
        Task<List<int>> GetPermissionsIdByRoleId(int roleId);
        Task<int> GetPermisionIdByName(string name);
        Task UpdatePermissionsRole(int roleId, List<int> permissions);
        Task<bool> CheckPermission(string permissionId, int userId);
        Task<List<string>> GetPermissionNamesByUserId(int userId);
        #endregion
    }
}
