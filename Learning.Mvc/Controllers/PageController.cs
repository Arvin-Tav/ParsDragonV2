using Learning.Application.Interfaces;
using Learning.Domain.Models.Contact;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Learning.Mvc.Controllers
{
    public class PageController : SiteBaseController
    {
        private IContactService _contactService;
        private IConfigService _configService;
        public PageController(IContactService contactService, IConfigService configService)
        {
            _contactService = contactService;
            _configService = configService;
        }
        [Route("Contact")]
        public async Task<IActionResult> ContactUs()
        {
            ViewData["Email"] = await _configService.GetConfigValueByKey("Email");
            ViewData["TellHome"] = await _configService.GetConfigValueByKey("TellHome");
            ViewData["Address"] = await _configService.GetConfigValueByKey("Address");
            ViewData["AksTamasBama"] = await _configService.GetConfigValueByKey("AksTamasBama");
            return View();
        }
        [HttpPost]
        [Route("Contact")]
        public async Task<IActionResult> ContactUs(Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return View(contact);
            }
            await _contactService.AddContact(contact);
            ViewBag.IsSend = true;
            return View();
        }
        [Route("About")]
        public async Task<IActionResult> About()
        {
            ViewData["DarBareyeMa"] =await _configService.GetConfigValueByKey("DarBareyeMa");
            ViewData["Email"] =await _configService.GetConfigValueByKey("Email");
            ViewData["TellHome"] =await _configService.GetConfigValueByKey("TellHome");
            ViewData["TellMobile"] =await _configService.GetConfigValueByKey("TellMobile");
            return View();
        }
        [Route("WorkWithMoneyMagnet")]
        public async Task<IActionResult> Work()
        {
            ViewData["HamkaryBama"] = await _configService.GetConfigValueByKey("HamkaryBama");
            return View();
        }
        [Route("Terms")]
        public async Task<IActionResult> Terms()
        {
            ViewBag.Terms = await _configService.GetConfigValueByKey("GavaninKharid");
            return View();
        }
        [Route("Help")]
        public async Task<IActionResult> Help()
        {
            ViewData["RahnemyaKharid"] = await _configService.GetConfigValueByKey("RahnemyaKharid");
            return View();
        }
        [Route("Consulting")]
        public async Task<IActionResult> Consulting()
        {
            ViewData["Consulting"] = await _configService.GetConfigValueByKey("Consulting");
            return View();
        }
    }
}
