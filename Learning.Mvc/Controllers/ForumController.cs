using Learning.Application.Interfaces;
using Learning.Application.Utils;
using Learning.Domain.DTO.Forum;
using Learning.Mvc.PresentationExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Learning.Mvc.Controllers
{
    public class ForumController : SiteBaseController
    {
        #region constructor
        private readonly IForumService _forumService;
        private readonly IUserService _userService;
        private readonly IPermissionService _permissionService;
        public ForumController(IForumService forumService, IUserService userService, IPermissionService permissionService)
        {
            _forumService = forumService;
            _userService = userService;
            _permissionService = permissionService;
        }

        #endregion
        [Authorize]
        [HttpGet("questions")]
        public async Task<IActionResult> Index(FilterQuestionDTO filter)
        {
            return View(await _forumService.FilterQuestion(filter));
        }


        #region CreateQuestion
        [Authorize]
        [HttpGet("questions/createQuestion/{id}")]
        public async Task<IActionResult> CreateQuestion(int id)
        {
            try
            {
                if (id == 0)
                {
                    return await Task.FromResult(Redirect("/Home/Error404"));
                }
                return await Task.FromResult(View(new CreateQuestionDTO()
                {
                    CourseId = id,
                }));
            }
            catch
            {
                return await Task.FromResult(Redirect("/Home/Error404"));
            }
        }
        [HttpPost("questions/createQuestion/{id}"), ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> CreateQuestion(CreateQuestionDTO question, IFormCollection form)
        {
            try
            {
                //version 2 reCaptcha
                if (!MethodRepo.CheckRechapcha(form))
                {
                    ModelState.AddModelError("Captcha", "اعتبار سنجی captcha نا موفق بود! ");
                    return View(question);
                }

                if (ModelState.IsValid)
                {
                    question.UserId = User.GetUserId();
                    var result = await _forumService.CreateQuestion(question);
                    switch (result)
                    {
                        case CreateForumResult.NotFound:
                            TempData[WarningMessage] = "مورد یافت نشد";
                            break;
                        case CreateForumResult.Success:
                            TempData[SuccessMessage] = "عملیات با موفقیت انجام شد";
                            return RedirectToAction("Index" , new { courseId = question.CourseId });
                    }
                }
                return View(question);
            }
            catch
            {
                return await Task.FromResult(Redirect("/Home/Error404"));
            }

        }
        #endregion


        #region ShowQuestion
        [Authorize]
        [Route("/ShowQuestion/{id}/{name?}")]
        public async Task<IActionResult> ShowQuestion(int id, string name = null)
        {

            try
            {
                ShowQuestionByAnswersDTO question = await _forumService.ShowQuestion(id);
                if (id == 0 || question.Question == null)
                {
                    return Redirect("/Forum");
                }
                ViewBag.ShowQuestions = question;

                return await Task.FromResult(View());
            }
            catch
            {
                return await Task.FromResult(Redirect("/Home/Error404"));

            }
        }
        #endregion



        #region Answer
        [Authorize]
        [HttpPost, ValidateAntiForgeryToken]
        [Route("/ShowQuestion/{id}/{name?}")]
        public async Task<IActionResult> ShowQuestion(CreateAnswerDTO createAnswer, IFormCollection form)
        {


            //version 2 reCaptcha
            if (!MethodRepo.CheckRechapcha(form))
            {
                ModelState.AddModelError("Captcha", "اعتبار سنجی captcha نا موفق بود! ");
            }
            if (ModelState.IsValid)
            {
                createAnswer.UserId = User.GetUserId();
                //var sanitizer = new HtmlSanitizer();
                // body = sanitizer.Sanitize(body);
                var result = await _forumService.CreateAnswer(createAnswer);
                switch (result)
                {
                    case CreateForumResult.NotFound:
                        TempData[WarningMessage] = "مورد یافت نشد";
                        break;
                    case CreateForumResult.Success:
                        TempData[SuccessMessage] = "عملیات با موفقیت انجام شد";
                        return RedirectToAction("ShowQuestion", new { id = createAnswer.QuestionId });
                }

            }
            ShowQuestionByAnswersDTO question = await _forumService.ShowQuestion(createAnswer.QuestionId);
            ViewBag.ShowQuestions = question;
            return View(createAnswer);

        }
        [Authorize]
        public async Task<IActionResult> DeleteQuestion(int id, int courseId)
        {
            var result = await _forumService.DeleteQuestionByUser(id, User.GetUserId());
            switch (result)
            {
                case DeleteForumResult.NotFound:
                    TempData[WarningSwalMessage] = "موردی یافت نشد";
                    break;
                case DeleteForumResult.Success:
                    TempData[SuccessSwalMessage] = "عملیات با موفقیت انجام شد";
                    return RedirectToAction("Index", new { courseId = courseId });
            }
            return RedirectToAction("Index");

        }
        [Authorize]
        public async Task<IActionResult> DeleteAnswer(int id, int questionId)
        {
            var result = await _forumService.DeleteAnswerByUser(id, questionId, User.GetUserId());
            switch (result)
            {
                case DeleteForumResult.NotFound:
                    TempData[WarningSwalMessage] = "موردی یافت نشد";
                    break;
                case DeleteForumResult.Success:
                    TempData[SuccessSwalMessage] = "عملیات با موفقیت انجام شد";
                    return Redirect("/ShowQuestion/" + questionId);
            }
            return RedirectToAction("Index");
        }
        //[Authorize]
        //public IActionResult SelectIsTrueAnswer(int questionId, int answerId)
        //{
        //    int currentUserId = _userService.GetUserIdByUserName(User.Identity.Name);
        //    var question = _forumService.ShowQuestion(questionId);
        //    if (question.Question.UserId == currentUserId)
        //    {
        //        _forumService.ChangeIsTrueAnswer(questionId, answerId);
        //    }
        //    return RedirectToAction("ShowQuestion", new { id = questionId });
        //}
        #endregion
    }
}
