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
    //پیش نیازها کلاس
    public class OnlineClassPrerequisite : BaseEntity
    {
        [Key]
        public int OCP_Id { get; set; }
        [Required]
        public int OnlineClassId { get; set; }
        [Display(Name = "سرفصل های کلاس")]
        [Required(ErrorMessage = "لطفا{0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        [StringLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        public string Name { get; set; }


        #region Relation
        [ForeignKey("OnlineClassId")]
        public virtual OnlineClass OnlineClass { get; set; }
        #endregion

    }
}
