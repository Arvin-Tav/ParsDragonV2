using Learning.Application.Interfaces;
using Learning.Application.Security;
using Learning.Domain.DTO.Course;
using Learning.Domain.DTO.CourseEpisode;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Learning.Mvc.Pages.Admin.Courses
{
    [PermissionChecker("Courses")]
    public class EditEpisodeModel : AdminBaseModel
    {
        private IEpisodeService _episodeService;
        public EditEpisodeModel(IEpisodeService episodeService)
        {
            _episodeService = episodeService;
        }

        [BindProperty]
        public EditCourseEpisodeDTO EditCourseEpisodeDTO { get; set; }
        public async Task OnGet(int id)
        {
            EditCourseEpisodeDTO = await _episodeService.GetEpisodeByIdForEdit(id);
        }

        public async Task<IActionResult> OnPost(IFormFile fileEpisode)
        {
            if (ModelState.IsValid)
            {
                var result = await _episodeService.EditCourseEpisode(EditCourseEpisodeDTO, fileEpisode);
                switch (result)
                {
                    case CourseEpisodeResut.Error:
                        TempData[ErrorMessage] = "خطا در ارتباط";
                        break;
                    case CourseEpisodeResut.NotFound:
                        TempData[ErrorMessage] = "وردی یافت نشد";
                        break;
                    case CourseEpisodeResut.NotFile:
                        TempData[ErrorMessage] = "فایلی وحود ندارد";
                        break;
                    case CourseEpisodeResut.ExistFile:
                        TempData[ErrorMessage] = "نام فایل تکراریست";
                        break;
                    case CourseEpisodeResut.ErrorFile:
                        TempData[ErrorMessage] = "خطا در آپلود فایل لطفا دقت کنید";
                        break;
                    case CourseEpisodeResut.Success:
                        TempData[SuccessMessage] = "عملبات با موفقیت انجام شد";
                        return Redirect("/Admin/Courses/IndexEpisode/" + EditCourseEpisodeDTO.CourseId);
                }
            }
            return Page();
        }
    }
}