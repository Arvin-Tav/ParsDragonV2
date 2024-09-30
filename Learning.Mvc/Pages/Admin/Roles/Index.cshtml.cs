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
    public class IndexModel : PageModel
    {
        private IPermissionService _permissionService;

        public IndexModel(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public List<Role> RolesList { get; set; }


        public async Task OnGet()
        {
            RolesList =await _permissionService.GetRoles();
        }


        public async Task<IActionResult> OnGetDeleteRole(int code)
        {
            try
            {
                var result = await _permissionService.DeleteRole(code);
                switch (result)
                {
                    case DeleteRoleResult.NotFound:
                        break;
                    case DeleteRoleResult.Success:
                        return Content("true");
                }
                return Content("false");
            }
            catch
            {
                return Content("false");
            }
        }
    }
}