using Learning.Domain.DTO.Paging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.DTO.Order
{
    public class FilterOrderDTO: BasePaging
    {
        #region properties
        public int? OrderId { get; set; }
        public int? UserId { get; set; }
        public string Search { get; set; }
        public FilterOrderState FilterOrderState { get; set; }
        public FilterOrderType FilterOrderType { get; set; }
        public FilterAsnwerOrder FilterAsnwerOrder { get; set; }
        public List<Learning.Domain.Models.Order.Order> Orders { get; set; }

        public int DiscountPrice { get; set; }
        public int OrderSum { get; set; }

        #endregion

        #region methoods
        public FilterOrderDTO SetOrders(List<Learning.Domain.Models.Order.Order> orders)
        {
            this.Orders = orders;
            return this;
        }
        public FilterOrderDTO SetPaging(BasePaging paging)
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
    public enum FilterOrderState
    {
        [Display(Name ="همه")]
        All,
        [Display(Name ="پرداخت شده")]
        Finaly,
        [Display(Name ="پرداخت نشده")]
        NotFinaly
    }
    public enum FilterOrderType
    {
        [Display(Name ="همه")]
        All,
        [Display(Name ="دوره ها")]
        Course,
        [Display(Name ="کلاس ها")]
        OnlineClass
    }
    public enum FilterAsnwerOrder
    {
        [Display(Name = "به ترتیب آخرین")]
        CreateDate_DES,
        [Display(Name = "به ترتیب تاریخ")]
        CreateDate_ASC,
    }
}
