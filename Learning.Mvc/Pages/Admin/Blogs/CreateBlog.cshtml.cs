using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Learning.Application.Interfaces;
using Learning.Application.Security;
using Learning.Domain.DTO.Blog;
using Learning.Mvc.PresentationExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Learning.Mvc.Pages.Admin.Blogs
{
    [PermissionChecker("Blogs")]
    public class CreateBlogModel : AdminBaseModel
    {
        private readonly IBlogService _blogService;
        public CreateBlogModel(IBlogService blogService)
        {
            _blogService = blogService;
        }
        [BindProperty]
        public CreateBlogDTO CreateBlogDTO { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost(IFormFile imageBlog)
        {
            if (ModelState.IsValid)
            {
                CreateBlogDTO.UserId = User.GetUserId();
                var result = await _blogService.CreateBlog(CreateBlogDTO, imageBlog);
                switch (result)
                {
                    case BlogResult.Error:
                        TempData[ErrorMessage] = "خطا در ارتباط";
                        break;
                    case BlogResult.NotImage:
                        TempData[WarningMessage] = "عکس را آپلود کنید";
                        break;
                    case BlogResult.ErrorImage:
                        TempData[WarningMessage] = "پسوند عکس مشکل دارد لطفا عکس انتخاب کنید";
                        break;
                    case BlogResult.Success:
                        TempData[SuccessMessage] = "عملبات با موفقیت انجام شد";
                        return Redirect("/Admin/Blogs");
                }
            }
            return Page();

        }
    }
}
