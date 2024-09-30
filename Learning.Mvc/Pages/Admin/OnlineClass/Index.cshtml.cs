using Learning.Application.Interfaces;
using Learning.Application.Security;
using Learning.Domain.DTO.OnlineClass;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Learning.Mvc.Pages.Admin.OnlineClass
{
    [PermissionChecker("AccessToOnlineClass")]
    public class IndexModel : PageModel
    {
        private IOnlineClassService _onlineClassService;

        public IndexModel(IOnlineClassService onlineClassService)
        {
            _onlineClassService = onlineClassService;
        }

        public FilterOnlineClassDTO FilterOnlineClassDTO { get; set; }

        public async Task OnGet(FilterOnlineClassDTO filter)
        {
            FilterOnlineClassDTO = await _onlineClassService.FilterOnlineClass(filter);
        }


        public async Task<IActionResult> OnGetIsShow(int id)
        {
            await _onlineClassService.IsShowOnlineClass(id);
            return RedirectToPage("Index");
        }
        public async Task<IActionResult> OnGetDeleteOnlineClass(int code)
        {
            var onlineClass = await _onlineClassService.GetOnlineClassById(code);
            if (onlineClass == null)
            {
                return Content("false");
            }
            else
            {
                onlineClass.IsDelete = true;
                await _onlineClassService.UpdateOnlineClass(onlineClass);
                return Content("true");
            }

        }


        public async Task<IActionResult> OnGetIsShowEpisode(int onlineClassId, int eposideId)
        {
            await _onlineClassService.IsShowOnlineClassEpisode(eposideId);
            return Redirect("/Admin/OnlineClassEpisode/Index/" + onlineClassId);
        }
        public async Task<IActionResult> OnGetDeleteEpisodeOnlineClass(int code)
        {
            var episode =await _onlineClassService.GetOnlineClassEpisodeById(code);
            if (episode == null)
            {
                return Content("false");
            }
            else
            {
                episode.IsDelete = true;
               await _onlineClassService.UpdateOnlineClassEpisode(episode);
                return Content("true");
            }

        }

    }
}