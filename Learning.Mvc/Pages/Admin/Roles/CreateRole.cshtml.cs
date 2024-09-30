using Learning.Application.Interfaces;
using Learning.Application.Security;
using Learning.Domain.DTO.Role;
using Learning.Domain.Models.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learning.Mvc.Pages.Admin.Roles
{
    [PermissionChecker("Roles")]
    public class CreateRoleModel : AdminBaseModel
    {
        private IPermissionService _permissionService;

        public CreateRoleModel(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }


        [BindProperty]
        public CreateRoleDTO CreateRoleDTO { get; set; }

        public async Task OnGet()
        {
            ViewData["Permission"] = await _permissionService.GetAllPermission();
        }

        public async Task<IActionResult> OnPost(List<int> SelectedPermission)
        {
            ViewData["Permission"] = await _permissionService.GetAllPermission();

            if (ModelState.IsValid)
            {
                CreateRoleDTO.SelectedPermission = SelectedPermission;
                var result = await _permissionService.CreateRolePermission(CreateRoleDTO);
                switch (result)
                {
                    case RoleResult.NotFound:
                        break;
                    case RoleResult.ErrorRoleTitleName:
                        ModelState.AddModelError("Role.RoleTitle", "عنوان نقش تکراری میباشد");
                        TempData[ErrorMessage] = "عنوان نقش تکراری میباشد";
                        break;
                    case RoleResult.Success:
                        TempData[SuccessMessage] = "عملبات با موفقیت انجام شد";
                        return RedirectToPage("Index");
                }
            }
            return Page();

        }
    }
}