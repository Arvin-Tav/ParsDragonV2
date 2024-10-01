using GoogleReCaptcha.V3.Interface;
using Learning.Application.Convertors;
using Learning.Application.Interfaces;
using Learning.Application.Utils;
using Learning.Domain.DTO.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Learning.Mvc.Controllers
{
    public class AccountController : SiteBaseController
    {
        private readonly IUserService _userService;
        private readonly ICaptchaValidator _captchaValidator;

        public AccountController(IUserService userService,
            ICaptchaValidator captchaValidator)
        {
            _userService = userService;
            _captchaValidator = captchaValidator;
        }
        #region Register
        [HttpGet("register")]
        public async Task<IActionResult> Register()
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    return Redirect("/UserPanel");
                }

                return await Task.FromResult(View());
            }
            catch
            {
                return await Task.FromResult(Redirect("/Home/Error404"));
            }
        }

        [HttpPost("register"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDTO register)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var result = await _userService.RegisterUser(register);
                    switch (result)
                    {
                        case RegisterResult.Error:
                            TempData[ErrorMessage] = "خطا در ارتباط";
                            break;
                        case RegisterResult.MobileExists:
                            ModelState.AddModelError("Mobile", "موبایل تکراری می باشد");
                            TempData[ErrorMessage] = "موبایل تکراری می باشد";
                            break;
                        case RegisterResult.EmailExists:
                            ModelState.AddModelError("Email", "ایمیل تکراری می باشد");
                            TempData[ErrorMessage] = "ایمیل تکراری می باشد";
                            break;
                        case RegisterResult.UsernameExists:
                            ModelState.AddModelError("UserName", "نام کاربری تکراری می باشد");
                            TempData[ErrorMessage] = "نام کاربری تکراری می باشد";
                            break;
                        case RegisterResult.Success:
                            TempData[SuccessMessage] = "ثبت نام شما با موفقیت انجام سد لطفا کد ارسالی را وارد کنید ";
                            TempData[InfoMessage] = "در صورت  دریافت نکردن کد فعال سازی از قسمت فراموشی کلمه عبور اقدام نمایید ";
                            return RedirectToAction("ActivateMobile", "Account", new { mobile = register.Mobile });
                    }
                }

                return View(register);
            }
            catch
            {
                return await Task.FromResult(Redirect("/Home/Error404"));
            }
        }
        #endregion


        #region active mobile
        [HttpGet("active-mobile")]
        public IActionResult ActivateMobile(string mobile)
        {
            if (User.Identity.IsAuthenticated) return Redirect("/");
            var activateMobileDto = new ActivateMobileDTO { Mobile = mobile };
            return View(activateMobileDto);
        }
        [HttpPost("active-mobile"), ValidateAntiForgeryToken]
        public async Task<IActionResult> ActivateMobile(ActivateMobileDTO activate)
        {

            if (ModelState.IsValid)
            {
                var result = await _userService.ActiveAccountByMobile(activate);
                switch (result)
                {
                    case ActivateMobileResult.Error:
                        TempData[ErrorMessage] = "خطا در ارتباط";
                        break;
                    case ActivateMobileResult.NotValideCode:
                        ModelState.AddModelError("MobileActiveCode", "کد وارد شده اشتباه است");
                        TempData[ErrorMessage] = "کد وارد شده اشتباه است";
                        break;
                    case ActivateMobileResult.NotFound:
                        ModelState.AddModelError("MobileActiveCode", "کاربری با مشخصات وارد شده یافت نشد");
                        TempData[ErrorMessage] = "کاربری با مشخصات وارد شده یافت نشد";
                        break;
                    case ActivateMobileResult.Success:
                        TempData[SuccessMessage] = "حساب کاربری شما فعال شد میتوانید وارد حساب خود شوید";
                        return await Task.FromResult(Redirect("/Login?ActiveAccount=true"));
                }
            }

            return await Task.FromResult(View(activate));
        }

        #endregion

        #region Login
        [HttpGet("login")]
        //[RequestSizeLimit(500000000)]  //30.000.000=28,6MB
        public async Task<IActionResult> Login(bool EditProfile = false, bool EditPassword = false, bool ActiveAccount = false, string ReturnUrl = "")
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    return Redirect("/UserPanel");
                }
                ViewBag.EditProfile = EditProfile;
                ViewBag.EditPassword = EditPassword;
                ViewBag.ActiveAccount = ActiveAccount;
                ViewData["returnUrl"] = ReturnUrl;
                return await Task.FromResult(View());
            }
            catch
            {
                return await Task.FromResult(Redirect("/Home/Error404"));
            }
        }
        [HttpPost("login"),ValidateAntiForgeryTokenAttribute]
        public async Task<IActionResult> Login(LoginDTO login, string ReturnUrl = "")
        {
            try
            {

                if (ModelState.IsValid)
                {

                    var result = await _userService.LoginUser(login);
                    switch (result)
                    {
                        case LoginResult.Error:
                            TempData[ErrorMessage] = "خطا در ارتباط";
                            break;
                        case LoginResult.NotFound:
                            ModelState.AddModelError("MobileActiveCode", "کاربری با مشخصات وارد شده یافت نشد");
                            TempData[ErrorMessage] = "کاربری با مشخصات وارد شده یافت نشد";
                            break;
                        case LoginResult.NotActive:
                            ModelState.AddModelError("MobileActiveCode", "کاربری با مشخصات وارد شده یافت نشد");
                            TempData[ErrorMessage] = "کاربری با مشخصات وارد شده یافت نشد";
                            TempData[InfoMessage] = "حساب کاربری شما فعال نمی باشد لطفا از قسمت فراموشی رمز اقدام کنی";

                            break;
                        case LoginResult.IsBlocked:
                            ModelState.AddModelError("MobileActiveCode", "حساب شما مسدود می باشد لطفا با پشتیبانی تماس بگیرید");
                            TempData[ErrorMessage] = "حساب شما مسدود می باشد لطفا با پشتیبانی تماس بگیرید";
                            break;
                        case LoginResult.Success:
                            TempData[SuccessMessage] = "عملیات ورود با موفقیت انجام شد";
                            var user = await _userService.GetUserByMobileOrEmail(login.EmailOrMobile);
                            var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                        new Claim(ClaimTypes.Name,user.UserName),
                    };
                            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            var principal = new ClaimsPrincipal(identity);

                            var properties = new AuthenticationProperties
                            {
                                IsPersistent = login.RememberMe
                            };
                            await HttpContext.SignInAsync(principal, properties);


                            if (ReturnUrl != "")
                            {
                                if (Url.IsLocalUrl(ReturnUrl))
                                {
                                    return Redirect(ReturnUrl);
                                }
                            }
                            return await Task.FromResult(Redirect("/UserPanel"));

                    }

                }
                return View(login);
            }
            catch
            {
                return await Task.FromResult(Redirect("/Home/Error404"));
            }
        }
        #endregion


        #region forget password
        [HttpGet("forgotPassword")]
        public async Task<IActionResult> ForgotPassword()
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    return Redirect("/UserPanel");
                }
                return await Task.FromResult(View());
            }
            catch
            {
                return await Task.FromResult(Redirect("/Home/Error404"));
            }

        }
        [HttpPost("forgotPassword"), ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDTO forgot)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var result = await _userService.ForgotPassword(forgot);
                    switch (result)
                    {
                        case ForgotPasswordResult.Error:
                            TempData[ErrorMessage] = "خطا در ارتباط";
                            break;
                        case ForgotPasswordResult.NotFound:
                            ModelState.AddModelError("MobileActiveCode", "کاربری با مشخصات وارد شده یافت نشد");
                            TempData[ErrorMessage] = "کاربری با مشخصات وارد شده یافت نشد";
                            break;
                        case ForgotPasswordResult.IsBlocked:
                            ModelState.AddModelError("MobileActiveCode", "حساب شما مسدود می باشد لطفا با پشتیبانی تماس بگیرید");
                            TempData[ErrorMessage] = "حساب شما مسدود می باشد لطفا با پشتیبانی تماس بگیرید";
                            break;
                        case ForgotPasswordResult.Success:
                            TempData[SuccessMessage] = "رمز جدید برای شما ارسال گردید میتوانید وارد حساب خود شوید";
                            TempData[InfoMessage] = "لطفا بعد از ورود از قسمت حساب کاربری اقدام به تغیر رمز خود نمایید";
                            return await Task.FromResult(Redirect("/Login?EditPassword=true"));
                    }
                }
                return await Task.FromResult(View(forgot));

            }
            catch
            {
                return await Task.FromResult(Redirect("/Home/Error404"));
            }
        }
        #endregion

        #region Logout
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return await Task.FromResult(Redirect("/Login"));
            }
            catch
            {
                return await Task.FromResult(Redirect("/Home/Error404"));
            }
        }

        #endregion

    }
}
