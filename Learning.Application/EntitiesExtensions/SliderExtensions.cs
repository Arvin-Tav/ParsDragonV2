using Learning.Application.Utils;
using Learning.Domain.DTO.Slider;
using Learning.Domain.Models.Banner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Application.EntitiesExtensions
{
    public static class SliderExtensions
    {
        public static string GetSliderImageAddress(this Slider slider)
        {
            return PathExtensions.SliderImageOrgin + slider.SliderImageName;
        }
        public static string GetSliderImageThumbAddress(this Slider slider)
        {
            return PathExtensions.SliderImageThumb + slider.SliderImageName;
        }
        public static string GetSliderImageAddress(this EditSliderDTO slider)
        {
            return PathExtensions.SliderImageOrgin + slider.SliderImageName;
        }
        public static string GetSliderImageThumbAddress(this EditSliderDTO slider)
        {
            return PathExtensions.SliderImageThumb + slider.SliderImageName;
        }
    }
}
