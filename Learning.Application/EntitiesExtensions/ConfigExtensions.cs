using Learning.Application.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Application.EntitiesExtensions
{
    public static class ConfigExtensions
    {
        public static string GetConfigImageAddress(this string image)
        {
            return PathExtensions.ConfigImageOrgin + image;
        }
    }
}
