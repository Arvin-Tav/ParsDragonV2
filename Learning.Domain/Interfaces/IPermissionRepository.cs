using Learning.Domain.DTO.Master;
using Learning.Domain.DTO.Permission;
using Learning.Domain.Models.Account;
using Learning.Domain.Models.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.Interfaces
{
    public interface IPermissionRepository
    {

        #region roles
        Task<IQueryable<User>> GetAllowMasterUser();
        Task<IQueryable<MastersDTO>> GetInformationMaster();
        Task<List<User>> GetAllowRolesUser();
        Task<List<int>> GetAllowRolesUserId();
        Task<List<int>> GetAllowMasterUserId();
        Task<IQueryable<User>> GetAllowAdminUser();
        Task<List<int>> GetAllowAdminUserId();

        Task<FilterMasterDTO> FilterMaster(FilterMasterDTO filter);

        Task<InfoMasterDTO> GetInfoMaster(string userName);

        Task<List<Role>> GetRoles();
        Task<List<Role>> GetRolesJustAdmin();
        Task<int> AddRole(Role role);
        void UpdateRole(Role role);
        Task AddUserRole(UserRole userRole);
        Task<List<int>> GetRoleIdByUserId(int userId);

        void DeleteUserRoleByUserId(int userId);
        Task<Role> GetRoleById(int roleId);
        Task<bool> IsExistRoleTitle(string name);
        Task<bool> IsExistEditRoleTitle(int roleId, string name);
        #endregion


        #region permission
        Task<List<Permission>> GetAllPermission();
        Task AddRolePermission(RolePermission rolePermission);
        Task<List<int>> GetPermissionsIdByRoleId(int roleId);
        Task<int> GetPermisionIdByName(string name);
        void DeleteRolePermissionByRoleId(int roleId);
        Task<bool> CheckPermission(string permissionId,  int userId);
        Task<List<string>> GetPermissionNamesByUserId(int userId);
        #endregion

        Task Save();
    }
}
