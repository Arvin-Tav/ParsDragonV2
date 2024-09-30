using Learning.Application.Interfaces;
using Learning.Application.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Learning.Mvc.Pages.Admin.Contact
{
    [PermissionChecker("Contact")]
    public class ShowMessageModel : PageModel
    {
        private readonly IContactService _contactService;
        public ShowMessageModel(IContactService contactService)
        {
            _contactService = contactService;
        }
        public Learning.Domain.Models.Contact.Contact Contact { get; set; }
        public async Task OnGet(int id)
        {
            await _contactService.IsSeenContact(id);
            Contact = await _contactService.GetContactById(id);
        }
    }
}
