using Learning.Domain.DTO.Account;
using System.ComponentModel.DataAnnotations;

namespace Learning.Domain.DTO.Forum
{
    public class CreateQuestionDTO
    {
        [Required]
        public int CourseId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Display(Name = "عنوان سوال")]
        [Required(ErrorMessage = "عنوان سوال را وارد کنید")]
        [MaxLength(400)]
        public string Title { get; set; }
        [Display(Name = "متن سوال")]
        [Required(ErrorMessage = "متن سوال را وارد کنید")]
        public string Body { get; set; }
    }
    
}
