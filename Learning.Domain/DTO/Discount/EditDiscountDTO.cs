using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.DTO.Discount
{
    public class EditDiscountDTO : CreateDiscountDTO
    {
        public int DiscountId { get; set; }
    }
}
