using Learning.Domain.DTO.Paging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.DTO.Wallet
{
    public class FilterWalletDTO : BasePaging
    {
        #region properties
        public string Search { get; set; }
        public int? UserId { get; set; }
        public int Charge { get; set; }
        public int Withdraw { get; set; }
        public FilterWalletType FilterWalletType { get; set; }
        public FilterCourseOrder FilterCourseOrder { get; set; }

        public List<Learning.Domain.Models.Wallet.Wallet> Wallets { get; set; }
        #endregion

        #region methoods
        public FilterWalletDTO SetWallets(List<Learning.Domain.Models.Wallet.Wallet> wallets)
        {
            this.Wallets = wallets;
            return this;
        }
        public FilterWalletDTO SetPaging(BasePaging paging)
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

    public enum FilterWalletType
    {
        [Display(Name = "همه ")]
        All,
        [Display(Name = "تراکنش ناموفق")]
        Error,
        [Display(Name = "واریز")]
        Deposit,
        [Display(Name = "برداشت")]
        Withdraw,
        [Display(Name = "واریز و یرداشت های موفق")]
        IsPay,
    }
    public enum FilterCourseOrder
    {
        [Display(Name = "به ترتیب آخرین")]
        CreateDate_DES,
        [Display(Name = "به ترتیب تاریخ")]
        CreateDate_ASC,
    }
}
