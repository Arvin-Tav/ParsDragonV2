using Learning.Application.Interfaces;
using Learning.Application.Security;
using Learning.Domain.DTO.Wallet;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Learning.Mvc.Pages.Admin.Wallet
{
    //[PermissionChecker("Wallet")]
    public class IndexModel : PageModel
    {
        private readonly IWalletService _walletService;

        public IndexModel(IWalletService walletService)
        {
            _walletService = walletService;
        }
        public FilterWalletDTO FilterWalletDTO { get; set; }
        public async Task OnGet(FilterWalletDTO filter)
        {
            FilterWalletDTO =await _walletService.FilterWallet(filter);
        }
    }
}
