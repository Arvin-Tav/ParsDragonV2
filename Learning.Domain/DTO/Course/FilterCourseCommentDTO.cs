using Learning.Domain.DTO.Paging;
using Learning.Domain.Models.Course;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.DTO.Course
{
    public class FilterCourseCommentDTO : BasePaging
    {
        #region properies
        public string Serach { get; set; }
        public string UserName { get; set; }
        public int? CourseId { get; set; }
        public int? UserId { get; set; }
        public FilterCourseCommentOrder FilterCourseCommentOrder { get; set; }
        public FilterCourseCommentRead FilterCourseCommentRead { get; set; }
        public List<CourseComment> CourseComments { get; set; }
        #endregion
        #region methoods
        public FilterCourseCommentDTO SetCourseComment(List<CourseComment> courseComments)
        {
            this.CourseComments = courseComments;
            return this;
        }
        public FilterCourseCommentDTO SetPaging(BasePaging paging)
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
    public enum FilterCourseCommentRead
    {
        [Display(Name = "خوانده نشده")]
        NotReadAdmin,
        [Display(Name = "خوانده شده")]
        ReadAdmin,
        [Display(Name = "همه ")]
        All,

    }
    public enum FilterCourseCommentOrder
    {
        [Display(Name = "به ترتیب آخرین")]
        CreateDate_DES,
        [Display(Name = "به ترتیب تاریخ")]
        CreateDate_ASC,
    }
}
