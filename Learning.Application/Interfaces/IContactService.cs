using Learning.Domain.DTO.Contact;
using Learning.Domain.Models.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Application.Interfaces
{
    public interface IContactService
    {
        Task AddContact(Contact contact);
        Task<FilterContactDTO> FilterContact(FilterContactDTO filter);
        Task<Contact> GetContactById(int id);
        Task IsSeenContact(int id);
        Task UpdateContact(Contact contact);
    }
}
