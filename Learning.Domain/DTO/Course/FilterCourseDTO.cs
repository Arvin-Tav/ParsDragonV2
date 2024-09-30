using Learning.Domain.DTO.Paging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.DTO.Course
{
    public class FilterCourseDTO:BasePaging
    {
        #region properies
        public int? UserId { get; set; }
        public int? TeacherId { get; set; }
        public string TeacherName { get; set; }
        public string Search { get; set; }
        public bool IsSuggested { get; set; }
        public List<int> SelectedGroups { get; set; }
        public int? StartPrice { get; set; }
        public int? EndPrice { get; set; }
        public FilterCourseState FilterCourseState { get; set; }
        public FilterCourseSort FilterCourseSort { get; set; }
        public FilterCourseRead FilterCourseRead { get; set; }
        public FilterCourseOrder FilterCourseOrder { get; set; }
        public List<Learning.Domain.Models.Course.Course> Courses { get; set; }
        #endregion
        #region methoods
        public FilterCourseDTO SetCoursees(List<Learning.Domain.Models.Course.Course> courses)
        {
            this.Courses = courses;
            return this;
        }
        public FilterCourseDTO SetPaging(BasePaging paging)
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

    public enum FilterCourseState
    {
        [Display(Name = "همه")]
        All,
        [Display(Name = "نقدی")]
        Buy,
        [Display(Name = "رایگان")]
        Free,
    }

    public enum FilterCourseSort
    {
        [Display(Name = "تاریخ انتشار")]
        Date,
        [Display(Name = "عنوان")]
        Title,
        [Display(Name = "قیمت")]
        Price,
        [Display(Name = "محبوبیت")]
        Popular
    }

    public enum FilterCourseRead
    {
        [Display(Name = "همه")]
        All,
        [Display(Name = "مخفی")]
        NotShow,
        [Display(Name = "نمایش ")]
        Show,
      
    }
    public enum FilterCourseOrder
    {
        [Display(Name = "به ترتیب آخرین")]
        CreateDate_DES,
        [Display(Name = "به ترتیب تاریخ")]
        CreateDate_ASC,
    }
}
