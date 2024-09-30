using Learning.Domain.Models.Account;
using Learning.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.Models.Blog
{
    public class Blog : BaseEntity
    {
        [Key]
        public int BlogId { get; set; }
        [StringLength(200)]
        [MaxLength(200)]
        public string BlogImageName { get; set; }
        [Display(Name = " موضوع")]
        [StringLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        [Required(ErrorMessage = "لطفا{0} را وارد کنید")]
        public string Title { get; set; }
        [Display(Name = " توضیح مختصر")]
        [Required(ErrorMessage = "لطفا{0} را وارد کنید")]
        [StringLength(300, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        [MaxLength(300, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        public string Description { get; set; }
        [Required(ErrorMessage = "توضیحات کامل پست را وارد کنید")]
        [MaxLength(150)]
        public string BodyHtml { get; set; }
        [Display(Name = " کلمات کلیدی ")]
        [StringLength(600, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        [MaxLength(600, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        public string Tags { get; set; }
        public int UserId { get; set; }

        #region Relation
        public virtual User User { get; set; }
        #endregion
    }
}
