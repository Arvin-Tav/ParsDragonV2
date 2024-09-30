using Learning.Domain.DTO.Contact;
using Learning.Domain.DTO.Paging;
using Learning.Domain.Interfaces;
using Learning.Domain.Models.Contact;
using Learning.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Infra.Data.Repositories
{
    public class ContactRepository : IContactRepository
    {
        #region constructor
        private readonly LearningContext _context;
        public ContactRepository(LearningContext context)
        {
            _context = context;
        }
        #endregion
        #region contact

        public Task AddContact(Contact contact)
        {
            throw new NotImplementedException();
        }
        public async Task<FilterContactDTO> FilterContacts(FilterContactDTO filter)
        {
            IQueryable<Contact> query = _context.Contacts.AsQueryable();

            #region state
            switch (filter.FilterContactRead)
            {
                case FilterContactRead.All:
                    break;
                case FilterContactRead.NotRead:
                    query = query.Where(i => !i.IsSee);
                    break;
                case FilterContactRead.Read:
                    query = query.Where(i => i.IsSee);
                    break;
            }
            #endregion
            #region filter
            if (!string.IsNullOrEmpty(filter.Search))
                query = query.Where(i => EF.Functions.Like(i.Name, $"%{filter.Search}%")||
                EF.Functions.Like(i.TellNo, $"%{filter.Search}%")||
                EF.Functions.Like(i.Email, $"%{filter.Search}%"));
            #endregion
            #region paging
            var pager = Pager.Build(filter.PageId,await query.CountAsync(),filter.TakeEntity,filter.HowManyShowPageAfterAndBefore);
            var allEntiies = await query.Paging(pager).ToListAsync();
            #endregion
            return filter.SetPaging(pager).SetContacts(allEntiies);
        }

        public async Task<Contact> GetContactById(int contactId)
        {
            return await _context.Contacts.AsQueryable().FirstOrDefaultAsync(i => i.ContactId == contactId);
        }

        public void UpdateContact(Contact contact)
        {
            _context.Update(contact);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
        #endregion

    }
}
