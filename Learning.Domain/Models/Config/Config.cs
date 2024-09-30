using Learning.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.Models.Config
{
    public class Config : BaseEntity
    {
        [Key]
        [StringLength(128)]
        [MaxLength(150)]
        public string Key { get; set; }
        [MaxLength(150)]
        public string Value { get; set; }
    }
}
