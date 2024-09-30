using Learning.Application.Interfaces;
using Learning.Application.Security;
using Learning.Domain.DTO.Forum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Learning.Mvc.Pages.Admin.Question
{
    [PermissionChecker("Courses")]
    public class QuestionsModel : PageModel
    {
        private readonly IForumService _forumService;

        public QuestionsModel(IForumService forumService)
        {
            _forumService = forumService;
        }
        public FilterQuestionDTO FilterQuestionDTO { get; set; }
        public async Task OnGet(FilterQuestionDTO filter)
        {
            FilterQuestionDTO =await _forumService.FilterQuestion(filter);
        }
        public async Task<IActionResult> OnGetIsReadAdminQuestion(int id)
        {
            await _forumService.IsAdminReadQuestion(id);
            return Redirect("/Forum/ShowQuestion/" + id);

        }
        public async Task<IActionResult> OnGetDeleteQuestion(int code)
        {
            var result = await _forumService.DeleteQuestion(code);
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
