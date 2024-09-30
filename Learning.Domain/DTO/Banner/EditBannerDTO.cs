using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.DTO.Banner
{
    public class EditBannerDTO
    {
        public int BannerId { get; set; }
        [Required(ErrorMessage = "نام اسلایدر الزامی میباشد")]
        [StringLength(100, ErrorMessage = "نام اسلایدر مناسب وارد کنید")]
        public string Name { get; set; }
        public string BannerImageName { get; set; }
        [Required(ErrorMessage = "لینک اسلایدر الزامی میباشد")]
        [StringLength(200, ErrorMessage = "لینک اسلایدر مناسب وارد کنید")]
        public string Link { get; set; }
    }

   
}
