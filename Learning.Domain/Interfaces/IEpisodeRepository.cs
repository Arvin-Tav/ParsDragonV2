using Learning.Domain.DTO.CourseEpisode;
using Learning.Domain.Models.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.Interfaces
{
    public interface IEpisodeRepository
    {
        Task<List<CourseEpisode>> GetListEpisodeCourse(int courseId);
        Task<List<CourseEpidoeForMaster>> GetCourseEpidoeForMaster(int courseId,int userId);
        Task AddEpisode(CourseEpisode episode);
        Task<CourseEpisode> GetEpisodeById(int episodeId);
        void UpdateEpisode(CourseEpisode episode);
        Task Save();
    }
}
