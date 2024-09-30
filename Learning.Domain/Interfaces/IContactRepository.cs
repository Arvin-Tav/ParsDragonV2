using Learning.Domain.DTO.Contact;
using Learning.Domain.Models.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.Interfaces
{
    public interface IContactRepository
    {
        Task<FilterContactDTO> FilterContacts(FilterContactDTO filter);
        Task AddContact(Contact contact);
        Task<Contact> GetContactById(int contactId);
        void UpdateContact(Contact contact);
        Task Save();
    }
}
