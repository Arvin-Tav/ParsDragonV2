using Learning.Application.Interfaces;
using Learning.Application.Security;
using Learning.Domain.Models.Course;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learning.Mvc.Pages.Admin.Courses
{
    [PermissionChecker("Courses")]

    public class IndexEpisodeModel : PageModel
    {
        private readonly IEpisodeService _episodeService;

        public IndexEpisodeModel(IEpisodeService episodeService)
        {
            _episodeService = episodeService;
        }
        public List<CourseEpisode> CourseEpisodes { get; set; }
        public async Task OnGet(int id)
        {
            ViewData["CourseId"] = id;
            CourseEpisodes =await _episodeService.GetListEpisodeCourse(id);
        }
     
    }
}