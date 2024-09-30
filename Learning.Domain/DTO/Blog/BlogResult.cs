using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.DTO.Blog
{
    public enum BlogResult
    {
        Error,
        NotImage,
        ErrorImage,
        Success,
    }
    public enum DeleteBlogResult
    {
        NotFound,
        Success
    }
}
