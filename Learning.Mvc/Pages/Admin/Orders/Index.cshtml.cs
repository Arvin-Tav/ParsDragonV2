using Learning.Application.Interfaces;
using Learning.Application.Security;
using Learning.Domain.DTO.Order;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Learning.Mvc.Pages.Admin.Orders
{
    [PermissionChecker("Orders")]

    public class IndexModel : PageModel
    {
        private readonly IOrderService _orderService;

        public IndexModel(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public FilterOrderDTO FilterOrderDTO { get; set; }
        public async Task OnGet(FilterOrderDTO filter)
        {
            FilterOrderDTO=await _orderService.FilterOrder(filter);
        }
    }
}
