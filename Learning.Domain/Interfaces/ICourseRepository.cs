using Learning.Domain.DTO.Course;
using Learning.Domain.DTO.UserPanel;
using Learning.Domain.Models.Account;
using Learning.Domain.Models.Course;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learning.Domain.Interfaces
{
    public interface ICourseRepository
    {
        #region group
        Task<bool> IsExistEditUrlNameCourseGroup(int groupId, string urlName);
        Task<bool> IsExistUrlNameCourseGroup(string urlName);
        Task<bool> IsExistGroupSetToCourse(int groupId);
        Task<bool> IsExistCourseGroup(int groupId);
        Task<List<CourseGroup>> GetAllGroup();
        Task<List<CourseGroup>> GetGroupForManageCourse();
        Task<List<CourseGroup>> GetSubGroupForManageCourse(int groupId);
        Task<List<CourseLevel>> GetLevels();
        Task<List<CourseStatus>> GetStatues();
        Task AddGroup(CourseGroup group);

        void UpdateGroup(CourseGroup group);
        void DeleteGroup(CourseGroup group);
        Task<CourseGroup> GetGroupById(int groupId);
        #endregion

        #region cource
        Task<FilterCourseDTO> FilterCourse(FilterCourseDTO filter);
        Task AddCource(Course course);
        Task<Course> GetCourseById(int courseId);
        void UpdateCourse(Course course);
        Task<Course> GetCourseForShow(int courseId);
        Task<List<ShowCourseListItemDTO>> GetPopularCourse(int takeCount);
        Task<List<ShowCourseListItemDTO>> GetLatesCourse(int takeCount);

        Task<List<ShowCourseListItemDTO>> GetSuggestedCourse(int takeCount);
        Task<bool> IsFree(int courseId);
        Task<List<string>> GetAllCourseTitle(string term);
        Task<CourseInformaationDTO> GetCourseInformaation();


        #endregion


        #region comment
        Task<FilterCourseCommentDTO> FilterCourseComment(FilterCourseCommentDTO filter);
        Task<CourseComment> GetCommentById(int commentId);
        Task<CourseComment> ShowCommentById(int commentId);
        Task AddComment(CourseComment comment);
        void UpdateComment(CourseComment comment);
        #endregion



        #region user course
        Task<List<int>> GetUsersIdByCourseId(int courseId);

        Task AddUserCourse(UserCourse userCourse);
        Task<List<ShowCoursesUserDTO>> ShowCoursesUser(int userId);
        #endregion
        #region course vote
        void UpdateVote(CourseVote courseVote);
        Task AddVote(CourseVote courseVote);
        Task<CourseVote> GetCourseVote(int userId, int courseId);
        Task<Tuple<int, int>> GetCourseVotes(int courseId);
        Task<bool> IsUserInCourse(int userId, int courseId);

        #endregion

        #region cours master
        Task<List<User>> GetMasters();
        Task<List<Course>> GetAllMasterCourses(int userId);
        Task<bool> IsMasterInCourse(int userId, int courseId);

        #endregion




        Task Save();

      
    }
}
