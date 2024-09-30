using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.DTO.Discount
{
    public enum DiscountResult
    {
        NotFound,
        CountAndDateIsNull,
        ErrorDiscountName,
        Success,
    }
    public enum DeleteDiscountResult
    {
        NotFound,
        Success
    }
    public enum DiscountUseType
    {
        Succes, 
        ExpierDate, 
        NotFound, 
        Finished, 
        UserUsed, 
        IsFinaly
    }
}
