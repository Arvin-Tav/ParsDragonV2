using Learning.Domain.DTO.Course;
using Learning.Domain.DTO.CourseEpisode;
using Learning.Domain.Models.Account;
using Learning.Domain.Models.Course;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Application.Interfaces
{
    public interface ICourseService
    {
        #region group
        Task<List<CourseGroup>> GetAllGroup();
        Task<DeleteCourseGroupResult> DeleteCourseGroup(int groupId);
        Task<List<SelectListItem>> GetGroupForManageCourse();
        Task<List<SelectListItem>> GetSubGroupForManageCourse(int groupId);
        Task<List<SelectListItem>> GetTeachers();
        Task<List<SelectListItem>> GetLevels();
        Task<List<SelectListItem>> GetStatues();
        Task<CourseGroupResult> CreateGroup(CreateCourseGroupDTO group);
        Task<CourseGroupResult> UpdateGroup(EditCourseGroupDTO group);
        Task<CourseGroup> GetGroupById(int groupId);
        Task<EditCourseGroupDTO> GetGroupForEdit(int groupId);
        
        #endregion


        #region cource
        Task<CourseResut> CreateCourse(CreateCourseDTO course, IFormFile imgCourse, IFormFile courseDemo);
        Task<FilterCourseDTO> FilterCourse(FilterCourseDTO filter);
        Task<Course> GetCourseById(int courseId);
        Task<EditCourseDTO> GetCourseForEdit(int courseId);
        Task<CourseResut> EditCourse(EditCourseDTO course, IFormFile imgCourse, IFormFile courseDemo);
        Task AddCourse(Course course);
        Task UpdateCourse(Course course);
       

        Task<Course> GetCourseForShow(int courseId);
        Task<ShowCourseEpisodeDTO> GetCourseEpisodeForShow(int courseId,int episodeId,int userId);
        Task<bool> IsUserInCourse(int userId, int courseId);
        Task<List<ShowCourseListItemDTO>> GetPopularCourse(int takeCount=8);
        Task<List<ShowCourseListItemDTO>> GetLatesCourse(int takeCount=8);
        
        Task<List<ShowCourseListItemDTO>> GetSuggestedCourse(int takeCount = 8);
        Task IsSuggestedCourse(int courseId);
        Task IsShowCourse(int courseId);
        Task<bool> IsFree(int courseId);
        Task<List<string>> GetAllCourseTitle(string term);
        Task<CourseInformaationDTO> GetCourseInformaation();
        Task<DeleteResult> DeleteCourse(int courseId);
        #endregion
        #region course master
        Task<bool> IsMasterInCourse(int userId, int courseId);
        Task<List<Course>> GetAllMasterCourses(int userId);

        #endregion
        #region user course
        // Task<List<int>> GetUsersIdByCourseId(int courseId);

        // Task AddUserCourse(UserCourse userCourse);
        Task<List<ShowCoursesUserDTO>> ShowCoursesUser(int userId);
        #endregion
        #region Comment
        Task<FilterCourseCommentDTO> FilterCourseComment(FilterCourseCommentDTO filter);
        Task<CourseComment> GetCommentById(int commentId);
        Task<CourseComment> ShowCommentById(int commentId);
        Task AddComment(CourseComment comment);
        Task<DeleteCommentResult> DeleteComment(int commentId);
        Task IsAdminReadComment(int commentId);
        #endregion


        #region course vote
        Task AddVote(int userId, int courseId, bool vote);
        Task<Tuple<int, int>> GetCourseVotes(int courseId);

        #endregion

        #region download

        #endregion
    }
}
