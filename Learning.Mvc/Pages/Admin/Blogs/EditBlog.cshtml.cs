using Learning.Application.Interfaces;
using Learning.Application.Security;
using Learning.Domain.DTO.Blog;
using Learning.Mvc.PresentationExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Learning.Mvc.Pages.Admin.Blogs
{
    [PermissionChecker("Blogs")]

    public class EditBlogModel : AdminBaseModel
    {
        private readonly IBlogService _blogService;
        public EditBlogModel(IBlogService blogService)
        {
            _blogService = blogService;
        }
        [BindProperty]
        public EditBlogDTO EditBlogDTO { get; set; }
        public async Task OnGet(int id)
        {
            EditBlogDTO = await _blogService.GetBlogForEdit(id);
        }
        public async Task<IActionResult> OnPost(IFormFile imageBlog)
        {
            if (ModelState.IsValid)
            {
                EditBlogDTO.UserId = User.GetUserId();
                var result = await _blogService.EditBlog(EditBlogDTO, imageBlog);
                switch (result)
                {
                    case BlogResult.Error:
                        TempData[ErrorMessage] = "خطا در ارتباط";
                        break;
                    case BlogResult.NotImage:
                        TempData[ErrorMessage] = "عکس را آپلود کنید";
                        break;
                    case BlogResult.ErrorImage:
                        TempData[ErrorMessage] = "پسوند عکس مشکل دارد لطفا عکس انتخاب کنید";
                        break;
                    case BlogResult.Success:
                        TempData[SuccessMessage] = "عملبات با موفقیت انجام شد";
                        return RedirectToPage("Index");
                }
            }
            return Page();
        }
    }
}
