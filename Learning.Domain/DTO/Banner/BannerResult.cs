using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.DTO.Banner
{
    public enum BannerResult
    {
        Error,
        NotImage,
        ErrorImage,
        Success,
    } 
    public enum DeleteBannerResult
    {
        NotFound,
        Success,
    }
}
