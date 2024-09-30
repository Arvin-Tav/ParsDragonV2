using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.DTO.Visit
{
    public class VisitSiteDTO
    {
        public int VisitToday { get; set; }
        public int VisitYesterday { get; set; }
        public int VisitSum { get; set; }
    }
}
