using Learning.Domain.DTO.Course;
using Learning.Domain.DTO.Master;
using Learning.Domain.DTO.Paging;
using Learning.Domain.DTO.Permission;
using Learning.Domain.Interfaces;
using Learning.Domain.Models.Account;
using Learning.Domain.Models.Course;
using Learning.Domain.Models.Permissions;
using Learning.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learning.Infra.Data.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        #region constructor
        private readonly LearningContext _context;
        public PermissionRepository(LearningContext context)
        {
            _context = context;
        }
        #endregion
        #region role
        public async Task<int> AddRole(Role role)
        {
            await _context.Roles.AddAsync(role);
            return role.RoleId;
        }

        public async Task AddUserRole(UserRole userRole)
        {
            await _context.UserRoles.AddAsync(userRole);
        }

        public void UpdateRole(Role role)
        {
            _context.Roles.Update(role);
        }



        public async Task<List<int>> GetRoleIdByUserId(int userId)
        {
            return await _context.UserRoles.Where(r => r.UserId == userId)
                 .Select(i => i.RoleId).ToListAsync();
        }
        public void DeleteUserRoleByUserId(int userId)
        {
            _context.UserRoles.Where(r => r.UserId == userId).ToList()
                .ForEach(r => _context.UserRoles.Remove(r));
        }

        public async Task<IQueryable<User>> GetAllowMasterUser()
        {
            int permissionId = await GetPermisionIdByName("Master");
            IQueryable<int> rolesId = _context.RolePermission.AsQueryable()
                .Where(i => i.PermissionId == permissionId).Select(i => i.RoleId);
            IQueryable<User> masterList = _context.UserRoles.AsQueryable()
                .Where(i => rolesId.Contains(i.RoleId)).Select(u => u.User);
            return masterList.AsQueryable();
        }
        public async Task<IQueryable<User>> GetAllowAdminUser()
        {
            int permissionId = await GetPermisionIdByName("Admin");
            IQueryable<int> rolesId = _context.RolePermission.AsQueryable()
                .Where(i => i.PermissionId == permissionId).Select(i => i.RoleId);
            IQueryable<User> masterList = _context.UserRoles.AsQueryable()
                .Where(i => rolesId.Contains(i.RoleId)).Select(u => u.User);
            return masterList.AsQueryable();
        }
        public async Task<List<int>> GetAllowMasterUserId()
        {
            var master = await GetAllowMasterUser();
            return master.Select(u => u.UserId).ToList();
        }
        public async Task<List<int>> GetAllowAdminUserId()
        {
            var admin = await GetAllowAdminUser();
            return admin.Select(u => u.UserId).ToList();
        }

        public async Task<List<User>> GetAllowRolesUser()
        {
            int permissionId = await GetPermisionIdByName("Roles");
            IQueryable<int> rolesId = _context.RolePermission.AsQueryable()
                .Where(i => i.PermissionId == permissionId).Select(i => i.RoleId);
            IQueryable<User> masterList = _context.UserRoles.AsQueryable()
                .Where(i => rolesId.Contains(i.RoleId)).Select(u => u.User);
            return masterList.ToList();
        }

        public async Task<List<int>> GetAllowRolesUserId()
        {
            var master = await GetAllowRolesUser();
            return master.Select(u => u.UserId).ToList();
        }

        public async Task<Role> GetRoleById(int roleId)
        {
            var role = await _context.Roles.AsQueryable().FirstOrDefaultAsync(i => i.RoleId == roleId);
            return role;
        }

        public async Task<List<Role>> GetRoles()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<List<Role>> GetRolesJustAdmin()
        {
            return await _context.Roles.Where(i => i.RoleTitle != "admin").ToListAsync();
        }
        public async Task<IQueryable<MastersDTO>> GetInformationMaster()
        {
            var masters = await GetAllowMasterUser();
            return masters.Select(u => new MastersDTO
            {
                Image = u.UserAvatar,
                Name = !string.IsNullOrEmpty(u.FirstName) ? u.FirstName + " " + u.LastName : "",
                UserName = u.UserName,
            });
        }

        public async Task<InfoMasterDTO> GetInfoMaster(string userName)
        {
            User user = await _context.Users.AsQueryable().FirstOrDefaultAsync(i => i.UserName == userName);
            if (user == null)
            {
                return null;
            }
            var result = await _context.Courses.AsQueryable()
                .Include(u => u.User).Include(c => c.CourseEpisodes).Where(i => i.TeacherId == user.UserId && i.IsShow).ToListAsync();
            var query = result
                           .Select(c => new ShowCourseListItemDTO()
                           {
                               CourseId = c.CourseId,
                               ImageName = c.CourseImageName,
                               Price = c.CoursePrice,
                               Title = c.CourseTitle,
                               TeacherName = c.User.UserName,
                               TotalTime = new TimeSpan(c.CourseEpisodes.Sum(e => e.EpisodeTime.Ticks))
                           }).ToList();

            return new InfoMasterDTO
            {
                Courses = query,
                Image = user.UserAvatar,
                Name = !string.IsNullOrEmpty(user.FirstName) ? user.FirstName + " " + user.LastName : "",
                UserName = user.UserName,
                AboutMe = user.AboutMe
            };
        }

        public async Task<FilterMasterDTO> FilterMaster(FilterMasterDTO filter)
        {
            var query = await GetInformationMaster();

            if (!string.IsNullOrEmpty(filter.Search))
            {
                query = query.Where(i => i.Name.Contains(filter.Search) || i.UserName.Contains(filter.Search));
            }
            #region paging

            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.HowManyShowPageAfterAndBefore);
            var allEntities = await query.Paging(pager).ToListAsync();

            #endregion
            return filter.SetPaging(pager).SetMasters(allEntities);
        }

        public async Task<bool> IsExistEditRoleTitle(int roleId, string name)
        {
            return await _context.Roles.AnyAsync(u => u.RoleId != roleId && u.RoleTitle == name);
        }

        public async Task<bool> IsExistRoleTitle(string name)
        {
            return await _context.Roles.AnyAsync(u => u.RoleTitle == name);
        }

        #endregion
        #region permission


        public async Task<List<Permission>> GetAllPermission()
        {
            return await _context.Permission.ToListAsync();

        }

        public async Task AddRolePermission(RolePermission rolePermission)
        {
            await _context.RolePermission.AddAsync(rolePermission);
        }

        public async Task<List<int>> GetPermissionsIdByRoleId(int roleId)
        {
            return await _context.RolePermission
                .Where(r => r.RoleId == roleId)
                .Select(r => r.PermissionId).ToListAsync();
        }

        public async Task<int> GetPermisionIdByName(string name)
        {
            var permisionName = await _context.Permission.FirstOrDefaultAsync(i => i.PermissionName == name);
            return permisionName == null ? 0 : permisionName.PermissionId;
        }

        public void DeleteRolePermissionByRoleId(int roleId)
        {
            _context.RolePermission.Where(i => i.RoleId == roleId).ToList().ForEach(p => _context.RolePermission.Remove(p));
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<List<string>> GetPermissionNamesByUserId(int userId)
        {
            IQueryable<int> userRoles = _context.UserRoles.AsQueryable()
               .Where(r => r.UserId == userId).Select(r => r.RoleId);
            List<string> permissions = new List<string>();
            foreach (var item in userRoles)
            {
                var permissionsName = await _context.RolePermission.AsQueryable()
                .Where(i => i.RoleId == item).
                Select(p => p.Permission.PermissionName).ToListAsync();
                permissions.AddRange(permissionsName);
            }
            return permissions.ToList();
        }


        public async Task<bool> CheckPermission(string permissionId, int userId)
        {
            IQueryable<int> userRoles = _context.UserRoles.AsQueryable()
                .Where(r => r.UserId == userId).Select(r => r.RoleId);
            IQueryable<int> rolesPermission = _context.RolePermission.AsQueryable()
                .Where(i => i.Permission.PermissionName == permissionId).
                Select(p => p.RoleId);
            return rolesPermission.Any(p => userRoles.Contains(p));
        }






        #endregion







    }
}
