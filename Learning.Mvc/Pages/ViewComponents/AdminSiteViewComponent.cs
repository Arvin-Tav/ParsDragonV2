using Learning.Application.Interfaces;
using Learning.Mvc.PresentationExtensions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Learning.Mvc.Pages.ViewComponents
{
    public class MainMenuAdminViewComponent : ViewComponent
    {
        private readonly IPermissionService _permissionService;
        public MainMenuAdminViewComponent(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("MainMenuAdmin",
                await _permissionService.GetPermissionNamesByUserId(User.GetUserId())));
        }
    }
}
