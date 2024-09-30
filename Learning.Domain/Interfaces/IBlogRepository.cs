using Learning.Domain.DTO.Blog;
using Learning.Domain.Models.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.Interfaces
{
    public interface IBlogRepository
    {
        Task<FilterBlogDTO> FilterBlog(FilterBlogDTO filter); 
        Task AddBlog(Blog blog);
        Task<List<Blog>> GetShowBlogsByTake(int takeCount);
        Task<Blog> GetBlogById(int blogId);
        void UpdateBlog(Blog blog);
        Task Save();

    }
}
