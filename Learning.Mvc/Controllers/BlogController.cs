using Learning.Application.Interfaces;
using Learning.Domain.DTO.Blog;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Learning.Mvc.Controllers
{
    public class BlogController : SiteBaseController
    {
        private readonly IBlogService _blogService;
        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }
        [HttpGet("blogs")]
        public async Task<IActionResult> Index(FilterBlogDTO filter)
        {
            filter.TakeEntity = 12;
            return await Task.FromResult(View(await _blogService.FilterBlog(filter)));

        }

        [Route("blog/{id}/{name?}")]
        public async Task<IActionResult> Blog(int id, string name = "")
        {
            var result =await _blogService.GetBlogById(id);
            if (result == null) return NotFound();
            return await Task.FromResult(View(result));
        }
    }
}
