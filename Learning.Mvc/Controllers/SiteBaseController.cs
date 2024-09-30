using Microsoft.AspNetCore.Mvc;

namespace Learning.Mvc.Controllers
{
    public class SiteBaseController : Controller
    {
        protected string ErrorMessage = "ErrorMessage";
        protected string SuccessMessage = "SuccessMessage";
        protected string WarningMessage = "WarningMessage";
        protected string InfoMessage = "InfoMessage";


        protected string ErrorSwalMessage = "ErrorSwalMessage";
        protected string SuccessSwalMessage = "SuccessSwalMessage";
        protected string WarningSwalMessage = "WarningSwalMessage";
        protected string InfoSwalMessage = "InfoSwalMessage";
    }
}
