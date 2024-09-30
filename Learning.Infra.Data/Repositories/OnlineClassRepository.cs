using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Learning.Domain.DTO.OnlineClass;
using Learning.Domain.DTO.Paging;
using Learning.Domain.Interfaces;
using Learning.Domain.Models.OnlineClass;
using Learning.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Learning.Infra.Data.Repositories
{
    public class OnlineClassRepository : IOnlineClassRepository
    {
        #region constructor
        private readonly LearningContext _context;
        public OnlineClassRepository(LearningContext context)
        {
            _context = context;
        }
        #endregion
        #region online class
        public async Task AddOnlineClass(OnlineClass onlineClass)
        {
            await _context.OnlineClasses.AddAsync(onlineClass);
        }

        public async Task AddUserOnlineClass(UserOnlineClass userOnlineClass)
        {
            await _context.UserOnlineClasses.AddAsync(userOnlineClass);
        }


        public async Task<FilterOnlineClassDTO> FilterOnlineClass(FilterOnlineClassDTO filter)
        {
            IQueryable<OnlineClass> query = _context.OnlineClasses.AsQueryable()
                .Include(u => u.User)
                .Include(u => u.UserClassOnlines);

            #region state
            switch (filter.FilterOnlineClassOrder)
            {
                case FilterOnlineClassOrder.CreateDate_DES:
                    query = query.OrderByDescending(i => i.CreateDate);
                    break;
                case FilterOnlineClassOrder.CreateDate_ASC:
                    query = query.OrderBy(i => i.CreateDate);
                    break;
            }

            switch (filter.FilterOnlineClassRead)
            {
                case FilterOnlineClassRead.Show:
                    query = query.Where(i => i.IsShow);
                    break;
                case FilterOnlineClassRead.NotShow:
                    query = query.Where(i => !i.IsShow);
                    break;
                case FilterOnlineClassRead.All:
                    break;
            }

            switch (filter.FilterOnlineClassSort)
            {
                case FilterOnlineClassSort.Date:
                    query = query.OrderByDescending(c => c.CreateDate);
                    break;
                case FilterOnlineClassSort.Title:
                    query = query.OrderBy(c => c.Title);
                    break;
                case FilterOnlineClassSort.Price:
                    query = query.OrderByDescending(c => c.Price);
                    break;
                case FilterOnlineClassSort.Popular:
                    query = query.OrderByDescending(c => c.OrderDetails.Any());
                    break;
            }


            switch (filter.FilterOnlineClassState)
            {
                case FilterOnlineClassState.All:
                    break;
                case FilterOnlineClassState.Buy:
                    query = query.Where(i => i.Price != 0);
                    break;
                case FilterOnlineClassState.Free:
                    query = query.Where(i => i.Price == 0);
                    break;
            }
            #endregion

            #region filter
            if (filter.UserId != null && filter.UserId != 0)
                query = query.Where(i => i.UserClassOnlines.Any(i => i.UserId == filter.UserId));
            if (filter.TeacherId != null && filter.TeacherId != 0)
                query = query.Where(i => i.TeacherId == filter.TeacherId);
            if (!string.IsNullOrEmpty(filter.TeacherName))
                query = query.Where(i => EF.Functions.Like(i.User.UserName, $"%{filter.TeacherName}%"));
            if (!string.IsNullOrEmpty(filter.Search))
                query = query.Where(i => EF.Functions.Like(i.Tags, $"%{filter.Search}%") ||
                 EF.Functions.Like(i.Title, $"%{filter.Search}%") ||
                 EF.Functions.Like(i.Description, $"%{filter.Search}%"));
            if (filter.StartPrice > 0 && filter.StartPrice != null)
            {
                query = query.Where(c => c.Price > filter.StartPrice);
            }

            if (filter.EndPage > 0 && filter.EndPrice != null)
            {
                query = query.Where(c => c.Price < filter.EndPage);
            }
            if (filter.SelectedGroups != null && filter.SelectedGroups.Any())
            {
                query = query.Where(i => filter.SelectedGroups.Contains(i.GroupId) ||
                filter.SelectedGroups.Contains((int)i.SubGroup));
                //TODo
                //foreach (int groupId in filter.SelectedGroups)
                //{
                //    query = query.Where(c => c.GroupId == groupId || c.SubGroup == groupId);
                //}
            }

            #endregion
            #region paging

            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.HowManyShowPageAfterAndBefore);
            var allEntities = await query.Paging(pager).ToListAsync();

            #endregion
            return filter.SetPaging(pager).SetOnlineClasses(allEntities);
        }
        public async Task<bool> IsMasterInOnlineClass(int userId, int onlineClassId)
        {
            return await _context.OnlineClasses.AnyAsync(c => c.TeacherId == userId && c.OnlineClassId == onlineClassId);
        }
        public async Task<bool> IsUserInOnlineClass(int userId, int onlineClassId)
        {
            return await _context.UserOnlineClasses.AnyAsync(c => c.UserId == userId && c.OnlineClassId == onlineClassId);
        }
        public async Task<InfoOnlineClassForUserDTO> GetInfoOnlineClassForUser(int onlineClassId)
        {
            return await _context.OnlineClasses.AsQueryable().Where(i => i.OnlineClassId == onlineClassId).Select(i => new InfoOnlineClassForUserDTO()
            {
                Link = i.Link,
            }).FirstOrDefaultAsync();
        }
        public async Task<OnlineClass> GetOnlineClassById(int onlineClassId)
        {
            return await _context.OnlineClasses
                .AsQueryable()
              .Include(u => u.User)
              .Include(u => u.OnlineClassOfDays)
              .Include(u => u.OnlineClassPrerequisites)
              .Include(u => u.OnlineClassGoals)
              .Include(u => u.OnlineClassContacts)
              .Include(u => u.OnlineClassHeadings)
              .FirstOrDefaultAsync(i => i.OnlineClassId == onlineClassId);
        }

        public async Task<List<int>> GetUsersIdByOnlineClassId(int onlineClassId)
        {
            return await _context.UserOnlineClasses.AsQueryable()
                .Where(u => u.OnlineClassId == onlineClassId).Select(u => u.UserId).ToListAsync();

        }

        public void UpdateOnlineClass(OnlineClass onlineClass)
        {
            _context.Update(onlineClass);
        }
        #endregion
        #region online class eposide
        public async Task<List<OnlineClassEpisode>> GetOnlineClassEpisodeByOnlineClassId(int onlineClassId)
        {
            return await _context.OnlineClassEpisodes.AsQueryable()
                .Where(i => i.OnlineClassId == onlineClassId).ToListAsync();
        }
        public async Task<List<OnlineClassEpisode>> ShowOnlineClassEpisodeUser(int onlineClassId)
        {
            return await _context.OnlineClassEpisodes
                .AsQueryable()
                .Where(i => i.OnlineClassId == onlineClassId).ToListAsync();
        }

        public async Task AddOnlineClassEpisode(OnlineClassEpisode episode)
        {
            await _context.OnlineClassEpisodes.AddAsync(episode);
        }

        public async Task<OnlineClassEpisode> GetOnlineClassEpisodeById(int episodeId)
        {
            return await _context.OnlineClassEpisodes
                .AsQueryable()
                .Include(i => i.OnlineClass)
                .FirstOrDefaultAsync(i => i.OnlineClassEpisodeId == episodeId);

        }

        public void UpdateOnlineClassEpisode(OnlineClassEpisode episode)
        {
            _context.OnlineClassEpisodes.Update(episode);
        }
        #endregion

        #region Contact

        public async Task<List<OnlineClassContact>> GetAllContactsByOnlineId(int onlineClassId)
        {
            return await _context.OnlineClassContacts.AsQueryable()
                .Where(x => x.OnlineClassId == onlineClassId)
                .ToListAsync();

        }

        public async Task<OnlineClassContact> GetOnlineClassContactById(int contactId)
        {
            return await _context.OnlineClassContacts.AsQueryable().FirstOrDefaultAsync(i => i.OCC_Id == contactId);

        }

        public async Task AddOnlineClassContact(OnlineClassContact contact)
        {
            await _context.OnlineClassContacts.AddAsync(contact);
        }

        public void RemoveOnlineClassContact(int onlineClassId)
        {
            _context.OnlineClassContacts.Where(i => i.OnlineClassId == onlineClassId).ToList()
                .ForEach(p => _context.OnlineClassContacts.Remove(p));
        }


        #endregion

        #region Goal

        public async Task<List<OnlineClassGoal>> GetAllGoalsByOnlineId(int onlineClassId)
        {
            return await _context.OnlineClassGoals
                .AsQueryable()
                .Where(x => x.OnlineClassId == onlineClassId)
                .ToListAsync();

        }

        public async Task<OnlineClassGoal> GetOnlineClassGoalById(int goalId)
        {
            return await _context.OnlineClassGoals.AsQueryable()
                .FirstOrDefaultAsync(i => i.OCG_Id == goalId);
        }

        public async Task AddOnlineClassGoal(OnlineClassGoal goal)
        {
            await _context.OnlineClassGoals.AddAsync(goal);
        }

        public void RemoveAllOnlineClassGoal(int onlineClassId)
        {
            _context.OnlineClassGoals.Where(i => i.OnlineClassId == onlineClassId).ToList()
                .ForEach(p => _context.OnlineClassGoals.Remove(p));
        }
        #endregion


        #region Heading


        public async Task<List<OnlineClassHeading>> GetAllHeadingsByOnlineId(int onlineClassId)
        {
            return await _context.OnlineClassHeadings.AsQueryable()
                .Where(x => x.OnlineClassId == onlineClassId).ToListAsync();

        }

        public async Task<OnlineClassHeading> GetOnlineClassHeadingById(int headingId)
        {
            return await _context.OnlineClassHeadings.AsQueryable()
                .FirstOrDefaultAsync(i => i.OCC_Id == headingId);
        }

        public async Task AddOnlineClassHeading(OnlineClassHeading heading)
        {
            await _context.OnlineClassHeadings.AddAsync(heading);
        }

        public void RemoveAllOnlineClassHeading(int onlineClassId)
        {
            _context.OnlineClassHeadings.Where(i => i.OnlineClassId == onlineClassId).ToList()
               .ForEach(p => _context.OnlineClassHeadings.Remove(p));
        }
        #endregion
        #region Prerequisite

        public async Task<List<OnlineClassPrerequisite>> GetAllPrerequisitesByOnlineId(int onlineClassId)
        {
            return await _context.OnlineClassPrerequisites.AsQueryable()
                .Where(x => x.OnlineClassId == onlineClassId).ToListAsync();
        }

        public async Task<OnlineClassPrerequisite> GetOnlineClassPrerequisiteById(int prerequisiteId)
        {
            return await _context.OnlineClassPrerequisites.AsQueryable()
                .FirstOrDefaultAsync(i => i.OCP_Id == prerequisiteId);

        }

        public async Task AddOnlineClassPrerequisite(OnlineClassPrerequisite prerequisite)
        {
            await _context.OnlineClassPrerequisites.AddAsync(prerequisite);
        }

        public void RemoveAllOnlineClassPrerequisite(int onlineClassId)
        {
            _context.OnlineClassPrerequisites.Where(i => i.OnlineClassId == onlineClassId).ToList()
                 .ForEach(p => _context.OnlineClassPrerequisites.Remove(p));
        }

        #endregion

        #region Day

        public async Task<List<OnlineClassOfDay>> GetAllDaysByOnlineId(int onlineClassId)
        {
            return await _context.OnlineClassOfDays
                .AsQueryable()
                .Where(x => x.OnlineClassId == onlineClassId)
                .ToListAsync();

        }

        public Task<OnlineClassOfDay> GetOnlineClassDayById(int dayId)
        {
            return _context.OnlineClassOfDays.AsQueryable()
                .FirstOrDefaultAsync(i => i.OCD_Id == dayId);
        }

        public async Task AddOnlineClassDay(OnlineClassOfDay day)
        {
            await _context.OnlineClassOfDays.AddAsync(day);

        }

        public void RemoveAllOnlineClassDay(int onlineClassId)
        {
            _context.OnlineClassOfDays.Where(i => i.OnlineClassId == onlineClassId).ToList()
                .ForEach(p => _context.OnlineClassOfDays.Remove(p));
        }


        #endregion

        #region save

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
        #endregion









    }
}
