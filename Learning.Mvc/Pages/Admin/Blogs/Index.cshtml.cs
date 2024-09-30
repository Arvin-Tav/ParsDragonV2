
using Learning.Application.Interfaces;
using Learning.Application.Security;
using Learning.Domain.DTO.Blog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Learning.Mvc.Pages.Admin.Blogs
{
    [PermissionChecker("Blogs")]
    public class IndexModel : AdminBaseModel
    {
        private readonly IBlogService _blogService;
        public IndexModel(IBlogService blogService)
        {
            _blogService = blogService;
        }
        public FilterBlogDTO FilterBlogDTO { get; set; }
        public async Task OnGet(FilterBlogDTO filter)
        {
            FilterBlogDTO = await _blogService.FilterBlog(filter);
        }
        //admin/Blogs/Index?handler=checkcode
        //admin/Blogs/Index/checkcode
        public async Task<IActionResult> OnGetDeleteBlog(int code)
        {
            var result = await _blogService.DeleteBlog(code);
            switch (result)
            {
                case DeleteBlogResult.NotFound:
                    break;
                case DeleteBlogResult.Success:
                    return Content("true");
            }
            return Content("false");

        }

    }
}
