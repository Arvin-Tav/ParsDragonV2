using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.DTO.Config
{
    public class ConfigDTO
    {
        [Required(ErrorMessage = "لطفا  ایمیل را وارد کنید")]
        public string Email { get; set; }
        [Required(ErrorMessage = "لطفا آدرس را وارد کنید")]
        public string Address { get; set; }
        public string AksTamasBamaName { get; set; }

        public string AkseSafeyeAslyName { get; set; }
        [Required(ErrorMessage = "لطفا شماره  را وارد کنید")]
        public string TellHome { get; set; }
        [Required(ErrorMessage = "لطفا موبایل را وارد کنید")]
        public string TellMobile { get; set; }
        [Required(ErrorMessage = "لطفا  تلگرم را وارد کنید")]
        public string Telegram { get; set; }
        [Required(ErrorMessage = "لطفا لینک تلگرم را وارد کنید")]
        public string Inista { get; set; }
        [Required(ErrorMessage = "لطفا آدرس واتساب را وارد کنید")]
        public string Whatsapp { get; set; }

        [Required(ErrorMessage = "لطفا  همکاری با ما  را وارد کنید")]
        public string HamkaryBama { get; set; }
        [Required(ErrorMessage = "لطفا مشاوره را وارد کنید")]
        public string Consulting { get; set; }
        [Required(ErrorMessage = "لطفا  قوانین خرید  را وارد کنید")]
        public string GavaninKharid { get; set; }
        [Required(ErrorMessage = "لطفا  راهنمای خرید  را وارد کنید")]
        public string RahnemyaKharid { get; set; }
        [Required(ErrorMessage = "لطفا درباره ما را کامل کنید")]
        public string DarBareyeMa { get; set; }
        [Required(ErrorMessage = "لطفا متن ردیف اول صفحه اصلی را کامل کنید")]
        public string TextHome1 { get; set; }
        [Required(ErrorMessage = "لطفا متن ردیف دوم صفحه اصلی را کامل کنید")]
        public string TextHome2 { get; set; }
        //public bool IsTelegram { get; set; }
        //public bool IsInista { get; set; }
        //public bool IsWhatsapp { get; set; }
    }
}
