using Learning.Application.Interfaces;
using Learning.Application.Security;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learning.Mvc.Pages.Admin.OnlineClassEpisode
{
    [PermissionChecker("AccessToOnlineClass")]
    public class IndexModel : PageModel
    {
        private readonly IOnlineClassService _onlineClassService;
        public IndexModel(IOnlineClassService onlineClassService)
        {
            _onlineClassService = onlineClassService;
        }
        public List<Learning.Domain.Models.OnlineClass.OnlineClassEpisode> OnlineClassEpisodes { get; set; }

        public async Task OnGet(int id)
        {
            ViewData["OnlineClassEpisodeId"] = id;
            OnlineClassEpisodes =await _onlineClassService.GetOnlineClassEpisodeByOnlineClassId(id);
        }


        

    }
}
