using Learning.Application.Extentions;
using Learning.Application.Interfaces;
using Learning.Application.Utils;
using Learning.Domain.DTO.Slider;
using Learning.Domain.Interfaces;
using Learning.Domain.Models.Banner;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Application.Services
{
    public class SliderService : ISliderService
    {
        #region constructor
        private readonly ISliderRepository _sliderRepository;
        public SliderService(ISliderRepository sliderRepository)
        {
            _sliderRepository = sliderRepository;
        }


        #endregion

        #region slider

        public async Task AddSlider(Slider slider)
        {
            await _sliderRepository.AddSlider(slider);
            await _sliderRepository.Save();
        }

        public async Task<SliderResult> CreateSLider(CreateSliderDTO slider, IFormFile imageSlider)
        {
            if (imageSlider == null) return SliderResult.NotImage;
            if (imageSlider != null && !imageSlider.IsImage()) return SliderResult.ErrorImage;

            Slider newSlider = new Slider();

            newSlider.SliderImageName = TextFixer.GenerateUniqCodeString(slider.Name) + Path.GetExtension(imageSlider.FileName);
            bool result = imageSlider
                .AddImageToServer(newSlider.SliderImageName, PathExtensions.SliderImageOrginServer, null, null, PathExtensions.SliderImageThumbServer);
            if (!result) return SliderResult.ErrorImage;

            newSlider.Name = slider.Name;
            newSlider.GroupId = slider.GroupId;
            newSlider.SubGroup = slider.SubGroup;
            newSlider.IsOnlineClass = slider.IsOnlineClass;

            await AddSlider(newSlider);
            return SliderResult.Success;

        }
        public async Task<EditSliderDTO> EditSlider(int sliderId)
        {
            var slider = await _sliderRepository.GetSliderById(sliderId);
            if (slider == null) return null;

            return new EditSliderDTO()
            {
                GroupId = slider.GroupId,
                SliderImageName = slider.SliderImageName,
                Name = slider.Name,
                SubGroup = slider.SubGroup,
                IsOnlineClass = slider.IsOnlineClass,
                SliderId = slider.SliderId,
            };
        }
        public async Task<SliderResult> GetSliderForEdit(EditSliderDTO slider, IFormFile imageSlider)
        {
            var editanner = await _sliderRepository.GetSliderById(slider.SliderId);
            if (imageSlider != null && !imageSlider.IsImage()) return SliderResult.ErrorImage;
            if (editanner == null) return SliderResult.Error;
            if (imageSlider != null)
            {
                slider.SliderImageName = TextFixer.GenerateUniqCodeString(slider.Name) + Path.GetExtension(imageSlider.FileName);
                bool result = imageSlider
                    .AddImageToServer(slider.SliderImageName, PathExtensions.SliderImageOrginServer, null, null, PathExtensions.SliderImageThumbServer, editanner.SliderImageName);
                if (!result) return SliderResult.ErrorImage;
            }
            editanner.Name = slider.Name;
            editanner.GroupId = slider.GroupId;
            editanner.SubGroup = slider.SubGroup;
            editanner.SliderImageName = slider.SliderImageName;
            editanner.IsOnlineClass = slider.IsOnlineClass;
            editanner.UpdateDate = DateTime.Now;
            _sliderRepository.UpdateSlider(editanner);
            await _sliderRepository.Save();
            return SliderResult.Success;
        }


        public async Task<DeleteSliderResult> DeleteSlider(int sliderId)
        {
            var slider = await GetSliderById(sliderId);
            if (slider == null) return DeleteSliderResult.NotFound;
            slider.IsDelete = true;
            _sliderRepository.UpdateSlider(slider);
            await _sliderRepository.Save();
            return DeleteSliderResult.Success;
        }




        public async Task<FilterSliderDTO> FilterSlider(FilterSliderDTO filter)
        {
            return await _sliderRepository.FilterSlider(filter);
        }
        public async Task<List<Slider>> GetSliders(int takeCount = 8)
        {
            return await _sliderRepository.GetSliders(takeCount);
        }

        public async Task<Slider> GetSliderById(int sliderId)
        {
            return await _sliderRepository.GetSliderById(sliderId);
        }


        public async Task UpdateSlider(Slider slider)
        {
            _sliderRepository.UpdateSlider(slider);
            await _sliderRepository.Save();
        }
        #endregion




    }
}
