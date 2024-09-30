using Learning.Application.Interfaces;
using Learning.Domain.DTO.Master;
using Learning.Domain.DTO.Permission;
using Learning.Domain.DTO.Role;
using Learning.Domain.Interfaces;
using Learning.Domain.Models.Account;
using Learning.Domain.Models.Permissions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Application.Services
{
    public class PermissionService : IPermissionService
    {
        #region constructor
        private readonly IPermissionRepository _permissionRepository;
        public PermissionService(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        #endregion

        #region roles
        public async Task<int> AddRole(Role role)
        {
            await _permissionRepository.AddRole(role);
            await _permissionRepository.Save();
            return role.RoleId;
        }
        public async Task<RoleResult> CreateRolePermission(CreateRoleDTO createRole)
        {
            if (await _permissionRepository.IsExistRoleTitle(createRole.RoleTitle)) return RoleResult.ErrorRoleTitleName;
            Role newRole = new Role()
            {
                RoleTitle = createRole.RoleTitle,
            };
            int roleId = await AddRole(newRole);
            if (createRole.SelectedPermission != null)
            {
                await CretePermissions(newRole.RoleId, createRole.SelectedPermission);
            }

            return RoleResult.Success;
        }
        public async Task<RoleResult> EditRolePermission(EditRoleDTO editRole)
        {
            Role role = await _permissionRepository.GetRoleById(editRole.RoleId);
            if (role == null) return RoleResult.NotFound;

            if (await _permissionRepository.IsExistEditRoleTitle(role.RoleId, editRole.RoleTitle)) return RoleResult.ErrorRoleTitleName;
            role.RoleTitle = editRole.RoleTitle;
            _permissionRepository.UpdateRole(role);
            _permissionRepository.DeleteRolePermissionByRoleId(role.RoleId);
            await CretePermissions(role.RoleId, editRole.SelectedPermission);

            return RoleResult.Success;
        }
        public async Task AddUserRole(List<int> roleIds, int userId)
        {
            foreach (var item in roleIds)
            {
                await _permissionRepository.AddUserRole(new UserRole
                {
                    RoleId = item,
                    UserId = userId,
                });
            }
            await _permissionRepository.Save();
        }
        public async Task<DeleteRoleResult> DeleteRole(int roleId)
        {
            var role = await _permissionRepository.GetRoleById(roleId);
            if (role == null) return DeleteRoleResult.NotFound;
            role.IsDelete = true;
            _permissionRepository.UpdateRole(role);
            await _permissionRepository.Save();
            return DeleteRoleResult.Success;
        }

        public async Task<EditRoleDTO> GetRoleForEdit(int roleId)
        {
            var role = await _permissionRepository.GetRoleById(roleId);
            if (role == null) return null;

            return new EditRoleDTO()
            {
                RoleId=role.RoleId,
                RoleTitle = role.RoleTitle,
                SelectedPermission = await _permissionRepository.GetPermissionsIdByRoleId(roleId),
            };
        }


        public async Task EditUserRole(List<int> roleIds, int userId)
        {
            await DeleteUserRole(userId);
            await AddUserRole(roleIds, userId);
        }
        public async Task<List<int>> GetRoleIdByUserId(int userId)
        {
            return await _permissionRepository.GetRoleIdByUserId(userId);
        }
        public async Task DeleteUserRole(int userId)
        {
            _permissionRepository.DeleteUserRoleByUserId(userId);
            await _permissionRepository.Save();
        }

        public async Task<FilterMasterDTO> FilterMaster(FilterMasterDTO filter)
        {
            return await _permissionRepository.FilterMaster(filter);
        }
        public async Task<InfoMasterDTO> GetInfoMaster(string userName)
        {
            return await _permissionRepository.GetInfoMaster(userName);
        }

        public async Task<List<User>> GetAllowMasterUser()
        {
            var result = await _permissionRepository.GetAllowMasterUser();
            return result.ToList();
        }

        public async Task<List<int>> GetAllowMasterUserId()
        {
            return await _permissionRepository.GetAllowMasterUserId();
        }

        public async Task<List<User>> GetAllowRolesUser()
        {
            return await _permissionRepository.GetAllowRolesUser();
        }

        public async Task<List<int>> GetAllowRolesUserId()
        {
            return await _permissionRepository.GetAllowRolesUserId();
        }

        public async Task<Role> GetRoleById(int roleId)
        {
            return await _permissionRepository.GetRoleById(roleId);
        }

        public async Task<List<Role>> GetRoles()
        {
            return await _permissionRepository.GetRoles();
        }

        public async Task<List<Role>> GetRolesJustAdmin()
        {
            return await _permissionRepository.GetRolesJustAdmin();
        }

        public Task<bool> IsExistEditRole(int roleId, string name)
        {
            return _permissionRepository.IsExistEditRoleTitle(roleId, name);
        }

        public Task<bool> IsExistRole(string name)
        {
            return _permissionRepository.IsExistRoleTitle(name);
        }

        public async Task UpdateRole(Role role)
        {
            _permissionRepository.UpdateRole(role);
            await _permissionRepository.Save();
        }

        #endregion

        #region permission
        public async Task<List<Permission>> GetAllPermission()
        {
            return await _permissionRepository.GetAllPermission();
        }

        public async Task CretePermissions(int roleId, List<int> permission)
        {
            foreach (int permissionsId in permission)
            {
                await _permissionRepository.AddRolePermission(new RolePermission
                {
                    RoleId = roleId,
                    PermissionId = permissionsId,
                });
            }
            await _permissionRepository.Save();
        }



        public async Task<List<int>> GetPermissionsIdByRoleId(int roleId)
        {
            return await _permissionRepository.GetPermissionsIdByRoleId(roleId);
        }

        public Task<int> GetPermisionIdByName(string name)
        {
            return _permissionRepository.GetPermisionIdByName(name);
        }

        public async Task UpdatePermissionsRole(int roleId, List<int> permissions)
        {
            _permissionRepository.DeleteRolePermissionByRoleId(roleId);
            await CretePermissions(roleId, permissions);
        }

        public async Task<bool> CheckPermission(string permissionId, int userId)
        {
            return await _permissionRepository.CheckPermission(permissionId, userId);
        }
        public async Task<List<string>> GetPermissionNamesByUserId(int userId)
        {
            return await _permissionRepository.GetPermissionNamesByUserId(userId);
        }

        #endregion



    }
}
