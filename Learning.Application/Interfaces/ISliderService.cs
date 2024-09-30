using Learning.Domain.DTO.Slider;
using Learning.Domain.Models.Banner;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Application.Interfaces
{
    public interface ISliderService
    {
        Task<FilterSliderDTO> FilterSlider(FilterSliderDTO filter);
        Task<List<Slider>> GetSliders(int takeCount = 8);
        Task AddSlider(Slider slider);
        Task UpdateSlider(Slider slider);
        Task<DeleteSliderResult> DeleteSlider(int sliderId);
        Task<Slider> GetSliderById(int sliderId);
        Task<SliderResult> CreateSLider(CreateSliderDTO slider, IFormFile imageSlider);
        Task<SliderResult> GetSliderForEdit(EditSliderDTO slider, IFormFile imageSlider);
        Task<EditSliderDTO> EditSlider(int sliderId);
    }
}
