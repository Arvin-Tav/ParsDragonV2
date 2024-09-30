using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.DTO.Course
{
    public enum CourseResut
    {
        Error,
        NotImage,
        ErrorImage,
        ErrorDemo,
        Success,
    }
    public enum CourseEpisodeResut
    {
        Error,
        NotFound,
        NotFile,
        ExistFile,
        ErrorFile,
        Success,
    }
    public enum UploadFileEpisodeResut
    {
        NotFile,
        ExistFile,
        ErrorFile,
        Success,
    }
    public enum DeleteCommentResult
    {
        NotFound,
        Success,
    }
    public enum DeleteCourseGroupResult
    {
        NotFound,
        SetCourse,
        Success,
    }
    public enum CourseGroupResult
    {
        NotFound,
        ErrorUrlName,
        Success,
    }
    public enum DeleteResult
    {
        NotFound,
        Success,
    }
}
