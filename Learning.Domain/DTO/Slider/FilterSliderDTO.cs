using Learning.Domain.DTO.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Learning.Domain.Models.Banner;
using System.ComponentModel.DataAnnotations;

namespace Learning.Domain.DTO.Slider
{
    public class FilterSliderDTO : BasePaging
    {
        #region properties
        public string Search { get; set; }
        public FilterSliderOrder FilterSliderOrder { get; set; }
        public List<Learning.Domain.Models.Banner.Slider> Sliders { get; set; }
        #endregion

        #region methoods
        public FilterSliderDTO SetSliders(List<Learning.Domain.Models.Banner.Slider> sliders)
        {
            this.Sliders = sliders;
            return this;
        }
        public FilterSliderDTO SetPaging(BasePaging paging)
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

    public enum FilterSliderOrder
    {
        [Display(Name = "به ترتیب آخرین")]
        CreateDate_DES,
        [Display(Name = "به ترتیب تاریخ")]
        CreateDate_ASC,
    }
}
