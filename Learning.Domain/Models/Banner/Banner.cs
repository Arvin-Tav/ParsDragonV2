using Learning.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.Models.Banner
{
    public class Banner: BaseEntity

    {
        [Key]
        public int BannerId { get; set; }
        [Required(ErrorMessage = "نام اسلایدر الزامی میباشد")]
        [StringLength(100, ErrorMessage = "نام اسلایدر مناسب وارد کنید")]
        [MaxLength(150)]
        public string Name { get; set; }
        [MaxLength(150)]
        public string BannerImageName { get; set; }
        [MaxLength(150)]
        [Required(ErrorMessage = "لینک اسلایدر الزامی میباشد")]
        [StringLength(200, ErrorMessage = "لینک اسلایدر مناسب وارد کنید")]
        public string Link { get; set; }
    }
}
