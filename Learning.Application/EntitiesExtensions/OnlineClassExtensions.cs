using Learning.Application.Utils;
using Learning.Domain.DTO.OnlineClass;
using Learning.Domain.Models.OnlineClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Application.EntitiesExtensions
{
    public static class OnlineClassExtensions
    {
        public static string GetOnlineClassImageAddress(this OnlineClass onlineClass)
        {
            return PathExtensions.OnlineClassImageOrgin + onlineClass.OnlineClassImageName;
        }
        public static string GetOnlineClassImageThumbAddress(this OnlineClass onlineClass)
        {
            return PathExtensions.OnlineClassImageThumb + onlineClass.OnlineClassImageName;
        }
        public static string GetOnlineClassImageAddress(this EditOnlineClassDTO onlineClass)
        {
            return PathExtensions.OnlineClassImageOrgin + onlineClass.OnlineClassImageName;
        }
        public static string GetOnlineClassImageThumbAddress(this EditOnlineClassDTO onlineClass)
        {
            return PathExtensions.OnlineClassImageThumb + onlineClass.OnlineClassImageName;
        }
        public static string GetOnlineClassFileAddress(this string onlineClassEpisode, int courseId)
        {
            return PathExtensions.CourseFiles + courseId + "/" + onlineClassEpisode;
        }
        public static string GetOnlineClassFileServerAddress(this string onlineClassEpisode, int courseId)
        {
            return PathExtensions.CourseFilesServer + courseId + "/" + onlineClassEpisode;
        }
    }
}
