using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.DTO.CourseEpisode
{
    public class EditCourseEpisodeDTO : CreateCourseEpisodeDTO
    {
        public int EpisodeId { get; set; }
    }
}
