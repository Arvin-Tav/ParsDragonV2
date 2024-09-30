using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.DTO.Course
{
    public class ShowCourseListItemDTO
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string TeacherName { get; set; }
        public string ImageName { get; set; }
        public int Price { get; set; }
        public TimeSpan TotalTime { get; set; }
    }
}
