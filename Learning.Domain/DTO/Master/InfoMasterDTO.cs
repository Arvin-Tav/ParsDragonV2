using Learning.Domain.DTO.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.DTO.Master
{
    public class InfoMasterDTO
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string AboutMe { get; set; }
        public List<ShowCourseListItemDTO> Courses { get; set; }
    }
}
