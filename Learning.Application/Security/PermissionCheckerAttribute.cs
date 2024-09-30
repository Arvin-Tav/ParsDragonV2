using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Learning.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Learning.Application.Security
{
    public class PermissionCheckerAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private IPermissionService _permissionService;
        private string _permissionName = "";
        public PermissionCheckerAttribute(string permissionName)
        {
            _permissionName = permissionName;
        }

        public async void OnAuthorization(AuthorizationFilterContext context)
        {
            _permissionService =
               (IPermissionService)context.HttpContext.RequestServices.GetService(typeof(IPermissionService));
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                int userId = Convert.ToInt32(context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

                if (!await _permissionService.CheckPermission(_permissionName, userId))
                {
                    context.Result = new RedirectResult("/Login" + context.HttpContext.Request.Path);
                }

            }
            else
            {
                context.Result = new RedirectResult("/Login");
            }
        }
    }
}
