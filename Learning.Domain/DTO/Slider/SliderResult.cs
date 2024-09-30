using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.DTO.Slider
{
    public enum SliderResult
    {
        Error,
        NotImage,
        ErrorImage,
        Success,
    }
    public enum DeleteSliderResult
    {
        NotFound,
        Success,
    }
}
