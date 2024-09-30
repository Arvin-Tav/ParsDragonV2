using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.Models.OnlineClass
{
    public class ShowOnlineClassEpisodeDTO
    {
        public List<OnlineClassEpisode> Episodes { get; set; }
        [MaxLength(150)]
        public string FilePath { get; set; }
    }
}
