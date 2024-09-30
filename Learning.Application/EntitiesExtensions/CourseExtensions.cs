using Learning.Application.Utils;
using Learning.Domain.DTO.Course;
using Learning.Domain.Models.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Application.EntitiesExtensions
{
    public static class CourseExtensions
    {
        public static string GetCourseImageAddress(this Course course)
        {
            return PathExtensions.CourseImageOrgin + course.CourseImageName;
        }
        public static string GetCourseImageThumbAddress(this Course course)
        {
            return PathExtensions.CourseImageThumb + course.CourseImageName;
        }
        public static string GetCourseImageAddress(this EditCourseDTO course)
        {
            return PathExtensions.CourseImageOrgin + course.CourseImageName;
        }
        public static string GetCourseImageThumbAddress(this EditCourseDTO course)
        {
            return PathExtensions.CourseImageThumb + course.CourseImageName;
        }
        public static string GetCourseImageAddress(this ShowCourseListItemDTO course)
        {
            return PathExtensions.CourseImageOrgin + course.ImageName;
        }
        public static string GetCourseImageThumbAddress(this ShowCourseListItemDTO course)
        {
            return PathExtensions.CourseImageThumb + course.ImageName;
        }
        public static string GetCourseFileAddress(this string courseEpisode, int courseId)
        {
            return PathExtensions.CourseFiles + courseId + "/" + courseEpisode;
        }
        public static string GetCourseFileServerAddress(this string courseEpisode, int courseId)
        {
            return PathExtensions.CourseFilesServer + courseId + "/" + courseEpisode;
        }
    }
}
