using Learning.Application.Utils;
using Learning.Domain.DTO.Blog;
using Learning.Domain.Models.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Application.EntitiesExtensions
{
    public static class BlogExtensions
    {
        public static string GetBlogImageAddress(this Blog blog)
        {
            return PathExtensions.BlogImageThumb + blog.BlogImageName;
        }
        public static string GetBlogImageThumbAddress(this Blog blog)
        {
            return PathExtensions.BlogImageThumb + blog.BlogImageName;
        }
        public static string GetBlogImageAddress(this EditBlogDTO blog)
        {
            return PathExtensions.BlogImageOrgin + blog.BlogImageName;
        }
        public static string GetBlogImageThumbAddress(this EditBlogDTO blog)
        {
            return PathExtensions.BlogImageThumb + blog.BlogImageName;
        }
    }
}
