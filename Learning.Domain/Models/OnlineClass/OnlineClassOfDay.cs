using Learning.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.Models.OnlineClass
{
    public class OnlineClassOfDay : BaseEntity
    {
        [Key]
        public int OCD_Id { get; set; }
        [Required]
        public int OnlineClassId { get; set; }
        [Display(Name = " روز")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Day { get; set; }
        [Display(Name = "زمان شروع")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string StartTime { get; set; }
        [Display(Name = "زمان پایان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string EndTime { get; set; }



        #region Relation
        [ForeignKey("OnlineClassId")]
        public virtual OnlineClass OnlineClass { get; set; }

        #endregion
    }
}
