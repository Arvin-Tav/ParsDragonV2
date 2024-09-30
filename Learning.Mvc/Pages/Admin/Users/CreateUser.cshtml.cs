using Learning.Application.Interfaces;
using Learning.Application.Security;
using Learning.Domain.DTO.Account;
using Learning.Mvc.PresentationExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learning.Mvc.Pages.Admin.Users
{
    [PermissionChecker("Users")]
    public class CreateUserModel : AdminBaseModel
    {
        private readonly IPermissionService _permissionService;
        private readonly IUserService _userService;
        public CreateUserModel(IUserService userService, IPermissionService permissionService)
        {
            _userService = userService;
            _permissionService = permissionService;
        }
        [BindProperty]
        public CreateUserByAdminDTO CreateUserByAdminDTO { get; set; }
        public async Task OnGet()
        {
            if (await _permissionService.CheckPermission("Roles", User.GetUserId()))
            {
                ViewData["Roles"] = await _permissionService.GetRoles();
            }
            else
            {
                ViewData["Roles"] = await _permissionService.GetRolesJustAdmin();
            }

        }

        public async Task<IActionResult> OnPost(IFormFile imageUser, List<int> selectedRoles)
        {
            if (ModelState.IsValid)
            {
                CreateUserByAdminDTO.AdminId = User.GetUserId();
                var result = await _userService.CreateUserByAdmin(CreateUserByAdminDTO, imageUser, selectedRoles);
                switch (result)
                {
                    case CreateUserByAdminResut.Error:
                        TempData[ErrorMessage] = "خطا در ارتباط";
                        break;
                    case CreateUserByAdminResut.ErrorImage:
                        break;
                    case CreateUserByAdminResut.MobileExists:
                        ModelState.AddModelError("CreateUserByAdminDTO.Mobile", "موبایل تکراری می باشد");
                        TempData[ErrorMessage] = "موبایل تکراری می باشد";
                        break;
                    case CreateUserByAdminResut.EmailExists:
                        ModelState.AddModelError("CreateUserByAdminDTO.Email", "ایمیل تکراری می باشد");
                        TempData[ErrorMessage] = "ایمیل تکراری می باشد";
                        break;
                    case CreateUserByAdminResut.UsernameExists:
                        ModelState.AddModelError("CreateUserByAdminDTO.UserName", "نام کاربری  تکراری می باشد");
                        TempData[ErrorMessage] = "نام کاربری تکراری می باشد";
                        break;
                    case CreateUserByAdminResut.Success:
                        TempData[SuccessMessage] = "عملبات با موفقیت انجام شد";
                        return Redirect("/Admin/Users");
                }
            }
            if (await _permissionService.CheckPermission("Roles", User.GetUserId()))
            {
                ViewData["Roles"] = await _permissionService.GetRoles();
            }
            else
            {
                ViewData["Roles"] = await _permissionService.GetRolesJustAdmin();
            }
            return Page();
        }
    }
}
