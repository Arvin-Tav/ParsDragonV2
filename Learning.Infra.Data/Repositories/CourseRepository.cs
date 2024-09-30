using Learning.Domain.DTO.Course;
using Learning.Domain.DTO.Paging;
using Learning.Domain.DTO.UserPanel;
using Learning.Domain.Interfaces;
using Learning.Domain.Models.Account;
using Learning.Domain.Models.Course;
using Learning.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Infra.Data.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        #region constructor
        private readonly LearningContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IPermissionRepository _permissionRepository;
        public CourseRepository(LearningContext context,
            IUserRepository userRepository,
            IPermissionRepository permissionRepository)
        {
            _context = context;
            _userRepository = userRepository;
            _permissionRepository = permissionRepository;
        }

        #endregion
        #region group
        public async Task AddGroup(CourseGroup group)
        {
            await _context.CourseGroups.AddAsync(group);
        }

        public void DeleteGroup(CourseGroup group)
        {
            _context.CourseGroups.Remove(group);
        }


        public async Task<bool> IsExistEditUrlNameCourseGroup(int groupId, string urlName)
        {
            return await _context.CourseGroups.AsQueryable()
                .AnyAsync(i => i.GroupId != groupId && i.UrlName == urlName);
        }
        public async Task<bool> IsExistGroupSetToCourse(int groupId)
        {
            return await _context.Courses.AsQueryable()
                .AnyAsync(i => i.SubGroup == groupId || i.GroupId == groupId);
        }
        public async Task<bool> IsExistUrlNameCourseGroup(string urlName)
        {
            return await _context.CourseGroups.AsQueryable()
                .AnyAsync(i => i.UrlName == urlName);
        }
        public async Task<bool> IsExistCourseGroup(int groupId)
        {
            return await _context.CourseGroups.AsQueryable()
                .AnyAsync(i => i.GroupId == groupId);
        }
        public async Task<List<CourseGroup>> GetAllGroup()
        {
            return await _context.CourseGroups.AsQueryable()
                .Include(c => c.CourseGroups).ToListAsync();
        }

        public async Task<CourseGroup> GetGroupById(int groupId)
        {
            return await _context.CourseGroups
                .AsQueryable()
                .FirstOrDefaultAsync(i => i.GroupId == groupId);
        }

        public async Task<List<CourseGroup>> GetGroupForManageCourse()
        {
            return await _context.CourseGroups.AsQueryable()
                .Where(i => i.ParentId == null).ToListAsync();
        }

        public async Task<List<CourseLevel>> GetLevels()
        {
            return await _context.CourseLevels.AsQueryable().ToListAsync();
        }

        public async Task<List<CourseStatus>> GetStatues()
        {
            return await _context.CourseStatuses.AsQueryable().ToListAsync();
        }

        public async Task<List<CourseGroup>> GetSubGroupForManageCourse(int groupId)
        {
            return await _context.CourseGroups
                .AsQueryable()
                .Where(i => i.ParentId == groupId)
                 .ToListAsync();
        }


        public void UpdateGroup(CourseGroup group)
        {
            _context.CourseGroups.Update(group);
        }
        #endregion
        #region save
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        #endregion




        #region cource

        public async Task<FilterCourseDTO> FilterCourse(FilterCourseDTO filter)
        {
            IQueryable<Course> query = _context.Courses.AsQueryable()
                .Include(c => c.UserCourses)
                .Include(c => c.CourseEpisodes)
                .Include(u => u.User);

            #region state
            switch (filter.FilterCourseOrder)
            {
                case FilterCourseOrder.CreateDate_DES:
                    query = query.OrderByDescending(i => i.CreateDate);
                    break;
                case FilterCourseOrder.CreateDate_ASC:
                    query = query.OrderBy(i => i.CreateDate);
                    break;
            }

            switch (filter.FilterCourseRead)
            {
                case FilterCourseRead.All:
                    break;
                case FilterCourseRead.NotShow:
                    query = query.Where(i => !i.IsShow);
                    break;
                case FilterCourseRead.Show:
                    query = query.Where(i => i.IsShow);
                    break;
            }

            switch (filter.FilterCourseSort)
            {
                case FilterCourseSort.Date:
                    query = query.OrderByDescending(c => c.CreateDate);
                    break;
                case FilterCourseSort.Title:
                    query = query.OrderBy(c => c.CourseTitle);
                    break;
                case FilterCourseSort.Price:
                    query = query.OrderByDescending(c => c.CoursePrice);
                    break;
                case FilterCourseSort.Popular:
                    query = query.OrderByDescending(c => c.OrderDetails.Any());
                    break;
            }
            switch (filter.FilterCourseState)
            {
                case FilterCourseState.All:
                    break;
                case FilterCourseState.Buy:
                    query = query.Where(i => i.CoursePrice != 0);
                    break;
                case FilterCourseState.Free:
                    query = query.Where(i => i.CoursePrice == 0);
                    break;
            }
            #endregion

            #region filter
            if (filter.IsSuggested)
                query = query.Where(i => i.IsSuggested);

            if (filter.SelectedGroups != null && filter.SelectedGroups.Any())
                query = query.Where(i => filter.SelectedGroups.Contains(i.GroupId) ||
                filter.SelectedGroups.Contains((int)i.SubGroup));

            if (filter.TeacherId != 0 && filter.TeacherId != null)
                query = query.Where(i => i.TeacherId == filter.TeacherId);

            if (filter.UserId != 0 && filter.UserId != null)
                query = query.Where(i => i.UserCourses.Any(i => i.UserId == filter.UserId));

            if (filter.StartPrice != 0 && filter.StartPrice != null)
                query = query.Where(i => i.CoursePrice >= filter.StartPrice);

            if (filter.EndPrice != 0 && filter.EndPrice != null)
                query = query.Where(i => i.CoursePrice <= filter.EndPrice);
            if (!string.IsNullOrEmpty(filter.TeacherName))
                query = query.Where(i => EF.Functions.Like(i.User.UserName, $"%{filter.TeacherName}%"));
            if (!string.IsNullOrEmpty(filter.Search))
                query = query.Where(i => EF.Functions.Like(i.CourseTitle, $"%{filter.Search}%") ||
                EF.Functions.Like(i.Tags, $"%{filter.Search}%"));

            #endregion
            #region paging

            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.HowManyShowPageAfterAndBefore);
            var allEntities = await query.Paging(pager).ToListAsync();

            #endregion
            return filter.SetPaging(pager).SetCoursees(allEntities);
        }

        public async Task AddCource(Course course)
        {
            await _context.Courses.AddAsync(course);
        }

        public async Task<Course> GetCourseById(int courseId)
        {
            return await _context.Courses.AsQueryable().FirstOrDefaultAsync(i => i.CourseId == courseId);
        }

        public void UpdateCourse(Course course)
        {
            _context.Courses.Update(course);
        }

        public async Task<Course> GetCourseForShow(int courseId)
        {
            return await _context.Courses.AsQueryable().Include(c => c.CourseEpisodes).Include(g => g.CourseGroup)
                .Include(c => c.CourseStatus)
                .Include(c => c.CourseLevel)
                .Include(c => c.User)
                .Include(c => c.UserCourses)
                .FirstOrDefaultAsync(c => c.CourseId == courseId && c.IsShow);
        }

        public async Task<List<ShowCourseListItemDTO>> GetPopularCourse(int takeCount)
        {
            var query = await _context.Courses.AsQueryable()
                .Include(c => c.OrderDetails)
                 .Include(c => c.User)
                  .Include(c => c.CourseEpisodes)
                 .Where(c => c.IsShow && c.OrderDetails.Any())
                 .OrderByDescending(d => d.OrderDetails.Count)
                   .Take(takeCount).ToListAsync();

            return query.Select(c => new ShowCourseListItemDTO()
            {
                CourseId = c.CourseId,
                ImageName = c.CourseImageName,
                Price = c.CoursePrice,
                Title = c.CourseTitle,
                TeacherName = c.User.UserName,
                TotalTime = new TimeSpan(c.CourseEpisodes.Sum(e => e.EpisodeTime.Ticks))
            }).ToList();

        }

        public async Task<List<ShowCourseListItemDTO>> GetLatesCourse(int takeCount)
        {
            var query = await _context.Courses.AsQueryable()
              .Include(c => c.OrderDetails)
                 .Where(c => c.IsShow)
                 .OrderByDescending(d => d.CreateDate)
                 .Include(c => c.User)
                  .Include(c => c.CourseEpisodes)
                   .Take(takeCount).ToListAsync();

            return query.Select(c => new ShowCourseListItemDTO()
            {
                CourseId = c.CourseId,
                ImageName = c.CourseImageName,
                Price = c.CoursePrice,
                Title = c.CourseTitle,
                TeacherName = c.User.UserName,
                TotalTime = new TimeSpan(c.CourseEpisodes.Sum(e => e.EpisodeTime.Ticks))
            }).ToList();
        }

        public async Task<List<ShowCourseListItemDTO>> GetSuggestedCourse(int takeCount)
        {
            var query = await _context.Courses
                 .Where(c => c.IsSuggested && c.IsShow)
                 .Include(c => c.User)
                 .Include(c => c.CourseEpisodes)
                 .Take(takeCount).ToListAsync();

            return query.Select(c => new ShowCourseListItemDTO()
            {
                CourseId = c.CourseId,
                ImageName = c.CourseImageName,
                Price = c.CoursePrice,
                Title = c.CourseTitle,
                TeacherName = c.User.UserName,
                TotalTime = new TimeSpan(c.CourseEpisodes.Sum(e => e.EpisodeTime.Ticks))
            }).ToList();

        }

        public async Task<bool> IsFree(int courseId)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(c => c.CourseId == courseId);
            return course.CoursePrice == 0;
        }
        public async Task<bool> IsMasterInCourse(int userId, int courseId)
        {
            return await _context.Courses.AnyAsync(c => c.TeacherId == userId && c.CourseId == courseId);
        }
        public Task<List<string>> GetAllCourseTitle(string term)
        {
            return _context.Courses
                   .Where(c => c.CourseTitle.Contains(term) && c.IsShow)
                   .Select(c => c.CourseTitle)
                   .ToListAsync();
        }
        public async Task<CourseInformaationDTO> GetCourseInformaation()
        {
            CourseInformaationDTO course = new CourseInformaationDTO();
            course.UserCount = await _userRepository.GetStudentCount();
            course.EpisodeAllTime = await _context.CourseEpisodes.SumAsync(i => i.EpisodeTime.Minutes);
            var masterCount = await GetMasters();
            course.MasterCount = masterCount.Count();
            return course;
        }
        public async Task<bool> IsUserInCourse(int userId, int courseId)
        {
            return await _context.UserCourses
                .AnyAsync(c => c.UserId == userId && c.CourseId == courseId);

        }
        #endregion



        #region comment

        public async Task<FilterCourseCommentDTO> FilterCourseComment(FilterCourseCommentDTO filter)
        {
            IQueryable<CourseComment> query = _context.CourseComments.AsQueryable().Include(c => c.User);


            #region state
            switch (filter.FilterCourseCommentRead)
            {
                case FilterCourseCommentRead.NotReadAdmin:
                    query = query.Where(i => !i.IsAdminRead);
                    break;
                case FilterCourseCommentRead.ReadAdmin:
                    query = query.Where(i => i.IsAdminRead);
                    break;
                case FilterCourseCommentRead.All:
                    break;
            }
            switch (filter.FilterCourseCommentOrder)
            {
                case FilterCourseCommentOrder.CreateDate_DES:
                    query = query.OrderByDescending(i => i.CreateDate);
                    break;
                case FilterCourseCommentOrder.CreateDate_ASC:
                    query = query.OrderBy(i => i.CreateDate);
                    break;
            }
            #endregion

            #region filter
            if (!string.IsNullOrEmpty(filter.Serach))
                query = query.Where(i => EF.Functions.Like(i.Comment, $"%{filter.Serach}%"));
            if (!string.IsNullOrEmpty(filter.UserName))
                query = query.Where(i => EF.Functions.Like(i.User.UserName, $"%{filter.UserName}%") ||
                EF.Functions.Like(i.User.Email, $"%{filter.UserName}%") ||
                EF.Functions.Like(i.Course.CourseTitle, $"%{filter.UserName}%"));

            if (filter.UserId != null && filter.UserId != 0)
                query = query.Where(i => i.UserId == filter.UserId);

            if (filter.CourseId != null && filter.CourseId != 0)
                query = query.Where(i => i.CourseId == filter.CourseId);
            #endregion


            #region paging

            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.HowManyShowPageAfterAndBefore);
            var allEntities = await query.Paging(pager).ToListAsync();

            #endregion
            return filter.SetPaging(pager).SetCourseComment(allEntities);
        }

        public async Task<CourseComment> GetCommentById(int commentId)
        {
            return await _context.CourseComments.AsQueryable().FirstOrDefaultAsync(i => i.CommentId == commentId);

        }

        public async Task<CourseComment> ShowCommentById(int commentId)
        {
            return await _context.CourseComments
                .Include(u => u.User).Include(c => c.Course)
                .AsQueryable()
                .FirstOrDefaultAsync(i => i.CommentId == commentId);
        }

        public async Task AddComment(CourseComment comment)
        {
            await _context.CourseComments.AddAsync(comment);
        }

        public void UpdateComment(CourseComment comment)
        {
            _context.CourseComments.Update(comment);
        }

        #endregion

        #region user course

        public async Task<List<int>> GetUsersIdByCourseId(int courseId)
        {
            return await _context.UserCourses
                .Where(u => u.CourseId == courseId)
                .Select(u => u.UserId).ToListAsync();
        }

        public async Task AddUserCourse(UserCourse userCourse)
        {
            await _context.UserCourses.AddAsync(userCourse);
        }

        public async Task<List<ShowCoursesUserDTO>> ShowCoursesUser(int userId)
        {
            return await _context.UserCourses.Where(i => i.UserId == userId).Select(p => new ShowCoursesUserDTO()
            {
                CoursePrice = p.Course.CoursePrice,
                Title = p.Course.CourseTitle,
                CourseId = p.CourseId,
            }).ToListAsync();
        }


        #endregion

        #region course vote
        public async Task AddVote(CourseVote courseVote)
        {
            await _context.CourseVotes.AddAsync(courseVote);
        }
        public void UpdateVote(CourseVote courseVote)
        {
            _context.CourseVotes.Update(courseVote);
        }
        public async Task<CourseVote> GetCourseVote(int userId, int courseId)
        {
            return await _context.CourseVotes
                .FirstOrDefaultAsync(c => c.UserId == userId &&
                c.CourseId == courseId);
        }

        public async Task<Tuple<int, int>> GetCourseVotes(int courseId)
        {
            var votes = await _context.CourseVotes.Where(v => v.CourseId == courseId)
                 .Select(v => v.Vote).ToListAsync();

            return Tuple.Create(votes.Count(c => c), votes.Count(c => !c));
        }

        #endregion

       
        #region cours master

        public async Task<List<Course>> GetAllMasterCourses(int userId)
        {
            return await _context.Courses
                .Include(u => u.User)
                .Include(u => u.CourseStatus)
                .Include(u => u.CourseEpisodes)
                .Include(u => u.UserCourses)
                .Where(i => i.TeacherId == userId).ToListAsync();
        }
        public async Task<List<User>> GetMasters()
        {
            int permissionId = await _permissionRepository.GetPermisionIdByName("Master");
            List<int> rolesId = await _context.RolePermission
                .Where(i => i.PermissionId == permissionId).Select(i => i.RoleId)
                .ToListAsync();
            List<User> masterList = await _context.UserRoles
                .Where(i => rolesId.Contains(i.RoleId)).Select(u => u.User).ToListAsync();
            return masterList;
        }
        #endregion

    }
}
