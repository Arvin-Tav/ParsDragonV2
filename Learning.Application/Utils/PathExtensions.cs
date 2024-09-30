using System.IO;

namespace Learning.Application.Utils
{
    public static class PathExtensions
    {
        #region default images

        public static string DefaultAvatar = "/images/DefultAvatar.jpg";

        #endregion
        #region default Course

        public static string DefaultCourse = "/images/LazyLoad.png";

        #endregion
        #region user avatar

        public static string UserAvatarOrgin= "/allFiles/userAvatar/orgin/";
        public static string UserAvatarOrginServer=Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/allFiles/userAvatar/orgin/");

        public static string UserAvatarThumb = "/allFiles/userAvatar/thumb/";
        public static string UserAvatarThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/allFiles/userAvatar/thumb/");

        #endregion

        #region course

        public static string CourseImageOrgin = "/allFiles/course/orgin/";
        public static string CourseImageOrginServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/allFiles/course/orgin/");

        public static string CourseImageThumb = "/allFiles/course/thumb/";
        public static string CourseImageThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/allFiles/course/thumb/");

        public static string CourseDemoFile = "/allFiles/course/demoFile/";
        public static string CourseDemoFileServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/allFiles/course/demoFile/");
       
        public static string CourseFiles = "/allFiles/course/files/";
        public static string CourseFilesServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/allFiles/course/files/");
        #endregion

        #region banner

        public static string BannerImageOrgin = "/allFiles/banner/orgin/";
        public static string BannerImageOrginServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/allFiles/banner/orgin/");
                             
        public static string BannerImageThumb = "/allFiles/banner/thumb/";
        public static string BannerImageThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/allFiles/banner/thumb/");

        #endregion
        #region blog

        public static string BlogImageOrgin = "/allFiles/blog/orgin/";
        public static string BlogImageOrginServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/allFiles/blog/orgin/");
                             
        public static string BlogImageThumb = "/allFiles/blog/thumb/";
        public static string BlogImageThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/allFiles/blog/thumb/");

        #endregion

        #region config

        public static string ConfigImageOrgin = "/allFiles/config/orgin/";
        public static string ConfigImageOrginServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/allFiles/config/orgin/");
        #endregion


        #region online class

        public static string OnlineClassImageOrgin = "/allFiles/onlineClass/orgin/";
        public static string OnlineClassImageOrginServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/allFiles/onlineClass/orgin/");

        public static string OnlineClassImageThumb = "/allFiles/onlineClass/thumb/";
        public static string OnlineClassImageThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/allFiles/onlineClass/thumb/");

        public static string OnlineClassDemoFile = "/allFiles/onlineClass/demoFile/";
        public static string OnlineClassDemoFileServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/allFiles/onlineClass/demoFile/");

        public static string OnlineClassFiles = "/allFiles/onlineClass/files/";
        public static string OnlineClassFilesServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/allFiles/onlineClass/files/");
        #endregion


        #region slider

        public static string SliderImageOrgin = "/allFiles/slider/orgin/";
        public static string SliderImageOrginServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/allFiles/slider/orgin/");

        public static string SliderImageThumb = "/allFiles/slider/thumb/";
        public static string SliderImageThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/allFiles/slider/thumb/");

        #endregion

        #region ckeditor
        public static string CkEditor = "/allFiles/ckEditor/";
        public static string CkEditorServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/allFiles/ckEditor/");

        #endregion
    }
}
