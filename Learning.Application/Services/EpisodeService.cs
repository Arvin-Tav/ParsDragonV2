using Learning.Application.EntitiesExtensions;
using Learning.Application.Extentions;
using Learning.Application.Interfaces;
using Learning.Application.Utils;
using Learning.Domain.DTO.Course;
using Learning.Domain.DTO.CourseEpisode;
using Learning.Domain.Interfaces;
using Learning.Domain.Models.Course;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Learning.Application.Services
{
    public class EpisodeService : IEpisodeService
    {
        #region constructor
        private readonly IEpisodeRepository _episodeRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IPermissionRepository _permissionRepository;
        public EpisodeService(IEpisodeRepository episodeRepository, ICourseRepository courseRepository, IPermissionRepository permissionRepository)
        {
            _episodeRepository = episodeRepository;
            _courseRepository = courseRepository;
            _permissionRepository = permissionRepository;
        }
        #endregion
        #region episode
        public async Task AddEpisode(CourseEpisode episode)
        {
            await _episodeRepository.AddEpisode(episode);
            await _episodeRepository.Save();
        }

        public async Task<CourseEpisodeResut> CreateEpisodeWithMaster(CreateCourseEpisodeDTO newEpisode, int userId)
        {
            var course = await _courseRepository.GetCourseById(newEpisode.CourseId);
            if (course == null || course.TeacherId != userId)
            {
                return CourseEpisodeResut.NotFound;
            }
            var episode = new CourseEpisode()
            {
                CourseId = course.CourseId,
                IsFree = newEpisode.IsFree,
                EpisodeTitle = newEpisode.EpisodeTitle,
                EpisodeTime = newEpisode.EpisodeTime,
                EpisodeFileName = newEpisode.EpisodeFileName,
            };
            course.UpdateDate = DateTime.Now;
            await AddEpisode(episode);
            _courseRepository.UpdateCourse(course);
            await _courseRepository.Save();


            return CourseEpisodeResut.Success;
        }

        public async Task<bool> CheckExistFile(string fileName, int courseId)
        {
            string path = PathExtensions.CourseFilesServer + "/" + courseId + "/" + fileName;
            return System.IO.File.Exists(path);
        }

        public async Task<CourseEpisodeResut> CreateEpisode(CreateCourseEpisodeDTO episode, IFormFile episodeFile)
        {
            var course = await _courseRepository.GetCourseById(episode.CourseId);
            if (course == null)
            {
                return CourseEpisodeResut.NotFound;
            }
            if (episodeFile == null) return CourseEpisodeResut.NotFile;
            if (await CheckExistFile(episodeFile.FileName, episode.CourseId))
            {
                return CourseEpisodeResut.ExistFile;
            }
            CourseEpisode newEpisode = new CourseEpisode();
            newEpisode.EpisodeFileName = episodeFile.FileName;
            newEpisode.IsShow = true;
            newEpisode.CourseId = episode.CourseId;
            newEpisode.IsFree = episode.IsFree;
            newEpisode.EpisodeTime = episode.EpisodeTime;
            newEpisode.EpisodeTitle = episode.EpisodeTitle;

            newEpisode.EpisodeFileName = episodeFile.FileName;
            bool result = episodeFile
                .AddVideoFileToServer(newEpisode.EpisodeFileName, PathExtensions.CourseFilesServer + episode.CourseId);
            if (!result) return CourseEpisodeResut.ErrorFile;



            course.UpdateDate = DateTime.Now;
            await AddEpisode(newEpisode);
            _courseRepository.UpdateCourse(course);
            await _courseRepository.Save();
            return CourseEpisodeResut.Success;
        }

        public async Task DeleteFileCourseEpisode(string fileName, int courseId)
        {
            fileName.DeleteImage(PathExtensions.CourseFilesServer + courseId, null);
        }


        public async Task<CourseEpisodeResut> EditCourseEpisode(EditCourseEpisodeDTO episode, IFormFile episodeFile)
        {
            CourseEpisode editeEpisode = await GetEpisodeById(episode.EpisodeId);
            if (editeEpisode.CourseId != episode.CourseId) return CourseEpisodeResut.NotFound;
            if (editeEpisode != null)
            {
                episode.EpisodeFileName = episodeFile.FileName;
                bool result = episodeFile
                .AddVideoFileToServer(episode.EpisodeFileName, PathExtensions.CourseFilesServer + episode.CourseId, editeEpisode.EpisodeFileName);
                if (!result) return CourseEpisodeResut.ErrorFile;
                editeEpisode.EpisodeFileName = episodeFile.FileName;
            }

            editeEpisode.IsShow = true;
            editeEpisode.CourseId = episode.CourseId;
            editeEpisode.IsFree = episode.IsFree;
            editeEpisode.EpisodeTime = episode.EpisodeTime;
            editeEpisode.EpisodeTitle = episode.EpisodeTitle;
            await UpdateEpisode(editeEpisode);
            return CourseEpisodeResut.Success;
        }

        public async Task<CourseEpisode> GetEpisodeById(int episodeId)
        {
            return await _episodeRepository.GetEpisodeById(episodeId);
        }
        public async Task<EditCourseEpisodeDTO> GetEpisodeByIdForEdit(int episodeId)
        {
            var episode = await _episodeRepository.GetEpisodeById(episodeId);
            if (episode == null) return null;

            return new EditCourseEpisodeDTO()
            {
                CourseId = episode.CourseId,
                EpisodeFileName = episode.EpisodeFileName,
                EpisodeTitle = episode.EpisodeTitle,
                EpisodeTime = episode.EpisodeTime,
                EpisodeId = episodeId,
                IsFree = episode.IsFree,
            };
        }

        public Task<List<CourseEpisode>> GetListEpisodeCourse(int courseId)
        {
            return _episodeRepository.GetListEpisodeCourse(courseId);
        }
        
        public Task<List<CourseEpidoeForMaster>> GetCourseEpidoeForMaster(int courseId, int userId)
        {
            return _episodeRepository.GetCourseEpidoeForMaster(courseId,userId);
        }

        public async Task IsFreeCourseEpisode(int eposideId)
        {
            var episode = await GetEpisodeById(eposideId);
            episode.IsFree = !episode.IsFree;
            await UpdateEpisode(episode);
        }

        public async Task<DeleteResult> DeleteCourseEpisode(int episodeId)
        {
            var episode = await _episodeRepository.GetEpisodeById(episodeId);
            if (episode == null) return DeleteResult.NotFound;
            episode.IsDelete = true;
            _episodeRepository.UpdateEpisode(episode);
            await _episodeRepository.Save();
            return DeleteResult.Success;
        }

        public async Task IsShowCourseEpisode(int eposideId)
        {
            var episode = await GetEpisodeById(eposideId);
            episode.IsShow = !episode.IsShow;
            await UpdateEpisode(episode);
        }
        public async Task<UploadFileEpisodeResut> UploadFileEpisode(IFormFile episodeFile, string fileName, int courseId)
        {
            if (episodeFile == null) return UploadFileEpisodeResut.NotFile;
            if (await CheckExistFile(episodeFile.FileName, courseId))
            {
                return UploadFileEpisodeResut.ExistFile;
            }
            bool result = episodeFile
                 .AddVideoFileToServer(fileName, PathExtensions.CourseFilesServer + courseId);
            if(!result) return UploadFileEpisodeResut.ErrorFile;
            return UploadFileEpisodeResut.Success;
        }
        public async Task<string> GetDowmloadFile(int episodeId, int userId)
        {
            var episode = await _episodeRepository.GetEpisodeById(episodeId);

            if (episode.Course.CoursePrice == 0)
            {
                bool isUserInCourse = await _courseRepository.IsUserInCourse(userId, episode.CourseId);
                if (!isUserInCourse)
                {
                    int courseId = episode.CourseId;
                    await _courseRepository.AddUserCourse(new UserCourse()
                    {
                        UserId = userId,
                        CourseId = courseId,
                    });
                    await _courseRepository.Save();
                }
            }
            string filepath = episode.EpisodeFileName.GetCourseFileServerAddress(episode.CourseId);
            if (System.IO.File.Exists(filepath))
            {
                if (episode.IsFree)
                {
                    return filepath;
                }
                else
                {
                    bool isUserInCourse = await _courseRepository.IsUserInCourse(userId, episode.CourseId);
                    bool isMasterInCourse = await _courseRepository.IsMasterInCourse(userId, episode.CourseId);
                    bool isAdmin = await _permissionRepository.CheckPermission("AccessToCourses", userId);
                    if (isMasterInCourse || isAdmin || isUserInCourse)
                    {
                        return filepath;
                    }
                }
            }
            return null;
        }

        public async Task UpdateEpisode(CourseEpisode episode)
        {
            _episodeRepository.UpdateEpisode(episode);
            await _episodeRepository.Save();
        }
        #endregion



    }
}
