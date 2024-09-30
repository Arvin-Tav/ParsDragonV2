using Learning.Domain.Interfaces;
using Learning.Domain.Models;
using Learning.Domain.Models.Account;
using Learning.Domain.DTO.Account;
using Learning.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Learning.Domain.DTO.Paging;
using Learning.Domain.DTO.UserPanel;

namespace Learning.Infra.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        #region constructor
        private readonly LearningContext _context;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly IOnlineClassRepository _onlineClassRepository;
        public UserRepository(LearningContext context,
            IPermissionRepository permissionRepository,
            IWalletRepository walletRepository,
            IOnlineClassRepository onlineClassRepository)
        {
            _context = context;
            _permissionRepository = permissionRepository;
            _walletRepository = walletRepository;
            _onlineClassRepository = onlineClassRepository;
        }
        #endregion

        #region user

        public async Task<FilterUserDTO> FilterUsers(FilterUserDTO filter)
        {
            IQueryable<User> query = _context.Users.AsQueryable();
            #region state
            switch (filter.FilterUserType)
            {
                case FilterUserType.All:
                    break;
                case FilterUserType.Admin:
                    var allAdminUserId = await _permissionRepository.GetAllowAdminUserId();
                    query = query.Where(p => allAdminUserId.Contains(p.UserId));
                    break;
                case FilterUserType.Master:
                    var allMasterUserId = await _permissionRepository.GetAllowMasterUserId();
                    query = query.Where(p => allMasterUserId.Contains(p.UserId));
                    break;
                case FilterUserType.Student:
                    var allStudentUserId = await GetStudentUserId();
                    query = query.Where(p => allStudentUserId.Contains(p.UserId));
                    break;
            }
            switch (filter.FilterUserOrder)
            {
                case FilterUserOrder.CreateDate_DES:
                    query = query.OrderByDescending(i => i.CreateDate);
                    break;
                case FilterUserOrder.CreateDate_ASC:
                    query = query.OrderBy(i => i.CreateDate);
                    break;
            }

            switch (filter.FilterUserStatus)
            {
                case FilterUserStatus.All:
                    break;
                case FilterUserStatus.Active:
                    query = query.Where(i => i.IsActive && !i.IsDelete);
                    break;
                case FilterUserStatus.NotActive:
                    query = query.Where(i => !i.IsActive && !i.IsDelete);
                    break;
                case FilterUserStatus.IsBlocked:
                    query = query.Where(i => i.IsBlocked && !i.IsDelete);
                    break;
                case FilterUserStatus.IsDelete:
                    query = query.Where(i => i.IsDelete);
                    break;
            }
            #endregion

            #region filter
            if (!string.IsNullOrEmpty(filter.Email))
                query = query.Where(i => EF.Functions.Like(i.Email, $"%{filter.Email}%"));
            if (!string.IsNullOrEmpty(filter.UserName))
                query = query.Where(i => EF.Functions.Like(i.UserName, $"%{filter.UserName}%"));
            if (filter.CourseId != 0 && filter.CourseId != null)
            {
                List<int> listUserId = await _context.UserCourses
                .Where(u => u.CourseId == (int)filter.CourseId)
                .Select(u => u.UserId).ToListAsync();
                query = query
                  .Where(p => listUserId.Contains(p.UserId));
            }
            if (filter.OnlineClassId != 0 && filter.OnlineClassId != null)
            {
                List<int> listUserId =await _onlineClassRepository.GetUsersIdByOnlineClassId((int)filter.OnlineClassId);
                query = query
                  .Where(p => listUserId.Contains(p.UserId));
            }
            if (!await _permissionRepository.CheckPermission("Roles",filter.UserIdLogin))
            {
                var roles = await _permissionRepository.GetAllowRolesUserId();
                query = query
                 .Where(p => !roles.Contains(p.UserId));
            }
            #endregion

            #region paging

            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.HowManyShowPageAfterAndBefore);
            var allEntities = await query.Paging(pager).ToListAsync();

            #endregion
            return filter.SetPaging(pager).SetUsers(allEntities);

        }


        public async Task<int> AddUser(User user)
        {
            await _context.Users.AddAsync(user);
            return user.UserId;
        }

        public async Task<int> GetStudentCount()
        {
            return await _context.Users.AsQueryable().Where(i => !i.UserRoles.Any()).CountAsync();
        }
        public async Task<List<int>> GetStudentUserId()
        {
            return await _context.Users.AsQueryable().Where(i => !i.UserRoles.Any()).Select(i => i.UserId).ToListAsync();
        }
        public async Task<User> GetUserByActiveCode(string activeCode)
        {
            return await _context.Users.AsQueryable().FirstOrDefaultAsync(u => u.ActiveCodeEmail == activeCode);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.AsQueryable().FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetUserByMobile(string mobile)
        {
            return await _context.Users.AsQueryable().FirstOrDefaultAsync(u => u.Mobile == mobile);
        }
        public async Task<User> GetUserForLogin(string userEmailOrMobile)
        {
            return await _context.Users.AsQueryable().FirstOrDefaultAsync(u => u.Email == userEmailOrMobile || u.Mobile == userEmailOrMobile);
        }

        public async Task<User> GetUserById(int userId)
        {
            return await _context.Users.AsQueryable().Include(i => i.UserRoles).FirstOrDefaultAsync(i => i.UserId == userId);
        }

        public async Task<User> GetUserByUserName(string userName)
        {
            return await _context.Users.AsQueryable().FirstOrDefaultAsync(u => u.UserName == userName);
        }

        public async Task<bool> IsExistEditEmail(int userId, string email)
        {
            return await _context.Users.AsQueryable().IgnoreQueryFilters().AnyAsync(u => u.UserId != userId && u.Email == email);
        }
        public async Task<bool> IsExistEditUserName(int userId, string userName)
        {
            return await _context.Users.AsQueryable().IgnoreQueryFilters().AnyAsync(u => u.UserId != userId && u.UserName == userName);
        }
        public async Task<bool> IsExistEditMobile(int userId, string mobile)
        {
            return await _context.Users.AsQueryable().IgnoreQueryFilters().AnyAsync(u => u.UserId != userId && u.Mobile == mobile);
        }
        public async Task<bool> IsExistEmail(string email)
        {
            return await _context.Users.AsQueryable().IgnoreQueryFilters().AnyAsync(i => i.Email == email);
        }

        public async Task<bool> IsExistMobile(string mobile)
        {
            return await _context.Users.AsQueryable().IgnoreQueryFilters().AnyAsync(i => i.Mobile == mobile);

        }

        public async Task<bool> IsExistUserName(string userName)
        {
            return await _context.Users.AsQueryable().IgnoreQueryFilters().AnyAsync(i => i.UserName == userName);
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }


        #endregion


        #region user panel



        #endregion




    }
}
