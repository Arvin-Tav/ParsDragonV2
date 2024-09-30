using Learning.Application.Interfaces;
using Learning.Application.Security;
using Learning.Domain.DTO.Account;
using Learning.Domain.DTO.PublicEnum;
using Learning.Mvc.PresentationExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Learning.Mvc.Pages.Admin.Users
{
    [PermissionChecker("Users")]
    public class IndexModel : PageModel
    {
        private IUserService _userService;
        public IndexModel(IUserService userService)
        {
            _userService = userService;

        }
        public FilterUserDTO FilterUserDTO { get; set; }
        public async Task OnGet(FilterUserDTO filter)
        {
            filter.UserIdLogin = User.GetUserId();
            FilterUserDTO = await _userService.FilterUsers(filter);
        }

        public async Task<IActionResult> OnGetDeleteUser(int code)
        {
            var result = await _userService.DeleteUser(code);
            switch (result)
            {
                case DeleteResult.NotFound:
                    break;
                case DeleteResult.Success:
                    return Content("true");
            }
            return Content("false");


        }
        public async Task<IActionResult> OnGetIsActiveUser(int id)
        {
            var result = await _userService.IsActive(id);
            switch (result)
            {
                case ActiveResult.NotFound:
                    break;
                case ActiveResult.Success:
                    return RedirectToPage("Index");
            }
            return Content("false");

        }
    }
}
