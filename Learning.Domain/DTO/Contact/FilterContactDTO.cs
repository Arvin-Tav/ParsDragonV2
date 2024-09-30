using Learning.Domain.DTO.Paging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.DTO.Contact
{
    public class FilterContactDTO: BasePaging
    {
        #region properties
        public string Search { get; set; }
        public FilterContactRead FilterContactRead { get; set; }
        public List<Learning.Domain.Models.Contact.Contact> Contacts { get; set; }
        #endregion

        #region methoods
        public FilterContactDTO SetContacts(List<Learning.Domain.Models.Contact.Contact> contacts)
        {
            this.Contacts = contacts;
            return this;
        }
        public FilterContactDTO SetPaging(BasePaging paging)
        {
            this.PageId = paging.PageId;
            this.AllEntitiesCount = paging.AllEntitiesCount;
            this.StartPage = paging.StartPage;
            this.EndPage = paging.EndPage;
            this.HowManyShowPageAfterAndBefore = paging.HowManyShowPageAfterAndBefore;
            this.TakeEntity = paging.TakeEntity;
            this.SkipEntity = paging.SkipEntity;
            this.PageCount = paging.PageCount;
            return this;
        }
        #endregion
    }

    public enum FilterContactRead
    {
        [Display(Name ="همه")]
        All,
        [Display(Name ="خوانده نشده")]
        NotRead,
        [Display(Name ="خوانده شده")]
        Read
    }
    public enum FilterContactOrder
    {
        [Display(Name ="به ترتیب آخرین")]
        CreateDate_DES,
        [Display(Name ="به ترتیب تاریخ")]
        CreateDate_ASC,
    }
}
