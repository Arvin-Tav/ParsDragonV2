using Learning.Application.Interfaces;
using Learning.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace Learning.Mvc.Controllers
{
    public class HomeController : SiteBaseController
    {
        private readonly ICourseService _courseService;
        private readonly ILogger<HomeController> _logger;
        private readonly IConfigService _configService;
        private readonly IWalletService _walletService;
        public HomeController(ICourseService courseService, ILogger<HomeController> logger, IConfigService configService, IWalletService walletService)
        {
            _courseService = courseService;
            _logger = logger;
            _configService = configService;
            _walletService = walletService;
        }


        public IActionResult Index()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> GetSubGroups(int id)
        {
            List<SelectListItem> list = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "انتخاب کنید",Value = ""}
            };
            list.AddRange(await _courseService.GetSubGroupForManageCourse(id));
            return Json(new SelectList(list, "Value", "Text"));
        }
        [HttpPost]
        [Route("file-upload")]
        public async Task<IActionResult> UploadImage(IFormFile upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            if (upload.Length <= 0) return null;
            var url = await _configService.AddImageCkEditor(upload);
            return Json(new { uploaded = true, url });
        }
        public IActionResult Error404()
        {
            return View();
        }


        [Route("OnlinePayment/{id}")]
        public async Task<IActionResult> onlinePayment(int id)
        {
            try
            {
               // if (HttpContext.Request.Query["Status"] != "" &&
               //HttpContext.Request.Query["Status"].ToString().ToLower() == "ok"
               //&& HttpContext.Request.Query["Authority"] != "")
               // {
               //     string authority = HttpContext.Request.Query["Authority"];

               //     var wallet =await _walletService.GetWalletById(id);

               //     var payment = new Payment(wallet.Amount);
               //     var res = payment.Verification(authority).Result;
               //     if (res.Status == 100)
               //     {
               //         ViewBag.code = res.RefId;
               //         ViewBag.IsSuccess = true;
               //         wallet.IsPay = true;
               //         _walletService.UpdateWallet(wallet);
               //         #region Send  Email
               //         //ChargeWalletForAdminViewModel charge = new ChargeWalletForAdminViewModel()
               //         //{
               //         //    UserName = wallet.User.UserName,
               //         //    Amount = wallet.Amount,
               //         //    Mobile = wallet.User.Mobile,
               //         //};
               //         //string body = _viewRender.RenderToStringAsync("_SendEmailToAdminForChargeWallet", charge);
               //         //SendEmail.Send("Sanafezy@gmail.com", "شارژ کیف پول توسط کاربر ", body);
               //         #endregion
               //     }

               // }
                return View();
            }
            catch (Exception)
            {
                return View();
            }
        }
    }
}
