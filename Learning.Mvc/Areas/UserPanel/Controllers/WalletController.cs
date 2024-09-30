using Learning.Application.Interfaces;
using Learning.Domain.DTO.Wallet;
using Learning.Mvc.Controllers;
using Learning.Mvc.PresentationExtensions;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using ZarinpalSandbox;

namespace Learning.Mvc.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    public class WalletController : SiteBaseController
    {
        private Microsoft.Extensions.Configuration.IConfiguration _configuration;
        private readonly IWalletService _walletService;
        public WalletController(IWalletService walletService, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            _walletService = walletService;
            _configuration = configuration;
        }
        [Route("UserPanel/Wallet")]
        public async Task<IActionResult> Index(FilterWalletDTO filter)
        {
            filter.FilterWalletType = FilterWalletType.IsPay;
            filter.UserId = User.GetUserId();
            ViewBag.WalletPrice = await _walletService.BalanceUserWallet(User.GetUserId());
            return View(await _walletService.FilterWallet(filter));
        }
        [Route("UserPanel/Wallet")]
        [HttpPost]
        public async Task<IActionResult> Index(ChargeWalletDTO charge)
        {
            //if (!ModelState.IsValid)
            //{
            //    ViewBag.ListWallet = await _walletService.FilterWallet(new FilterWalletDTO() { UserId = User.GetUserId() });
            //    return View(charge);
            //}
            //IPAddress ipAddres = Request.HttpContext.Connection.RemoteIpAddress;
            //string ip = ipAddres.ToString();
            ////Todo Online Payment
            //int walletId = await _walletService.ChargeWallet(User.GetUserId(), charge.Amount, "شارژ حساب", false, ip);

            //#region Online Payment

            ////   var payment = new Payment("ec98a5a6-bd43-46a7-82f8-9da71c891f42", charge.Amount);
            //var payment = new Payment(charge.Amount);

            //var res = payment.PaymentRequest("شارژ کیف پول", $"https://{_configuration["localhost:NameAdress"]}/OnlinePayment/" + walletId, "Info@MoneyMagnet.Com", "+989120487833");

            //if (res.Result.Status == 100)
            //{
            //    // return Redirect("https://zarinpal.com/pg/StartPay/" + res.Result.Authority);
            //    return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + res.Result.Authority);
            //}

            //#endregion
            return View();
        }
    }
}
