using Learning.Application.Interfaces;
using Learning.Domain.DTO.Permission;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Learning.Mvc.Controllers
{
    public class MasterController : SiteBaseController
    {
        private readonly IPermissionService _permissionService;
        private readonly ICourseService _courseService;
        public MasterController(IPermissionService permissionService, ICourseService courseService)
        {
            _permissionService = permissionService;
            _courseService = courseService;
        }
        [Route("Masters")]
        public async Task<IActionResult> Masters(FilterMasterDTO filter)
        {
            return View(await _permissionService.FilterMaster(filter));
        }


        [Route("InfoMaster")]
        public async Task<IActionResult> InfoMaster(string name)
        {
            var result =await _permissionService.GetInfoMaster(name);
            if (result == null)
            {
                return NotFound();
            }
            return View(result);
        }
    }
}
