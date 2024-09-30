using Learning.Application.Utils;
using Learning.Domain.DTO.Banner;
using Learning.Domain.Models.Banner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Application.EntitiesExtensions
{
    public static class BannerExtensions
    {
        public static string GetBannerImageAddress(this Banner blog)
        {
            return PathExtensions.BannerImageOrgin + blog.BannerImageName;
        }
        public static string GetBannerImageThumbAddress(this Banner blog)
        {
            return PathExtensions.BannerImageThumb + blog.BannerImageName;
        }
        public static string GetBannerImageAddress(this EditBannerDTO blog)
        {
            return PathExtensions.BannerImageOrgin + blog.BannerImageName;
        }
        public static string GetBannerImageThumbAddress(this EditBannerDTO blog)
        {
            return PathExtensions.BannerImageThumb + blog.BannerImageName;
        }
    }
}
