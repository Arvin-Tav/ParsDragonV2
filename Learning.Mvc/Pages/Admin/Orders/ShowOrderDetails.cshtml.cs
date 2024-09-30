using Learning.Application.Interfaces;
using Learning.Application.Security;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Learning.Mvc.Pages.Admin.Orders
{
    [PermissionChecker("Orders")]
    public class ShowOrderDetailsModel : PageModel
    {
        private readonly IOrderService _orderService;

        public ShowOrderDetailsModel(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public Learning.Domain.Models.Order.Order Order { get; set; }
        public async Task OnGet(int id)
        {
            Order = await _orderService.ShowOrderDetails(id);
        }
    }
}
