using Learning.Application.Interfaces;
using Learning.Domain.DTO.UserPanel;
using Learning.Mvc.Controllers;
using Learning.Mvc.PresentationExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp.Metadata.Profiles.Exif;
using System.Threading.Tasks;

namespace Learning.Mvc.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class HomeController : SiteBaseController
    {
        private readonly IUserService _userService;
        public HomeController(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _userService.GetUserInformation(User.GetUserId()));
        }
        [HttpPost]
        [Route("UserPanel/ChangeAvatar")]
        public async Task<IActionResult> ChangeAvatarAsync(IFormFile avatar)
        {
            try
            {
                if (avatar != null && avatar.Length < 100000)
                {
                    var result = await _userService.ChangeAvatar(User.GetUserId(), avatar);
                    switch (result)
                    {
                        case UserPanelResult.NotFound:
                            TempData[ErrorMessage] = "موردی یافت نشد";
                            break;
                        case UserPanelResult.Error:
                            TempData[WarningMessage] = "عکس مورد نظر آپلود نشد";
                            break;
                        case UserPanelResult.Success:
                            TempData[SuccessMessage] = "عملیات با موفقیت انجام شد";
                            break;
                    }
                }
                return await Task.FromResult(Redirect("/UserPanel"));
            }
            catch
            {
                return await Task.FromResult(Redirect("/Home/Error404"));
            }
        }
        [Route("UserPanel/EditProfile")]
        public async Task<IActionResult> EditProfile()
        {
            return View(await _userService.GetUserInfoForEdit(User.GetUserId()));
        }
        [Route("UserPanel/EditProfile")]
        [HttpPost]
        public async Task<IActionResult> EditProfile(EditProfileDTO editProfile)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.EditForUserInfo(editProfile, User.GetUserId());
                switch (result)
                {
                    case UserPanelResult.NotFound:
                        TempData[ErrorMessage] = "موردی یافت نشد";
                        break;
                    case UserPanelResult.Success:
                        TempData[SuccessMessage] = "عملیات با موفقیت انجام شد";
                        break;
                }
            }
            return View(editProfile);
        }


        [Route("/UserPanel/ChangePassword")]
        public async Task<IActionResult> ChangePassword()
        {
            return View();
        }
        [Route("/UserPanel/ChangePassword")]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO change)
        {
            string userName = User.Identity.Name;
            if (ModelState.IsValid)
            {
                var result = await _userService.ChangeUserPassword(change, User.GetUserId());
                switch (result)
                {
                    case UserPanelResult.NotFound:
                        TempData[ErrorMessage] = "موردی یافت نشد";
                        break;
                    case UserPanelResult.Error:
                        TempData[WarningMessage] = "پسورد قدیمی اشتباه است";
                        break;
                    case UserPanelResult.Success:
                        TempData[SuccessMessage] = "عملیات با موفقیت انجام شد";
                        break;
                }
            }
            return View(change);
        }
    }
}
