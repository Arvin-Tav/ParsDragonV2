using Learning.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Learning.Mvc.ViewComponents.Admin
{
    public class VisitSiteAdminViewComponent : ViewComponent
    {
        private readonly IVisitService _visitService;
        public VisitSiteAdminViewComponent(IVisitService visitService)
        {
            _visitService = visitService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("VisitSiteAdmin", await _visitService.GetAllVisitSilte()));
        }
    }
}
