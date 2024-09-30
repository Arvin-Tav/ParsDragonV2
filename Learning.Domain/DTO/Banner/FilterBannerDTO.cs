using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Learning.Domain.DTO.Paging;
using Learning.Domain.Models.Banner;

namespace Learning.Domain.DTO.Banner
{
    public class FilterBannerDTO: BasePaging
    {
        #region properties
        public string Search { get; set; }
        public FilterBannerOrder FilterBannerOrder { get; set; }
        public List<Learning.Domain.Models.Banner.Banner> Banners { get; set; }
        #endregion

        #region methods
        public FilterBannerDTO SetBanners(List<Learning.Domain.Models.Banner.Banner> banners)
        {
            this.Banners = banners;
            return this;
        }
        public FilterBannerDTO SetPaging(BasePaging paging)
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


    public enum FilterBannerOrder
    {
        CreateDate_DES,
        CreateDate_ASC,
    }
}
