using Learning.Application.Extentions;
using Learning.Application.Interfaces;
using Learning.Application.Utils;
using Learning.Domain.DTO.Blog;
using Learning.Domain.Interfaces;
using Learning.Domain.Models.Blog;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Application.Services
{
    public class BlogService : IBlogService
    {
        #region constuctor
        private IBlogRepository _blogRepository;
        public BlogService(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        #endregion
        #region blog


        public async Task AddBlog(Blog blog)
        {
            await _blogRepository.AddBlog(blog);
        }

        public async Task<BlogResult> CreateBlog(CreateBlogDTO blog, IFormFile imageBlog)
        {
            //check image and add image
            if (imageBlog == null) return BlogResult.NotImage;
            blog.BlogImageName = TextFixer.GenerateUniqCodeString(blog.Title) + Path.GetExtension(imageBlog.FileName);
            bool result = imageBlog
               .AddImageToServer(blog.BlogImageName, PathExtensions.BlogImageOrginServer, null, null, PathExtensions.BlogImageThumbServer);
            if (!result) return BlogResult.ErrorImage;


            //add blog
            await AddBlog(new Blog
            {
                BlogImageName = blog.BlogImageName,
                BodyHtml = blog.BodyHtml,
                Description = blog.Description,
                Tags = blog.Tags,
                Title = blog.Title,
                UserId = blog.UserId,
            });
            await _blogRepository.Save();
            return BlogResult.Success;
        }

        public async Task<DeleteBlogResult> DeleteBlog(int blogId)
        {
            var blog = await GetBlogById(blogId);
            if (blog == null) return DeleteBlogResult.NotFound;
            blog.IsDelete = true;
            blog.UpdateDate = DateTime.Now;
            _blogRepository.UpdateBlog(blog);
            await _blogRepository.Save();
            return DeleteBlogResult.Success;
        }

        public async Task<BlogResult> EditBlog(EditBlogDTO blog, IFormFile imageBlog)
        {
            var getBlog = await GetBlogById(blog.BlogId);
            if (getBlog == null) return BlogResult.Error;
            //check image
            if (imageBlog != null)
            {
                blog.BlogImageName = TextFixer.GenerateUniqCodeString(blog.Title) + Path.GetExtension(imageBlog.FileName);
                bool result = imageBlog
                   .AddImageToServer(blog.BlogImageName, PathExtensions.BlogImageOrginServer, null, null, PathExtensions.BlogImageThumbServer, getBlog.BlogImageName);
                if (!result) return BlogResult.ErrorImage;
            }

            //edit blog
            getBlog.BlogImageName = blog.BlogImageName;
            getBlog.BodyHtml = blog.BodyHtml;
            getBlog.Description = blog.Description;
            getBlog.Tags = blog.Tags;
            getBlog.Title = blog.Title;
            getBlog.UserId = blog.UserId;
            getBlog.UpdateDate = DateTime.Now;

            await _blogRepository.Save();
            return BlogResult.Success;
        }

        public async Task<FilterBlogDTO> FilterBlog(FilterBlogDTO filter)
        {
            return await _blogRepository.FilterBlog(filter);
        }

        public async Task<Blog> GetBlogById(int blogId)
        {
            return await _blogRepository.GetBlogById(blogId);
        }

        public async Task<EditBlogDTO> GetBlogForEdit(int blogId)
        {
            var blog = await _blogRepository.GetBlogById(blogId);
            if (blog == null) return null;

            return new EditBlogDTO()
            {
                BlogId = blog.BlogId,
                BlogImageName = blog.BlogImageName,
                BodyHtml = blog.BodyHtml,
                Description = blog.Description,
                Tags = blog.Tags,
                Title = blog.Title,
                UserId = blog.UserId,
            };
        }

        public async Task<List<Blog>> GetShowBlogsInSlider(int takeCount)
        {
            return await _blogRepository.GetShowBlogsByTake(takeCount);
        }

        #endregion

    }
}
