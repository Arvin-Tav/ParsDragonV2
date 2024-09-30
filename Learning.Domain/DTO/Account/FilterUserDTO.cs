using Learning.Domain.DTO.Paging;
using Learning.Domain.Models.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.DTO.Account
{
    public class FilterUserDTO: BasePaging
    {
        #region properties
        public string UserName { get; set; }
        public string Email { get; set; }
        public int? CourseId { get; set; }
        public int? OnlineClassId { get; set; }
        public int UserIdLogin { get; set; }
        public FilterUserStatus FilterUserStatus { get; set; }
        public FilterUserType FilterUserType { get; set; }
        public FilterUserOrder FilterUserOrder { get; set; }
        public List<User> Users{ get; set; }
        #endregion

        #region methods
        public FilterUserDTO SetUsers(List<User> users)
        {
            this.Users = users;
            return this;
        }
        public FilterUserDTO SetPaging(BasePaging paging)
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
    public enum FilterUserOrder
    {
        CreateDate_DES,
        CreateDate_ASC,
    }
    public enum FilterUserStatus
    {
        [Display(Name = "همه")]
        All,
        [Display(Name = "فعال")]
        Active,
        [Display(Name = "غیرفعال")]
        NotActive,
        [Display(Name = "مسدود شده ها")]
        IsBlocked,
        [Display(Name = "حذف شده ها")]
        IsDelete,
    }
    public enum FilterUserType
    {
        [Display(Name = "همه")]
        All,
        [Display(Name = "مدیر")]
        Admin,
        [Display(Name = "اساتید")]
        Master,
        [Display(Name = "دانشجوها")]
        Student,
    }
}
