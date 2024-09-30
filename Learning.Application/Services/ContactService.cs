using Learning.Application.Interfaces;
using Learning.Domain.DTO.Contact;
using Learning.Domain.Interfaces;
using Learning.Domain.Models.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Application.Services
{
    public class ContactService : IContactService
    {
        #region constructor
        private readonly IContactRepository _contactRepository;
        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        #endregion
        #region contact

        public async Task AddContact(Contact contact)
        {
            await _contactRepository.AddContact(contact);
            await _contactRepository.Save();
        }
        public Task<FilterContactDTO> FilterContact(FilterContactDTO filter)
        {
            return _contactRepository.FilterContacts(filter);
        }

        public async Task<Contact> GetContactById(int id)
        {
           return await _contactRepository.GetContactById(id);
        }

        public async Task IsSeenContact(int id)
        {
            var contact =await GetContactById(id);
            contact.IsSee = true;
           await UpdateContact(contact);
        }

        public async Task UpdateContact(Contact contact)
        {
            _contactRepository.UpdateContact(contact);
            await _contactRepository.Save();
        }

        #endregion

    }
}
