using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.DTO.OnlineClass
{
    public enum OnlineClassResult
    {
        Error,
        NotImage,
        NotDate,
        ErrorImage,
        Success,
    }
    public enum OnlineClassInfoResult
    {
        NotFound,
        Success,
    }
}
