using Learning.Application.Interfaces;
using Learning.Application.Security;
using Learning.Domain.DTO.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learning.Mvc.Pages.Admin.Roles
{
    [PermissionChecker("Roles")]
    public class EditRoleModel : AdminBaseModel
    {
        private IPermissionService _permissionService;

        public EditRoleModel(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [BindProperty]
        public EditRoleDTO EditRoleDTO { get; set; }
        public async Task OnGet(int id)
        {
            EditRoleDTO = await _permissionService.GetRoleForEdit(id);
            ViewData["Permission"] = await _permissionService.GetAllPermission();
        }

        public async Task<IActionResult> OnPost(List<int> SelectedPermission)
        {
            ViewData["Permission"] =await _permissionService.GetAllPermission();
            if (ModelState.IsValid)
            {
                EditRoleDTO.SelectedPermission = SelectedPermission;
                var result = await _permissionService.EditRolePermission(EditRoleDTO);
                switch (result)
                {
                    case RoleResult.NotFound:
                        TempData[ErrorMessage] = " موردی یافت نشد ";
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