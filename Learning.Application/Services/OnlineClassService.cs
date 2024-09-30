using Learning.Application.Convertors;
using Learning.Application.EntitiesExtensions;
using Learning.Application.Extentions;
using Learning.Application.Interfaces;
using Learning.Application.Utils;
using Learning.Domain.DTO.OnlineClass;
using Learning.Domain.Interfaces;
using Learning.Domain.Models.Course;
using Learning.Domain.Models.OnlineClass;
using Microsoft.AspNetCore.Http;
using SharpCompress.Archives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Application.Services
{
    public class OnlineClassService : IOnlineClassService
    {
        #region constructor
        private readonly IOnlineClassRepository _onlineClassRepository;
        public OnlineClassService(IOnlineClassRepository onlineClassRepository)
        {
            _onlineClassRepository = onlineClassRepository;
        }

        #endregion
        #region online class

        public async Task<FilterOnlineClassDTO> FilterOnlineClass(FilterOnlineClassDTO filter)
        {
            return await _onlineClassRepository.FilterOnlineClass(filter);
        }

        public async Task AddUserOnlineClass(UserOnlineClass userOnlineClass)
        {
            await _onlineClassRepository.AddUserOnlineClass(userOnlineClass);
        }

        public async Task<List<int>> GetUsersIdByOnlineClassId(int onlineClassId)
        {
            return await _onlineClassRepository.GetUsersIdByOnlineClassId(onlineClassId);
        }

        public async Task<OnlineClassResult> CreateOnlineClass(CreateOnlineClassDTO onlineClass, IFormFile imageName)
        {
            string date = onlineClass.StartClass;
            if (!date.PersianNumberToEnglishNumber(out date)) return OnlineClassResult.NotDate;
            onlineClass.StartClass = date;

            if (imageName == null) return OnlineClassResult.NotImage;
            onlineClass.AvatarOnlineClassName = TextFixer.GenerateUniqCodeString(onlineClass.Title) + Path.GetExtension(imageName.FileName);
            bool result = imageName
                .AddImageToServer(onlineClass.AvatarOnlineClassName, PathExtensions.OnlineClassImageOrginServer, null, null, PathExtensions.OnlineClassImageThumbServer);
            if (!result) return OnlineClassResult.ErrorImage;


            OnlineClass newOnlineClasss = new OnlineClass();
            newOnlineClasss.Address = onlineClass.Address;
            newOnlineClasss.BodyHtml = onlineClass.BodyHtml;
            newOnlineClasss.NumberSessions = (int)onlineClass.NumberSessions;
            newOnlineClasss.IsShow = true;
            newOnlineClasss.Description = onlineClass.Description;
            newOnlineClasss.GroupId = onlineClass.GroupId;
            newOnlineClasss.SubGroup = onlineClass.SubGroup;
            newOnlineClasss.Tell = onlineClass.Tell;
            newOnlineClasss.Title = onlineClass.Title;
            newOnlineClasss.TeacherId = onlineClass.TeacherId;
            newOnlineClasss.Link = onlineClass.Link;
            newOnlineClasss.OnlineClassImageName = onlineClass.AvatarOnlineClassName;
            newOnlineClasss.Price = (int)onlineClass.Price;
            newOnlineClasss.StartClass = onlineClass.StartClass.DateShamsiToMiladi();
            newOnlineClasss.Tags = onlineClass.Tags;
            newOnlineClasss.TimeSessions = (int)onlineClass.TimeSessions;
            await AddOnlineClass(newOnlineClasss);
            return OnlineClassResult.Success;
        }

        public async Task<OnlineClass> GetOnlineClassById(int onlineClassId)
        {
            return await _onlineClassRepository.GetOnlineClassById(onlineClassId);
        }

        public Task<InfoOnlineClassForUserDTO> GetInfoOnlineClassForUser(int onlineClassId)
        {
            return _onlineClassRepository.GetInfoOnlineClassForUser(onlineClassId);
        }

        public async Task<List<OnlineClassEpisode>> ShowOnlineClassEpisodeUser(int onlineClassId)
        {
            return await _onlineClassRepository.ShowOnlineClassEpisodeUser(onlineClassId);
        }
        public async Task<ShowOnlineClassEpisodeDTO> ShowOnlineClassEpisode(int id, int episodeId = 0)
        {
            ShowOnlineClassEpisodeDTO episodeDTO = new ShowOnlineClassEpisodeDTO();
            List<OnlineClassEpisode> onlineClass = await _onlineClassRepository.ShowOnlineClassEpisodeUser(id);
            if (episodeId != 0)
            {
                var ep = onlineClass.Find(e => e.OnlineClassEpisodeId == episodeId);
                string filePath = "";
                string checkFilePath = "";

                var checkIsExitstsFolder = PathExtensions.OnlineClassFilesServer + id;
                if (!Directory.Exists(checkIsExitstsFolder))
                {
                    Directory.CreateDirectory(checkIsExitstsFolder);
                }
                if (ep.EpisodeFileName.EndsWith(".rar"))
                {
                    filePath =ep.EpisodeFileName.Replace(".rar", ".mp4").GetOnlineClassFileAddress(id);
                    checkFilePath =ep.EpisodeFileName.Replace(".rar", ".mp4").GetOnlineClassFileServerAddress(id);
                }
                else if (ep.EpisodeFileName.EndsWith(".zip"))
                {
                    filePath = $"{PathExtensions.OnlineClassFiles + id}/" + ep.EpisodeFileName.Replace(".zip", ".mp4");
                    checkFilePath =ep.EpisodeFileName.Replace(".zip", ".mp4").GetOnlineClassFileServerAddress(id);
                }
                else if (ep.EpisodeFileName.EndsWith(".mp4"))
                {
                    filePath = $"{PathExtensions.OnlineClassFiles + id}/" + ep.EpisodeFileName;
                    checkFilePath = ep.EpisodeFileName.GetOnlineClassFileServerAddress(id);
                }




                if (!File.Exists(checkFilePath.Replace(" ", "")))
                {
                    string targetPath = PathExtensions.OnlineClassFilesServer + id;

                    string rarPath = ep.EpisodeFileName.GetCourseFileServerAddress(id);
                    if (System.IO.File.Exists(rarPath) && !ep.EpisodeFileName.EndsWith(".mp4"))
                    {
                        var archive = ArchiveFactory.Open(rarPath);

                        var Entries = archive.Entries.OrderBy(x => x.Key.Length);
                        foreach (var en in Entries)
                        {
                            if (Path.GetExtension(en.Key) == ".mp4")
                            {
                                if (ep.EpisodeFileName.EndsWith(".rar"))
                                {
                                    en.WriteTo(System.IO.File.Create(Path.Combine(targetPath, ep.EpisodeFileName.Replace(".rar", ".mp4").Replace(" ", ""))));
                                }
                                else if (ep.EpisodeFileName.EndsWith(".zip"))
                                {
                                    en.WriteTo(System.IO.File.Create(Path.Combine(targetPath, ep.EpisodeFileName.Replace(".zip", ".mp4").Replace(" ", ""))));
                                }

                            }
                        }
                    }
                    else if (ep.EpisodeFileName.EndsWith(".mp4"))
                    {
                        filePath = ep.EpisodeFileName.GetOnlineClassFileAddress(id);
                    }


                }

                episodeDTO.FilePath = filePath.Replace(" ", "");
            }
            episodeDTO.Episodes = onlineClass;
            return episodeDTO;
        }

        public async Task<EditOnlineClassDTO> GetOnlineClassByIdForEdit(int onlineClassId)
        {
            OnlineClass onlineClass = await GetOnlineClassById(onlineClassId);
            if (onlineClass == null) return null;

            EditOnlineClassDTO editOnlineClass = new EditOnlineClassDTO();
            editOnlineClass.OnlineClassId = onlineClass.OnlineClassId;
            editOnlineClass.Address = onlineClass.Address;
            editOnlineClass.BodyHtml = onlineClass.BodyHtml;
            editOnlineClass.NumberSessions = (int)onlineClass.NumberSessions;
            editOnlineClass.Description = onlineClass.Description;
            editOnlineClass.GroupId = onlineClass.GroupId;
            editOnlineClass.SubGroup = onlineClass.SubGroup;
            editOnlineClass.Tell = onlineClass.Tell;
            editOnlineClass.Title = onlineClass.Title;
            editOnlineClass.TeacherId = onlineClass.TeacherId;
            editOnlineClass.Link = onlineClass.Link;
            editOnlineClass.OnlineClassImageName = onlineClass.OnlineClassImageName;
            editOnlineClass.Price = (int)onlineClass.Price;
            editOnlineClass.StartClass = Convert.ToString(onlineClass.StartClass.ToShamsi());
            editOnlineClass.Tags = onlineClass.Tags;
            editOnlineClass.TimeSessions = (int)onlineClass.TimeSessions;

            return editOnlineClass;
        }

        public async Task AddOnlineClass(OnlineClass onlineClass)
        {
            await _onlineClassRepository.AddOnlineClass(onlineClass);
            await _onlineClassRepository.Save();
        }

        public async Task UpdateOnlineClass(OnlineClass onlineClass)
        {
            _onlineClassRepository.UpdateOnlineClass(onlineClass);
            await _onlineClassRepository.Save();
        }

        public async Task<OnlineClassResult> UpdateByOnlineClass(EditOnlineClassDTO onlineClass, IFormFile imageName)
        {
            OnlineClass getOnlineClass = await GetOnlineClassById(onlineClass.OnlineClassId);
            if (getOnlineClass == null) return OnlineClassResult.Error;
            if (Convert.ToString(getOnlineClass.StartClass.ToShamsi()) != onlineClass.StartClass)
            {
                string date = onlineClass.StartClass;
                if (!date.PersianNumberToEnglishNumber(out date)) return OnlineClassResult.NotDate;
                onlineClass.StartClass = date;
            }
            if (imageName != null && !imageName.IsImage()) return OnlineClassResult.NotImage;
            if (imageName != null)
            {
                onlineClass.OnlineClassImageName = TextFixer.GenerateUniqCodeString(onlineClass.Title) + Path.GetExtension(imageName.FileName);
                bool result = imageName
                    .AddImageToServer(onlineClass.OnlineClassImageName, PathExtensions.OnlineClassImageOrginServer, null, null, PathExtensions.OnlineClassImageThumbServer, getOnlineClass.OnlineClassImageName);
                if (!result) return OnlineClassResult.ErrorImage;
            }
            getOnlineClass.Address = onlineClass.Address;
            getOnlineClass.BodyHtml = onlineClass.BodyHtml;
            getOnlineClass.NumberSessions = (int)onlineClass.NumberSessions;
            getOnlineClass.IsShow = true;
            getOnlineClass.Description = onlineClass.Description;
            getOnlineClass.GroupId = onlineClass.GroupId;
            getOnlineClass.SubGroup = onlineClass.SubGroup;
            getOnlineClass.Tell = onlineClass.Tell;
            getOnlineClass.Title = onlineClass.Title;
            getOnlineClass.TeacherId = onlineClass.TeacherId;
            getOnlineClass.Link = onlineClass.Link;
            getOnlineClass.OnlineClassImageName = onlineClass.OnlineClassImageName;
            getOnlineClass.Price = (int)onlineClass.Price;
            getOnlineClass.StartClass = onlineClass.StartClass.DateShamsiToMiladi();
            getOnlineClass.Tags = onlineClass.Tags;
            getOnlineClass.TimeSessions = (int)onlineClass.TimeSessions;
            await UpdateOnlineClass(getOnlineClass);
            return OnlineClassResult.Success;
        }

        public async Task IsShowOnlineClass(int onlineClassId)
        {
            var onlineClass = await GetOnlineClassById(onlineClassId);
            onlineClass.IsShow = !onlineClass.IsShow;
            await UpdateOnlineClass(onlineClass);
        }

        public async Task<List<OnlineClassEpisode>> GetOnlineClassEpisodeByOnlineClassId(int onlineClassId)
        {
            return await _onlineClassRepository.GetOnlineClassEpisodeByOnlineClassId(onlineClassId);
        }

        public async Task<bool> CheckExistFile(string fileName, int onlineClassId)
        {
            string path = PathExtensions.OnlineClassFilesServer + "/" + onlineClassId + "/" + fileName;
            return File.Exists(path);
        }

        public async Task AddOnlineClassEpisode(OnlineClassEpisode episode)
        {
            await _onlineClassRepository.AddOnlineClassEpisode(episode);
            await _onlineClassRepository.Save();
        }
        public async Task<OnlineClassEpisodeResult> CreateOnlineClassEpisode(CreateOnlineClassEpisodeDTO episode, IFormFile episodeFile)
        {

            if (episodeFile == null) return OnlineClassEpisodeResult.NotFile;
            if (await CheckExistFile(episodeFile.FileName, episode.OnlineClassId))
            {
                return OnlineClassEpisodeResult.ErrorFile;
            }
            OnlineClassEpisode newEpisode = new OnlineClassEpisode();

            newEpisode.EpisodeFileName = episodeFile.FileName;
            bool result = episodeFile
            .AddVideoFileToServer(newEpisode.EpisodeFileName, PathExtensions.OnlineClassFilesServer + episode.OnlineClassId);
            if (!result) return OnlineClassEpisodeResult.ErrorFile;

            newEpisode.OnlineClassId = episode.OnlineClassId;
            newEpisode.Title = episode.Title;

            await _onlineClassRepository.AddOnlineClassEpisode(newEpisode);
            await _onlineClassRepository.Save();
            return OnlineClassEpisodeResult.Success;
        }

        public Task<OnlineClassEpisode> GetOnlineClassEpisodeById(int episodeId)
        {
            return _onlineClassRepository.GetOnlineClassEpisodeById(episodeId);
        }
        public async Task<EditOnlineClassEpisodeDTO> GetOnlineClassEpisodeForEdit(int episodeId)
        {
            var episode=await _onlineClassRepository.GetOnlineClassEpisodeById(episodeId);
            if (episode == null) return null;

            return new EditOnlineClassEpisodeDTO()
            {
                EpisodeFileName=episode.EpisodeFileName,
                OnlineClassEpisodeId=episode.OnlineClassEpisodeId,
                OnlineClassId=episode.OnlineClassId,
                Title=episode.Title,
            };
        }

        public async Task<OnlineClassEpisodeResult> EditOnlineClassEpisode(EditOnlineClassEpisodeDTO episode, IFormFile episodeFile)
        {
            var editEpisode = await GetOnlineClassEpisodeById(episode.OnlineClassEpisodeId);
            if (editEpisode == null || editEpisode.OnlineClassId != episode.OnlineClassId) return OnlineClassEpisodeResult.Error;
            if (episodeFile != null)
            {
                if (episodeFile.FileName == episode.EpisodeFileName)
                {
                    await DeleteFileOnlineClassEpisode(episodeFile.FileName, episode.OnlineClassId);
                }
                if (await CheckExistFile(episodeFile.FileName, episode.OnlineClassId))
                {
                    return OnlineClassEpisodeResult.ErrorFile;
                }
                episode.EpisodeFileName = episodeFile.FileName;
                bool result = episodeFile
                .AddVideoFileToServer(episode.EpisodeFileName, PathExtensions.OnlineClassFilesServer + episode.OnlineClassId, editEpisode.EpisodeFileName);
                if (!result) return OnlineClassEpisodeResult.ErrorFile;
            }
            editEpisode.EpisodeFileName = episode.EpisodeFileName;
            editEpisode.Title = episode.Title;
            editEpisode.EpisodeFileName = episode.EpisodeFileName;

            await UpdateOnlineClassEpisode(editEpisode);
            return OnlineClassEpisodeResult.Success;
        }

        public async Task DeleteFileOnlineClassEpisode(string fileName, int onlioneClassId)
        {
            fileName.DeleteImage(PathExtensions.CourseFilesServer + onlioneClassId, null);
        }

        public async Task UpdateOnlineClassEpisode(OnlineClassEpisode episode)
        {
            _onlineClassRepository.UpdateOnlineClassEpisode(episode);
            await _onlineClassRepository.Save();
        }

        public async Task IsShowOnlineClassEpisode(int eposideId)
        {
            var episode = await GetOnlineClassEpisodeById(eposideId);
            episode.IsShow = !episode.IsShow;
            await UpdateOnlineClassEpisode(episode);
        }
        #endregion

        public async Task<bool> IsMasterInOnlineClass(int userId, int onlineClassId)
        {
            return await _onlineClassRepository.IsMasterInOnlineClass(userId, onlineClassId);
        }
        public async Task<bool> IsUserInOnlineClass(int userId, int onlineClassId)
        {
            return await _onlineClassRepository.IsUserInOnlineClass(userId, onlineClassId);
        }

        #region Contact
        public async Task<List<OnlineClassContact>> GetAllContactsByOnlineId(int onlineClassId)
        {
            return await _onlineClassRepository.GetAllContactsByOnlineId(onlineClassId);
        }

        public async Task<OnlineClassContact> GetOnlineClassContactById(int contactId)
        {
            return await _onlineClassRepository.GetOnlineClassContactById(contactId);
        }

        public async Task<OnlineClassInfoResult> AddOnlineClassContactsByList(int onlineClassId, List<string> contactList)
        {
            var onlineClass = await _onlineClassRepository.GetOnlineClassById(onlineClassId);
            if (onlineClass == null) return OnlineClassInfoResult.NotFound;
            _onlineClassRepository.RemoveOnlineClassContact(onlineClassId);

            foreach (var item in contactList.Where(i => i != null && i != ""))
            {
                await _onlineClassRepository.AddOnlineClassContact(new OnlineClassContact()
                {
                    OnlineClassId = onlineClassId,
                    Name = item,
                });
            }
            await _onlineClassRepository.Save();
            return OnlineClassInfoResult.Success;
        }

        #endregion
        #region Goal
        public async Task<List<OnlineClassGoal>> GetAllGoalsByOnlineId(int onlineClassId)
        {
            return await _onlineClassRepository.GetAllGoalsByOnlineId(onlineClassId);
        }

        public async Task<OnlineClassGoal> GetOnlineClassGoalById(int goalId)
        {
            return await _onlineClassRepository.GetOnlineClassGoalById(goalId);
        }

        public async Task<OnlineClassInfoResult> AddOnlineClassGoalsByList(int onlineClassId, List<string> goalList)
        {
            var onlineClass = await _onlineClassRepository.GetOnlineClassById(onlineClassId);
            if (onlineClass == null) return OnlineClassInfoResult.NotFound;
            _onlineClassRepository.RemoveAllOnlineClassGoal(onlineClassId);
            foreach (var item in goalList.Where(i => i != null && i != ""))
            {
                await _onlineClassRepository.AddOnlineClassGoal(new OnlineClassGoal()
                {
                    OnlineClassId = onlineClassId,
                    Name = item,
                });
            }
            await _onlineClassRepository.Save();
            return OnlineClassInfoResult.Success;
        }

        public async Task AddOnlineClassGoal(OnlineClassGoal goal)
        {
            await _onlineClassRepository.AddOnlineClassGoal(goal);
            await _onlineClassRepository.Save();
        }

        public async Task RemoveAllOnlineClassGoal(int onlineClassId)
        {
            _onlineClassRepository.RemoveAllOnlineClassGoal(onlineClassId);
            await _onlineClassRepository.Save();
        }
        #endregion

        #region Heading

        public async Task<List<OnlineClassHeading>> GetAllHeadingsByOnlineId(int onlineClassId)
        {
            return await _onlineClassRepository.GetAllHeadingsByOnlineId(onlineClassId);
        }

        public async Task<OnlineClassHeading> GetOnlineClassHeadingById(int headingId)
        {
            return await _onlineClassRepository.GetOnlineClassHeadingById(headingId);
        }

        public async Task<OnlineClassInfoResult> AddOnlineClassHeadingsByList(int onlineClassId, List<string> headingList)
        {
            var onlineClass = await _onlineClassRepository.GetOnlineClassById(onlineClassId);
            if (onlineClass == null) return OnlineClassInfoResult.NotFound;
            _onlineClassRepository.RemoveAllOnlineClassHeading(onlineClassId);
            foreach (var item in headingList.Where(i => i != null && i != ""))
            {
                await _onlineClassRepository.AddOnlineClassHeading(new OnlineClassHeading()
                {
                    OnlineClassId = onlineClassId,
                    Name = item,
                });
            }
            await _onlineClassRepository.Save();
            return OnlineClassInfoResult.Success;

        }

        public async Task AddOnlineClassHeading(OnlineClassHeading heading)
        {
            await _onlineClassRepository.AddOnlineClassHeading(heading);
            await _onlineClassRepository.Save();
        }

        public async Task RemoveAllOnlineClassHeading(int onlineClassId)
        {
            _onlineClassRepository.RemoveAllOnlineClassHeading(onlineClassId);
            await _onlineClassRepository.Save();
        }

        #endregion

        #region Prerequisite
        public async Task<List<OnlineClassPrerequisite>> GetAllPrerequisitesByOnlineId(int onlineClassId)
        {
            return await _onlineClassRepository.GetAllPrerequisitesByOnlineId(onlineClassId);

        }

        public async Task<OnlineClassPrerequisite> GetOnlineClassPrerequisiteById(int prerequisiteId)
        {
            return await _onlineClassRepository.GetOnlineClassPrerequisiteById(prerequisiteId);

        }

        public async Task<OnlineClassInfoResult> AddOnlineClassPrerequisitesByList(int onlineClassId, List<string> prerequisiteList)
        {
            var onlineClass = await _onlineClassRepository.GetOnlineClassById(onlineClassId);
            if (onlineClass == null) return OnlineClassInfoResult.NotFound;
            _onlineClassRepository.RemoveAllOnlineClassPrerequisite(onlineClassId);
            foreach (var item in prerequisiteList.Where(i => i != null && i != ""))
            {
                await AddOnlineClassPrerequisite(new OnlineClassPrerequisite()
                {
                    OnlineClassId = onlineClassId,
                    Name = item,
                });
            }
            await _onlineClassRepository.Save();
            return OnlineClassInfoResult.Success;
        }

        public async Task AddOnlineClassPrerequisite(OnlineClassPrerequisite prerequisite)
        {
            await _onlineClassRepository.AddOnlineClassPrerequisite(prerequisite);
            await _onlineClassRepository.Save();
        }

        public async Task RemoveAllOnlineClassPrerequisite(int onlineClassId)
        {
            _onlineClassRepository.RemoveAllOnlineClassPrerequisite(onlineClassId);
            await _onlineClassRepository.Save();
        }
        #endregion

        #region Day

        public async Task<List<OnlineClassOfDay>> GetAllDaysByOnlineId(int onlineClassId)
        {
            return await _onlineClassRepository.GetAllDaysByOnlineId(onlineClassId);
        }

        public async Task<OnlineClassOfDay> GetOnlineClassDayById(int dayId)
        {
            return await _onlineClassRepository.GetOnlineClassDayById(dayId);
        }

        public async Task<OnlineClassInfoResult> AddOnlineClassDaysByList(int onlineClassId, List<string> day, List<string> startTime, List<string> endTime)
        {
            var onlineClass = await _onlineClassRepository.GetOnlineClassById(onlineClassId);
            if (onlineClass == null) return OnlineClassInfoResult.NotFound;

            _onlineClassRepository.RemoveAllOnlineClassDay(onlineClassId);
            for (int i = 0; i < day.Count; i++)
            {
                if (!string.IsNullOrEmpty(day[i]) && !string.IsNullOrEmpty(startTime[i]) && !string.IsNullOrEmpty(endTime[i]))
                {
                    await _onlineClassRepository.AddOnlineClassDay(new OnlineClassOfDay()
                    {
                        OnlineClassId = onlineClassId,
                        Day = day[i],
                        StartTime = startTime[i],
                        EndTime = endTime[i],
                    });
                }

            }
            await _onlineClassRepository.Save();
            return OnlineClassInfoResult.Success;
        }

        public async Task AddOnlineClassDay(OnlineClassOfDay day)
        {
            await _onlineClassRepository.AddOnlineClassDay(day);
            await _onlineClassRepository.Save();
        }

        public async Task RemoveAllOnlineClassDay(int onlineClassId)
        {
            _onlineClassRepository.RemoveAllOnlineClassDay(onlineClassId);
            await _onlineClassRepository.Save();
        }




        #endregion











    }
}
