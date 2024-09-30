using Learning.Domain.DTO.Paging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.DTO.OnlineClass
{
    public class FilterOnlineClassDTO : BasePaging
    {
        #region properies
        public int? UserId { get; set; }
        public int? TeacherId { get; set; }
        public string TeacherName { get; set; }
        public string Search { get; set; }
        public List<int> SelectedGroups { get; set; }
        public int? StartPrice { get; set; }
        public int? EndPrice { get; set; }
        public FilterOnlineClassState FilterOnlineClassState { get; set; }
        public FilterOnlineClassSort FilterOnlineClassSort { get; set; }
        public FilterOnlineClassRead FilterOnlineClassRead { get; set; }
        public FilterOnlineClassOrder FilterOnlineClassOrder { get; set; }
        public List<Learning.Domain.Models.OnlineClass.OnlineClass> OnlineClasses { get; set; }
        #endregion
        #region methoods
        public FilterOnlineClassDTO SetOnlineClasses(List<Learning.Domain.Models.OnlineClass.OnlineClass> onlineClasses)
        {
            this.OnlineClasses = onlineClasses;
            return this;
        }
        public FilterOnlineClassDTO SetPaging(BasePaging paging)
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

    public enum FilterOnlineClassState
    {
        [Display(Name = "همه")]
        All,
        [Display(Name = "نقدی")]
        Buy,
        [Display(Name = "رایگان")]
        Free,
    }

    public enum FilterOnlineClassSort
    {
        [Display(Name = "تاریخ انتشار")]
        Date,
        [Display(Name = "عنوان")]
        Title,
        [Display(Name = "قسمت")]
        Price,
        [Display(Name = "محبوبیت")]
        Popular
    }

    public enum FilterOnlineClassRead
    {
        [Display(Name = "نمایش ")]
        Show,
        [Display(Name = "مخفی")]
        NotShow,
        [Display(Name = "همه")]
        All,
    }
    public enum FilterOnlineClassOrder
    {
        [Display(Name = "به ترتیب آخرین")]
        CreateDate_DES,
        [Display(Name = "به ترتیب تاریخ")]
        CreateDate_ASC,
    }
}
