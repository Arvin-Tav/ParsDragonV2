using Learning.Application.EntitiesExtensions;
using Learning.Application.Extentions;
using Learning.Application.Interfaces;
using Learning.Application.Utils;
using Learning.Domain.DTO.Course;
using Learning.Domain.DTO.CourseEpisode;
using Learning.Domain.Interfaces;
using Learning.Domain.Models.Account;
using Learning.Domain.Models.Course;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using SharpCompress.Archives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Learning.Application.Services
{
    public class CourseService : ICourseService
    {
        #region constructor
        private readonly ICourseRepository _courseRepository;
        private readonly IMemoryCache _memoryCache;
        private readonly IPermissionRepository _permissionRepository;
        public CourseService(ICourseRepository courseRepository, IMemoryCache memoryCache, IPermissionRepository permissionRepository)
        {
            _courseRepository = courseRepository;
            _memoryCache = memoryCache;
            _permissionRepository = permissionRepository;
        }
        #endregion
        #region group
        public async Task<CourseGroupResult> CreateGroup(CreateCourseGroupDTO group)
        {
            if (await _courseRepository.IsExistUrlNameCourseGroup(group.UrlName.FixTextBetween())) return CourseGroupResult.ErrorUrlName;
            if (group.ParentId != null && !await _courseRepository.IsExistCourseGroup((int)group.ParentId))
                return CourseGroupResult.NotFound;
            var newGroup = new CourseGroup()
            {
                GroupTitle = group.GroupTitle,
                UrlName = group.UrlName.FixTextBetween(),
                ParentId = group.ParentId,
            };
            await _courseRepository.AddGroup(newGroup);
            await _courseRepository.Save();
            return CourseGroupResult.Success;
        }



        public async Task<List<CourseGroup>> GetAllGroup()
        {
            return await _courseRepository.GetAllGroup();
        }

        public async Task<DeleteCourseGroupResult> DeleteCourseGroup(int groupId)
        {
            var group = await _courseRepository.GetGroupById(groupId);
            if (group == null) return DeleteCourseGroupResult.NotFound;
            if (await _courseRepository.IsExistGroupSetToCourse(groupId))
                return DeleteCourseGroupResult.SetCourse;
            group.IsDelete = true;
            group.UpdateDate = DateTime.Now;
            _courseRepository.UpdateGroup(group);
            await _courseRepository.Save();
            return DeleteCourseGroupResult.Success;
        }

        public async Task<CourseGroup> GetGroupById(int groupId)
        {
            return await _courseRepository.GetGroupById(groupId);
        }
        public async Task<EditCourseGroupDTO> GetGroupForEdit(int groupId)
        {
            var group = await _courseRepository.GetGroupById(groupId);
            if (group == null) return null;
            return new EditCourseGroupDTO
            {
                GroupId = group.GroupId,
                GroupTitle = group.GroupTitle,
                ParentId = group.ParentId,
                UrlName = group.UrlName,
            };
        }

        public async Task<List<SelectListItem>> GetGroupForManageCourse()
        {
            var query = await _courseRepository.GetGroupForManageCourse();
            return query.Select(g => new SelectListItem()
            {
                Text = g.GroupTitle,
                Value = g.GroupId.ToString(),
            }).ToList();
        }

        public async Task<List<SelectListItem>> GetLevels()
        {
            var query = await _courseRepository.GetLevels();
            return query.Select(s => new SelectListItem()
            {
                Value = s.LevelId.ToString(),
                Text = s.LevelTitle,
            }).ToList();
        }

        public async Task<List<SelectListItem>> GetStatues()
        {
            var query = await _courseRepository.GetStatues();
            return query.Select(s => new SelectListItem()
            {
                Value = s.StatusId.ToString(),
                Text = s.StatusTitle,
            }).ToList();
        }

        public async Task<List<SelectListItem>> GetSubGroupForManageCourse(int groupId)
        {
            var query = await _courseRepository.GetSubGroupForManageCourse(groupId);
            return query.Select(g => new SelectListItem()
            {
                Text = g.GroupTitle,
                Value = g.GroupId.ToString(),
            }).ToList();
        }

        public async Task<List<SelectListItem>> GetTeachers()
        {
            var query = await _courseRepository.GetMasters();
            return query.Select(u => new SelectListItem()
            {
                Value = u.UserId.ToString(),
                Text = u.UserName
            }).ToList();
        }

        public async Task<CourseGroupResult> UpdateGroup(EditCourseGroupDTO group)
        {
            var editGroup = await _courseRepository.GetGroupById(group.GroupId);
            if (editGroup == null) return CourseGroupResult.NotFound;
            if (await _courseRepository.IsExistEditUrlNameCourseGroup(group.GroupId, group.UrlName.FixTextBetween()))
                return CourseGroupResult.ErrorUrlName;
            if (!await _courseRepository.IsExistCourseGroup(group.GroupId))
                return CourseGroupResult.NotFound;
            if (group.ParentId != null && !await _courseRepository.IsExistCourseGroup((int)group.ParentId))
                return CourseGroupResult.NotFound;
            editGroup.UrlName = group.UrlName.FixTextBetween();
            editGroup.GroupTitle = group.GroupTitle;
            editGroup.UpdateDate = DateTime.Now;
            _courseRepository.UpdateGroup(editGroup);
            await _courseRepository.Save();
            return CourseGroupResult.Success;
        }
        #endregion


        #region course master
        public async Task<List<Course>> GetAllMasterCourses(int userId)
        {
            return await _courseRepository.GetAllMasterCourses(userId);
        }
        #endregion

        #region course


        public async Task<CourseResut> CreateCourse(CreateCourseDTO course, IFormFile imgCourse, IFormFile courseDemo)
        {
            if (imgCourse == null) return CourseResut.NotImage;
            Course addCourse = new Course();
            addCourse.IsShow = true;
            addCourse.GroupId = course.GroupId;
            addCourse.SubGroup = course.SubGroup;
            addCourse.TeacherId = course.TeacherId;
            addCourse.StatusId = course.StatusId;
            addCourse.LevelId = course.LevelId;
            addCourse.CourseTitle = course.CourseTitle;
            addCourse.CourseDescription = course.CourseDescription;
            addCourse.CoursePrice = course.CoursePrice;
            addCourse.Tags = course.Tags;

            if (imgCourse != null && imgCourse.IsImage())
            {
                addCourse.CourseImageName = TextFixer.GenerateUniqCodeString(addCourse.CourseTitle) + Path.GetExtension(imgCourse.FileName);
                bool result = imgCourse
                    .AddImageToServer(addCourse.CourseImageName, PathExtensions.CourseImageOrginServer, null, null, PathExtensions.CourseImageThumbServer);
                if (!result) return CourseResut.ErrorImage;

            }
            if (courseDemo != null)
            {

                addCourse.CourseImageName = TextFixer.GenerateUniqCodeString(addCourse.CourseTitle) + Path.GetExtension(imgCourse.FileName);
                bool result = imgCourse
                   .AddVideoFileToServer(addCourse.CourseImageName, PathExtensions.CourseDemoFileServer);
                if (!result) return CourseResut.ErrorDemo;
            }
            await AddCourse(addCourse);

            return CourseResut.Success;
        }

        public async Task<FilterCourseDTO> FilterCourse(FilterCourseDTO filter)
        {
            return await _courseRepository.FilterCourse(filter);
        }

        public async Task<Course> GetCourseById(int courseId)
        {
            return await _courseRepository.GetCourseById(courseId);
        }

        public async Task<EditCourseDTO> GetCourseForEdit(int courseId)
        {
            var course = await GetCourseById(courseId);
            if (course == null) return null;
            return new EditCourseDTO()
            {
                CourseId = courseId,
                CourseDescription = course.CourseDescription,
                CourseImageName = course.CourseImageName,
                CoursePrice = course.CoursePrice,
                CourseTitle = course.CourseTitle,
                DemoFileName = course.DemoFileName,
                GroupId = course.GroupId,
                LevelId = course.LevelId,
                Tags = course.Tags,
                SubGroup = course.SubGroup,
                StatusId = course.StatusId,
                TeacherId = course.TeacherId,
            };
        }
        public async Task<CourseResut> EditCourse(EditCourseDTO course, IFormFile imgCourse, IFormFile courseDemo)
        {
            var editCourse = await GetCourseById(course.CourseId);
            if (editCourse == null) return CourseResut.Error;
            editCourse.SubGroup = course.SubGroup;
            editCourse.TeacherId = course.TeacherId;
            editCourse.StatusId = course.StatusId;
            editCourse.LevelId = course.LevelId;
            editCourse.CourseTitle = course.CourseTitle;
            editCourse.CourseDescription = course.CourseDescription;
            editCourse.CoursePrice = course.CoursePrice;
            editCourse.Tags = course.Tags;

            if (imgCourse != null && imgCourse.IsImage())
            {
                var imageName = TextFixer.GenerateUniqCodeString(course.CourseTitle) + Path.GetExtension(imgCourse.FileName);
                bool result = imgCourse
                    .AddImageToServer(imageName, PathExtensions.CourseImageOrginServer, null, null, PathExtensions.CourseImageThumbServer, editCourse.CourseImageName);
                if (!result) return CourseResut.ErrorImage;
                editCourse.CourseImageName = imageName;
            }
            if (courseDemo != null)
            {

                var demoName = TextFixer.GenerateUniqCodeString(course.CourseTitle) + Path.GetExtension(imgCourse.FileName);
                bool result = imgCourse
                   .AddVideoFileToServer(demoName, PathExtensions.CourseDemoFileServer, editCourse.DemoFileName);
                if (!result) return CourseResut.ErrorDemo;
                editCourse.DemoFileName = demoName;

            }
            await UpdateCourse(editCourse);
            return CourseResut.Success;
        }

        public async Task AddCourse(Course course)
        {
            await _courseRepository.AddCource(course);
            await _courseRepository.Save();
        }
        public async Task UpdateCourse(Course course)
        {
            _courseRepository.UpdateCourse(course);
            await _courseRepository.Save();
        }
        public Task<bool> IsMasterInCourse(int userId, int courseId)
        {
            return _courseRepository.IsMasterInCourse(userId, courseId);
        }
        public async Task<Course> GetCourseForShow(int courseId)
        {
            var result = await _courseRepository.GetCourseForShow(courseId);
            if (result == null) return null;
            return result;
        }
        public async Task<ShowCourseEpisodeDTO> GetCourseEpisodeForShow(int courseId, int episodeId, int userId)
        {
            var result = await _courseRepository.GetCourseForShow(courseId);
            if (result == null) return null;
            ShowCourseEpisodeDTO episodeDTO = new ShowCourseEpisodeDTO();
            var courseEpisode = result.CourseEpisodes.FirstOrDefault(e => e.EpisodeId == episodeId);
            if (courseEpisode == null)
            {
                episodeDTO.NotSuccesEpisodes = true;
                return episodeDTO;

            }

            if (!courseEpisode.IsFree)
            {
                bool isUserInCourse = await IsUserInCourse(userId, courseId);
                bool isMasterInCourse = await IsMasterInCourse(userId, courseId);
                bool isAdmin = await _permissionRepository.CheckPermission("AccessToCourses", userId);
                if (!isMasterInCourse && !isAdmin)
                {
                    if (!isUserInCourse)
                    {
                        episodeDTO.NotSucces = true;
                        return episodeDTO;

                    }
                }
            }



            episodeDTO.Episode = courseEpisode;
            string filePath = "";
            string checkFilePath = "";
            var checkIsExitstsFolder = PathExtensions.CourseFilesServer + courseId;
            if (!Directory.Exists(checkIsExitstsFolder))
            {
                Directory.CreateDirectory(checkIsExitstsFolder);
            }
            if (courseEpisode.EpisodeFileName.EndsWith(".rar"))
            {
                filePath = courseEpisode.EpisodeFileName.Replace(".rar", ".mp4").GetCourseFileAddress(courseId);
                checkFilePath = courseEpisode.EpisodeFileName.Replace(".rar", ".mp4").GetCourseFileServerAddress(courseId);
            }
            else if (courseEpisode.EpisodeFileName.EndsWith(".zip"))
            {
                filePath = courseEpisode.EpisodeFileName.Replace(".zip", ".mp4").GetCourseFileAddress(courseId);
                checkFilePath = courseEpisode.EpisodeFileName.Replace(".zip", ".mp4").GetCourseFileServerAddress(courseId);
            }
            else if (courseEpisode.EpisodeFileName.EndsWith(".mp4"))
            {
                filePath = courseEpisode.EpisodeFileName.GetCourseFileAddress(courseId);
                checkFilePath = courseEpisode.EpisodeFileName.GetCourseFileServerAddress(courseId);
            }

            if (!File.Exists(checkFilePath.Replace(" ", "")))
            {
                string targetPath = PathExtensions.CourseFilesServer + courseId;

                string rarPath = courseEpisode.EpisodeFileName.GetCourseFileServerAddress(courseId);
                if (File.Exists(rarPath) && !courseEpisode.EpisodeFileName.EndsWith(".mp4"))
                {
                    var archive = ArchiveFactory.Open(rarPath);

                    var Entries = archive.Entries.OrderBy(x => x.Key.Length);
                    foreach (var en in Entries)
                    {
                        if (Path.GetExtension(en.Key) == ".mp4")
                        {
                            if (courseEpisode.EpisodeFileName.EndsWith(".rar"))
                            {
                                en.WriteTo(File.Create(Path.Combine(targetPath, courseEpisode.EpisodeFileName.Replace(".rar", ".mp4").Replace(" ", ""))));
                            }
                            else if (courseEpisode.EpisodeFileName.EndsWith(".zip"))
                            {
                                en.WriteTo(File.Create(Path.Combine(targetPath, courseEpisode.EpisodeFileName.Replace(".zip", ".mp4").Replace(" ", ""))));
                            }

                        }
                    }
                }
                else if (courseEpisode.EpisodeFileName.EndsWith(".mp4"))
                {
                    filePath = courseEpisode.EpisodeFileName.GetCourseFileAddress(courseId);
                }

            }
            episodeDTO.FilePath = filePath.Replace(" ", "");
            return episodeDTO;
        }
        public async Task<bool> IsUserInCourse(int userId, int courseId)
        {
            return await _courseRepository.IsUserInCourse(userId, courseId);

        }
        public async Task<List<ShowCourseListItemDTO>> GetPopularCourse(int takeCount = 8)
        {
            return await _courseRepository.GetPopularCourse(takeCount);
        }
        public async Task<List<ShowCourseListItemDTO>> GetLatesCourse(int takeCount = 8)
        {
            return await _courseRepository.GetLatesCourse(takeCount);
        }



        public async Task<List<ShowCourseListItemDTO>> GetSuggestedCourse(int takeCount = 8)
        {
            return await _courseRepository.GetSuggestedCourse(takeCount);
        }

        public async Task IsSuggestedCourse(int courseId)
        {
            var course = await GetCourseById(courseId);
            course.IsSuggested = !course.IsSuggested;
            await UpdateCourse(course);
        }

        public async Task IsShowCourse(int courseId)
        {
            var course = await GetCourseById(courseId);
            course.IsShow = !course.IsShow;
            await UpdateCourse(course);
        }

        public async Task<bool> IsFree(int courseId)
        {
            return await _courseRepository.IsFree(courseId);
        }

        public async Task<List<string>> GetAllCourseTitle(string term)
        {
            return await _courseRepository.GetAllCourseTitle(term);
        }

        public async Task<CourseInformaationDTO> GetCourseInformaation()
        {
            return await _courseRepository.GetCourseInformaation();
        }

        public async Task<DeleteResult> DeleteCourse(int courseId)
        {
            var course = await _courseRepository.GetCourseById(courseId);
            if (course == null) return DeleteResult.NotFound;
            course.IsDelete = true;
            _courseRepository.UpdateCourse(course);
            await _courseRepository.Save();
            return DeleteResult.Success;
        }

        #endregion

        #region course comment

        public async Task<FilterCourseCommentDTO> FilterCourseComment(FilterCourseCommentDTO filter)
        {
            return await _courseRepository.FilterCourseComment(filter);
        }

        public async Task<CourseComment> GetCommentById(int commentId)
        {
            return await _courseRepository.GetCommentById(commentId);
        }

        public async Task<CourseComment> ShowCommentById(int commentId)
        {
            return await _courseRepository.ShowCommentById(commentId);
        }

        public async Task AddComment(CourseComment comment)
        {
            await _courseRepository.AddComment(comment);
            await _courseRepository.Save();
        }

        public async Task<DeleteCommentResult> DeleteComment(int commentId)
        {
            var comment = await _courseRepository.GetCommentById(commentId);
            if (comment == null) return DeleteCommentResult.NotFound;
            comment.IsDelete = true;
            comment.UpdateDate = DateTime.Now;
            await _courseRepository.Save();
            return DeleteCommentResult.Success;
        }

        public async Task IsAdminReadComment(int commentId)
        {
            var comment = await _courseRepository.GetCommentById(commentId);
            comment.IsAdminRead = true;
            await _courseRepository.Save();
        }




        #endregion
        #region course vote
        public async Task AddVote(int userId, int courseId, bool vote)
        {
            var userVote = await _courseRepository.GetCourseVote(userId, courseId);
            if (userVote != null)
            {
                userVote.Vote = vote;
                _courseRepository.UpdateVote(userVote);
            }
            else
            {
                userVote = new CourseVote()
                {
                    CourseId = courseId,
                    UserId = userId,
                    Vote = vote
                };
                await _courseRepository.AddVote(userVote);
            }
            await _courseRepository.Save();
        }

        public async Task<Tuple<int, int>> GetCourseVotes(int courseId)
        {
            return await _courseRepository.GetCourseVotes(courseId);
        }
        #endregion

        #region user course
        public async Task<List<ShowCoursesUserDTO>> ShowCoursesUser(int userId)
        {
            return await _courseRepository.ShowCoursesUser(userId);
        }
        #endregion






    }
}
