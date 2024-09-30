using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.DTO.Slider
{
    public class EditSliderDTO
    {
        public int SliderId { get; set; }
        [Required(ErrorMessage = "نام اسلایدر الزامی میباشد")]
        [StringLength(100, ErrorMessage = "نام اسلایدر مناسب وارد کنید")]
        public string Name { get; set; }
        public string SliderImageName { get; set; }
        [Required(ErrorMessage = "گروه الزامی میباشد")]
        public int GroupId { get; set; }
        public int? SubGroup { get; set; }
        public bool IsOnlineClass { get; set; }
    }
}
