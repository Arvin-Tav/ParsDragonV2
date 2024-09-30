using Learning.Domain.DTO.CourseEpisode;
using Learning.Domain.Interfaces;
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
    public class EpisodeRepository : IEpisodeRepository
    {
        #region constructor
        private readonly LearningContext _context;
        public EpisodeRepository(LearningContext context)
        {
            _context = context;
        }

        #endregion

        #region course episoide
        public async Task AddEpisode(CourseEpisode episode)
        {
            await _context.CourseEpisodes.AddAsync(episode);
        }
        public async Task<CourseEpisode> GetEpisodeById(int episodeId)
        {
            return await _context.CourseEpisodes.Include(i => i.Course).AsQueryable().FirstOrDefaultAsync(i => i.EpisodeId == episodeId);
        }

        public async Task<List<CourseEpisode>> GetListEpisodeCourse(int courseId)
        {
            return await _context.CourseEpisodes
                .AsQueryable().Where(i => i.CourseId == courseId).ToListAsync();
        }

        public async Task<List<CourseEpidoeForMaster>> GetCourseEpidoeForMaster(int courseId, int userId)
        {
            var query =await _context.CourseEpisodes
                .AsQueryable().Where(i => i.CourseId == courseId && i.Course.TeacherId == userId).ToListAsync();
            return query
                .Select(i => new CourseEpidoeForMaster()
                {
                    EpisodeId = i.CourseId,
                    EpisodeTime = i.EpisodeTime,
                    EpisodeTitle = i.EpisodeTitle,
                    IsFree = i.IsFree
                }).ToList();
        }

        public void UpdateEpisode(CourseEpisode episode)
        {
            _context.CourseEpisodes.Update(episode);
        }

        #endregion



        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
