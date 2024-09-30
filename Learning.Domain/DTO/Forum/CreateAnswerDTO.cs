using Learning.Domain.DTO.Account;
using System.ComponentModel.DataAnnotations;

namespace Learning.Domain.DTO.Forum
{
    public class CreateAnswerDTO : CaptchaDTO
    {
        [Required]
        public int CourseId { get; set; }
        [Required]
        public int QuestionId { get; set; }
        [Display(Name = " جواب سوال")]
        [Required(ErrorMessage = "جواب سوال را وارد کنید")]
        public string Body { get; set; }

        [Required]
        public int UserId { get; set; }

    }
}
