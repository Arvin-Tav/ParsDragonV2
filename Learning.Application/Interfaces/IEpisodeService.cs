using Learning.Domain.DTO.Course;
using Learning.Domain.DTO.CourseEpisode;
using Learning.Domain.Models.Course;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Application.Interfaces
{
    public interface IEpisodeService
    {
        Task<List<CourseEpisode>> GetListEpisodeCourse(int courseId);
        Task<List<CourseEpidoeForMaster>> GetCourseEpidoeForMaster(int courseId, int userId);
        Task AddEpisode(CourseEpisode episode);
        Task<CourseEpisodeResut> CreateEpisode(CreateCourseEpisodeDTO episode, IFormFile episodeFile);
        Task<CourseEpisodeResut> CreateEpisodeWithMaster(CreateCourseEpisodeDTO episodeViewModel, int userId);
        Task<bool> CheckExistFile(string fileName, int courseId);
        Task DeleteFileCourseEpisode(string fileName, int courseId);
        Task<CourseEpisode> GetEpisodeById(int episodeId);
        Task<EditCourseEpisodeDTO> GetEpisodeByIdForEdit(int episodeId);
        Task<CourseEpisodeResut> EditCourseEpisode(EditCourseEpisodeDTO episode, IFormFile episodeFile);
        Task UpdateEpisode(CourseEpisode episode);
        Task IsShowCourseEpisode(int eposideId);
        Task IsFreeCourseEpisode(int eposideId);
        Task<DeleteResult> DeleteCourseEpisode(int episodeId);
        Task<string> GetDowmloadFile(int episodeId,int userId);
        Task<UploadFileEpisodeResut> UploadFileEpisode(IFormFile episodeFile, string fileName, int courseId);
    }
}
