using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.DTO.Course
{
    public class ShowCoursesUserDTO
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public int CoursePrice { get; set; }
    }
}
