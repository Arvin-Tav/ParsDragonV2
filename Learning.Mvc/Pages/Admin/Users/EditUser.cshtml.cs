using Learning.Application.Interfaces;
using Learning.Application.Security;
using Learning.Domain.DTO.Account;
using Learning.Mvc.Pages.Admin;
using Learning.Mvc.PresentationExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learning.Mvc.Pages.Admin.Users
{
    [PermissionChecker("Users")]
    public class EditUserModel : AdminBaseModel
    {
        private readonly IPermissionService _permissionService;
        private readonly IUserService _userService;
        public EditUserModel(IUserService userService, IPermissionService permissionService)
        {
            _userService = userService;
            _permissionService = permissionService;
        }
        [BindProperty]
        public EditUserByAdminDTO EditUserByAdminDTO { get; set; }
        public async Task OnGet(int id)
        {
            EditUserByAdminDTO = await _userService.GeUserForEditByAdmin(id);
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
                EditUserByAdminDTO.AdminId = User.GetUserId();
                var result = await _userService.EditUserByAdmin(EditUserByAdminDTO, imageUser, selectedRoles);
                switch (result)
                {
                    case EditUserByAdminResut.Error:
                        TempData[ErrorMessage] = "خطا در ارتباط";
                        break;
                    case EditUserByAdminResut.ErrorImage:
                        break;
                    case EditUserByAdminResut.MobileExists:
                        ModelState.AddModelError("CreateUserViewModel.Mobile", "موبایل تکراری می باشد");
                        TempData[ErrorMessage] = "موبایل تکراری می باشد";
                        break;
                    case EditUserByAdminResut.EmailExists:
                        ModelState.AddModelError("CreateUserViewModel.Email", "ایمیل تکراری می باشد");
                        TempData[ErrorMessage] = "ایمیل تکراری می باشد";
                        break;
                    case EditUserByAdminResut.UsernameExists:
                        ModelState.AddModelError("CreateUserViewModel.UserName", "نام کاربری  تکراری می باشد");
                        TempData[ErrorMessage] = "نام کاربری تکراری می باشد";
                        break;
                    case EditUserByAdminResut.Success:
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
