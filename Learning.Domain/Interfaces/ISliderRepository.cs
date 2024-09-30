using Learning.Domain.DTO.Slider;
using Learning.Domain.Models.Banner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.Interfaces
{
    public interface ISliderRepository
    {
        Task<FilterSliderDTO> FilterSlider(FilterSliderDTO filter);
        Task<List<Slider>> GetSliders(int takeCount = 8);
        Task AddSlider(Slider slider);
        void UpdateSlider(Slider slider);
        Task<Slider> GetSliderById(int sliderId);
        Task Save();
    }
}
