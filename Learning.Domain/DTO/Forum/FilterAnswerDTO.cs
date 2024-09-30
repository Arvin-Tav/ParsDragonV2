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
    public class FilterAnswerDTO : BasePaging
    {
        #region properties
        public int? CourseId { get; set; }
        public int? QuestionId { get; set; }
        public int? AsnswerId { get; set; }
        public int? UserId { get; set; }
        public string Search { get; set; }
        public string UserName { get; set; }
        public FilterAsnwerRead FilterAsnwerRead { get; set; }
        public FilterAsnwerOrder FilterAsnwerOrder { get; set; }
        public List<Answer> Answers { get; set; }
        #endregion
        #region methoods
        public FilterAnswerDTO SetAsnwers(List<Answer> answers)
        {
            this.Answers = answers;
            return this;
        }
        public FilterAnswerDTO SetPaging(BasePaging paging)
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

    public enum FilterAsnwerRead
    {
        [Display(Name = "همه")]
        All,
        [Display(Name = "خوانده نشده")]
        NotRead,
        [Display(Name = "خوانده شده")]
        Read
    }
    public enum FilterAsnwerOrder
    {
        [Display(Name = "به ترتیب آخرین")]
        CreateDate_DES,
        [Display(Name = "به ترتیب تاریخ")]
        CreateDate_ASC,
    }
}
