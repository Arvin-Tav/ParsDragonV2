using Learning.Domain.DTO.Paging;
using Learning.Domain.Models.Question;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.DTO.Forum
{
    public class FilterQuestionDTO:BasePaging
    {
        #region properties
        public int? CourseId { get; set; }
        public int? QuestionId { get; set; }
        public int? UserId { get; set; }
        public string Search { get; set; }
        public string UserName { get; set; }
        public FilterQuestionRead FilterQuestionRead { get; set; }
        public FilterQuestionOrder FilterQuestionOrder { get; set; }
        public List<Question> Questions { get; set; }
        #endregion
        #region methoods
        public FilterQuestionDTO SetQuestions(List<Question> questions)
        {
            this.Questions = questions;
            return this;
        }
        public FilterQuestionDTO SetPaging(BasePaging paging)
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

    public enum FilterQuestionRead
    {
        [Display(Name = "همه")]
        All,
        [Display(Name = "خوانده نشده")]
        NotRead,
        [Display(Name = "خوانده شده")]
        Read
    }
    public enum FilterQuestionOrder
    {
        [Display(Name = "به ترتیب آخرین")]
        CreateDate_DES,
        [Display(Name = "به ترتیب تاریخ")]
        CreateDate_ASC,
    }
}
