using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.DTO.CourseEpisode
{
    public class CourseEpidoeForMaster
    {
        public int EpisodeId { get; set; }
        public string EpisodeTitle { get; set; }
        public TimeSpan EpisodeTime { get; set; }
        public bool IsFree { get; set; }

    }
}
