using Learning.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.Models.Visit
{
    public class Visit : BaseEntity
    {
        [Key]
        public int VisitId { get; set; }
        [StringLength(25)]
        [MaxLength(150)]
        public string Ip { get; set; }
        public DateTime Date { get; set; }
    }
}
