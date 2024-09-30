using Learning.Application.Interfaces;
using Learning.Application.Security;
using Learning.Domain.DTO.Forum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Learning.Mvc.Pages.Admin.Question
{
    [PermissionChecker("Courses")]
    public class AnswersModel : PageModel
    {
        private readonly IForumService _forumService;

        public AnswersModel(IForumService forumService)
        {
            _forumService = forumService;
        }
        public FilterAnswerDTO FilterAnswerDTO { get; set; }
        public async Task OnGet(FilterAnswerDTO filter)
        {
            FilterAnswerDTO =await _forumService.FilterAnswer(filter);
        }
        public async Task<IActionResult> OnGetIsReadAdminAnswer(int id,int answerId)
        {
            await _forumService.IsAdminReadAnswer(answerId);
            return Redirect("/Forum/ShowQuestion/" + id);
        }
        public async Task<IActionResult> OnGetDeleteAnswer(int code)
        {
            var result = await _forumService.DeleteAnswer(code);
            switch (result)
            {
                case DeleteForumResult.NotFound:
                    break;
                case DeleteForumResult.Success:
                    return Content("true");
            }
            return Content("false");

        }
    }
}
