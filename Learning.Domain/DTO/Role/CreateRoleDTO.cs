using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.DTO.Role
{
    public class CreateRoleDTO
    {
        [Display(Name = "عنوان نقش")]
        [Required(ErrorMessage = "لطفا{0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        public string RoleTitle { get; set; }
        public List<int> SelectedPermission { get; set; }
    }
}
