
using Learning.Application.Interfaces;
using Learning.Application.Security;
using Learning.Domain.DTO.Contact;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Learning.Mvc.Pages.Admin.Contact
{
    [PermissionChecker("Contact")]
    public class IndexModel : PageModel
    {
        private readonly IContactService _contactService;
        public IndexModel(IContactService contactService)
        {
            _contactService = contactService;
        }
        public FilterContactDTO FilterContactDTO { get; set; }
        public async Task OnGet(FilterContactDTO filter)
        {
            FilterContactDTO =await _contactService.FilterContact(filter);
        }
    }
}
