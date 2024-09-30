using Learning.Domain.DTO.Wallet;
using Learning.Domain.Models.Account;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Learning.Mvc.Areas.UserPanel.ViewComponents
{
    public class ChargeWalletViewComponent : ViewComponent
    {

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("ChargeWallet",new ChargeWalletDTO()));
        }
    }
}
