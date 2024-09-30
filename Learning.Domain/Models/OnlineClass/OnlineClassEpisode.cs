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
    public class OnlineClassEpisode : BaseEntity
    {
        [Key]
        public int OnlineClassEpisodeId { get; set; }

        [Required]
        public int OnlineClassId { get; set; }

        [Display(Name = "عنوان بخش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Title { get; set; }
        [MaxLength(150)]
        [Display(Name = "فایل")]
        public string EpisodeFileName { get; set; }

        [Display(Name = "نمایش")]
        public bool IsShow { get; set; }



        #region Relations
        [ForeignKey("OnlineClassId")]
        public virtual OnlineClass OnlineClass { get; set; }
        #endregion
    }
}
