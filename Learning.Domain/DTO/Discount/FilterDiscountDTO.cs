using Learning.Domain.DTO.Paging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Learning.Domain.DTO.Discount
{
    public class FilterDiscountDTO : BasePaging
    {
        public FilterDiscountDTO()
        {
            Discounts = new List<Models.Order.Discount>();
        }
        #region properies
        public string Search { get; set; }

        public FilterDiscountOrder FilterDiscountOrder { get; set; }
        public List<Learning.Domain.Models.Order.Discount> Discounts { get; set; }
        #endregion
        #region methoods
        public FilterDiscountDTO SetDiscount(List<Learning.Domain.Models.Order.Discount> discounts)
        {
            this.Discounts =discounts;
            return this;
        }
        public FilterDiscountDTO SetPaging(BasePaging paging)
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
    public enum FilterDiscountOrder
    {
        [Display(Name = "به ترتیب آخرین")]
        CreateDate_DES,
        [Display(Name = "به ترتیب تاریخ")]
        CreateDate_ASC,
    }
}
