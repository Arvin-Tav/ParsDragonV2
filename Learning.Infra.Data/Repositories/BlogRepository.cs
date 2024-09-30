using Learning.Domain.DTO.Blog;
using Learning.Domain.DTO.Paging;
using Learning.Domain.Interfaces;
using Learning.Domain.Models.Blog;
using Learning.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Infra.Data.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        #region constructor
        private readonly LearningContext _context;
        public BlogRepository(LearningContext context)
        {
            _context = context;
        }
        #endregion

        #region blog

        public async Task<FilterBlogDTO> FilterBlog(FilterBlogDTO filter)
        {
            IQueryable<Blog> query = _context.Blogs.AsQueryable().Include(i=>i.User);

            #region state
            switch (filter.FilterBlogOrder)
            {
                case FilterBlogOrder.CreateDate_DES:
                    query = query.OrderByDescending(i => i.CreateDate);
                    break;
                case FilterBlogOrder.CreateDate_ASC:
                    query = query.OrderBy(i => i.CreateDate);
                    break;
            }
            #endregion
            #region filter
            if (!string.IsNullOrEmpty(filter.Search))
                query = query.Where(i => EF.Functions.Like(i.Tags, $"%{filter.Search}%")
                || EF.Functions.Like(i.Title, $"%{filter.Search}%")
                || EF.Functions.Like(i.Description, $"%{filter.Search}%"));
            #endregion
            #region paging
            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.HowManyShowPageAfterAndBefore);
            var allEntities = await query.Paging(pager).ToListAsync();
            #endregion
            return filter.SetPaging(pager).SetBlog(allEntities);
        }
        public async Task AddBlog(Blog blog)
        {
            await _context.Blogs.AddAsync(blog);
        }
        public async Task<Blog> GetBlogById(int blogId)
        {
            return await _context.Blogs.AsQueryable()
                .Include(u => u.User)
                .FirstOrDefaultAsync(i => i.BlogId == blogId && !i.IsDelete);
        }

        public async Task<List<Blog>> GetShowBlogsByTake(int takeCount)
        {
            return await _context.Blogs
                .AsQueryable()
                .Include(u=>u.User)
                .Where(i => !i.IsDelete)
                .OrderByDescending(i => i.CreateDate)
                .Take(takeCount)
                .ToListAsync();
        }

        public void UpdateBlog(Blog blog)
        {
            _context.Blogs.Update(blog);
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        #endregion


    }
}
