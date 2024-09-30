using Learning.Domain.DTO.Blog;
using Learning.Domain.Models.Blog;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Application.Interfaces
{
    public interface IBlogService
    {
        Task<FilterBlogDTO> FilterBlog(FilterBlogDTO filter);
        Task AddBlog(Blog blog);
        Task<BlogResult> CreateBlog(CreateBlogDTO blog, IFormFile imageBlog);
        Task<List<Blog>> GetShowBlogsInSlider(int takeCount);
        Task<EditBlogDTO> GetBlogForEdit(int blogId);
        Task<Blog> GetBlogById(int blogId);
        Task<BlogResult> EditBlog(EditBlogDTO blog, IFormFile imageBlog);
        Task<DeleteBlogResult> DeleteBlog(int blogId);
    }
}
