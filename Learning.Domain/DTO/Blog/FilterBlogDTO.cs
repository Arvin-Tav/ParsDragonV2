using Learning.Domain.DTO.Paging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.DTO.Blog
{
    public class FilterBlogDTO : BasePaging
    {
        #region properies
        public string Search { get; set; }

        public FilterBlogOrder FilterBlogOrder { get; set; }
        public List<Learning.Domain.Models.Blog.Blog> Blogs { get; set; }
        #endregion
        #region methoods
        public FilterBlogDTO SetBlog(List<Learning.Domain.Models.Blog.Blog> blogs)
        {
            this.Blogs = blogs;
            return this;
        }
        public FilterBlogDTO SetPaging(BasePaging paging)
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
    public enum FilterBlogOrder
    {
        [Display(Name = "به ترتیب آخرین")]
        CreateDate_DES,
        [Display(Name = "به ترتیب تاریخ")]
        CreateDate_ASC,
    }
}
