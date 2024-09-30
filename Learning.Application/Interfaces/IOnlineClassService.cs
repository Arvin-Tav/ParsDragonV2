using Learning.Domain.DTO.OnlineClass;
using Learning.Domain.Models.OnlineClass;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Application.Interfaces
{
    public interface IOnlineClassService
    {
        #region online class
        Task<FilterOnlineClassDTO> FilterOnlineClass(FilterOnlineClassDTO filter);
        Task AddUserOnlineClass(UserOnlineClass userOnlineClass);
        Task<List<int>> GetUsersIdByOnlineClassId(int onlineClassId);
        Task<OnlineClassResult> CreateOnlineClass(CreateOnlineClassDTO onlineClass,IFormFile imageName);
        Task<OnlineClass> GetOnlineClassById(int onlineClassId);
        Task<InfoOnlineClassForUserDTO> GetInfoOnlineClassForUser(int onlineClassId);
        Task<ShowOnlineClassEpisodeDTO> ShowOnlineClassEpisode(int id, int episodeId = 0);
        Task<List<OnlineClassEpisode>> ShowOnlineClassEpisodeUser(int onlineClassId);

        Task<EditOnlineClassDTO> GetOnlineClassByIdForEdit(int onlineClassId);

        Task AddOnlineClass(OnlineClass onlineClass);
        Task UpdateOnlineClass(OnlineClass onlineClass);
        Task<OnlineClassResult> UpdateByOnlineClass(EditOnlineClassDTO onlineClass, IFormFile imageName);
        Task IsShowOnlineClass(int onlineClassId);
        Task<List<OnlineClassEpisode>> GetOnlineClassEpisodeByOnlineClassId(int onlineClassId);
        Task<bool> CheckExistFile(string fileName, int onlineClassId);
        Task AddOnlineClassEpisode(OnlineClassEpisode episode);
        Task<OnlineClassEpisodeResult> CreateOnlineClassEpisode(CreateOnlineClassEpisodeDTO episode, IFormFile episodeFile);
        Task<OnlineClassEpisode> GetOnlineClassEpisodeById(int episodeId);
        Task<EditOnlineClassEpisodeDTO> GetOnlineClassEpisodeForEdit(int episodeId);
        Task<OnlineClassEpisodeResult> EditOnlineClassEpisode(EditOnlineClassEpisodeDTO episode, IFormFile episodeFile);
        Task DeleteFileOnlineClassEpisode(string fileName, int onlioneClassId);
        Task UpdateOnlineClassEpisode(OnlineClassEpisode episode);
        Task IsShowOnlineClassEpisode(int eposideId);
        #endregion




        Task<bool> IsMasterInOnlineClass(int userId, int onlineClassId);
        Task<bool> IsUserInOnlineClass(int userId, int onlineClassId);

        #region Contact
        Task<List<OnlineClassContact>> GetAllContactsByOnlineId(int onlineClassId);
        Task<OnlineClassContact> GetOnlineClassContactById(int contactId);
        Task<OnlineClassInfoResult> AddOnlineClassContactsByList(int onlineClassId, List<string> contactList);
        #endregion

        #region Goal
        Task<List<OnlineClassGoal>> GetAllGoalsByOnlineId(int onlineClassId);
        Task<OnlineClassGoal> GetOnlineClassGoalById(int goalId);
        Task<OnlineClassInfoResult> AddOnlineClassGoalsByList(int onlineClassId, List<string> goalList);
        Task AddOnlineClassGoal(OnlineClassGoal goal);
         Task RemoveAllOnlineClassGoal(int onlineClassId);
        #endregion

        #region Heading
        Task<List<OnlineClassHeading>> GetAllHeadingsByOnlineId(int onlineClassId);
        Task<OnlineClassHeading> GetOnlineClassHeadingById(int headingId);
        Task<OnlineClassInfoResult> AddOnlineClassHeadingsByList(int onlineClassId, List<string> headingList);
        Task AddOnlineClassHeading(OnlineClassHeading heading);
        Task RemoveAllOnlineClassHeading(int onlineClassId);
        #endregion

        #region Prerequisite
        Task<List<OnlineClassPrerequisite>> GetAllPrerequisitesByOnlineId(int onlineClassId);
        Task<OnlineClassPrerequisite> GetOnlineClassPrerequisiteById(int prerequisiteId);
        Task<OnlineClassInfoResult> AddOnlineClassPrerequisitesByList(int onlineClassId, List<string> prerequisiteList);
        Task AddOnlineClassPrerequisite(OnlineClassPrerequisite prerequisite);
        Task RemoveAllOnlineClassPrerequisite(int onlineClassId);
        #endregion


        #region Day
        Task<List<OnlineClassOfDay>> GetAllDaysByOnlineId(int onlineClassId);
        Task<OnlineClassOfDay> GetOnlineClassDayById(int dayId);
        Task<OnlineClassInfoResult> AddOnlineClassDaysByList(int onlineClassId, List<string> day, List<string> startTime, List<string> endTime);
        Task AddOnlineClassDay(OnlineClassOfDay day);
        Task RemoveAllOnlineClassDay(int onlineClassId);
        #endregion
    }
}
