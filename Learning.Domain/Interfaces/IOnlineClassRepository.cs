using Learning.Domain.DTO.OnlineClass;
using Learning.Domain.Models.OnlineClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.Interfaces
{
    public interface IOnlineClassRepository
    {
        #region online class
        Task<FilterOnlineClassDTO> FilterOnlineClass(FilterOnlineClassDTO filter);
        Task AddUserOnlineClass(UserOnlineClass userOnlineClass);
        Task<List<int>> GetUsersIdByOnlineClassId(int onlineClassId);
        Task AddOnlineClass(OnlineClass onlineClass);
        Task<OnlineClass> GetOnlineClassById(int onlineClassId);
        void UpdateOnlineClass(OnlineClass onlineClass);
        Task<bool> IsMasterInOnlineClass(int userId, int onlineClassId);
        Task<bool> IsUserInOnlineClass(int userId, int onlineClassId);
        Task<InfoOnlineClassForUserDTO> GetInfoOnlineClassForUser(int onlineClassId);

        #endregion


        #region online class eposide
        Task<List<OnlineClassEpisode>> ShowOnlineClassEpisodeUser(int onlineClassId);
        Task<List<OnlineClassEpisode>> GetOnlineClassEpisodeByOnlineClassId(int onlineClassId);
        Task AddOnlineClassEpisode(OnlineClassEpisode episode);
        Task<OnlineClassEpisode> GetOnlineClassEpisodeById(int episodeId);
        void UpdateOnlineClassEpisode(OnlineClassEpisode episode);
        #endregion

        #region Contact
        Task<List<OnlineClassContact>> GetAllContactsByOnlineId(int onlineClassId);
        Task<OnlineClassContact> GetOnlineClassContactById(int contactId);
        Task AddOnlineClassContact(OnlineClassContact contact);
        void RemoveOnlineClassContact(int onlineClassId);
        #endregion



        #region Goal
        Task<List<OnlineClassGoal>> GetAllGoalsByOnlineId(int onlineClassId);
        Task<OnlineClassGoal> GetOnlineClassGoalById(int goalId);
        Task AddOnlineClassGoal(OnlineClassGoal goal);
        void RemoveAllOnlineClassGoal(int onlineClassId);

        #endregion


        #region Heading
        Task<List<OnlineClassHeading>> GetAllHeadingsByOnlineId(int onlineClassId);
        Task<OnlineClassHeading> GetOnlineClassHeadingById(int headingId);
        Task AddOnlineClassHeading(OnlineClassHeading heading);
        void RemoveAllOnlineClassHeading(int onlineClassId);
        #endregion


        #region Prerequisite
        Task<List<OnlineClassPrerequisite>> GetAllPrerequisitesByOnlineId(int onlineClassId);
        Task<OnlineClassPrerequisite> GetOnlineClassPrerequisiteById(int prerequisiteId);
        Task AddOnlineClassPrerequisite(OnlineClassPrerequisite prerequisite);
        void RemoveAllOnlineClassPrerequisite(int onlineClassId);
        #endregion



        #region Day
        Task<List<OnlineClassOfDay>> GetAllDaysByOnlineId(int onlineClassId);
        Task<OnlineClassOfDay> GetOnlineClassDayById(int dayId);
        Task AddOnlineClassDay(OnlineClassOfDay day);
        void RemoveAllOnlineClassDay(int onlineClassId);
        #endregion


        #region save
        Task Save();
        #endregion
    }
}
