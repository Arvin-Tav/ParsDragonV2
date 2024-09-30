using Learning.Domain.DTO.Config;
using Learning.Domain.DTO.Slider;
using Learning.Domain.Models.Config;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Application.Interfaces
{
    public interface IConfigService
    {
        Task<ConfigDTO> GetAllCongigs();
        Task<string> GetConfigValueByKey(string key);
        void UpdateConfig(Config config);
        Task UpdateConfigForEdit(string key, string value);
        Task<Config> GetConfigByKey(string key);
        Task<ConfigReult> EditConfig(ConfigDTO config, IFormFile akseSafeyeAsly, IFormFile aksTamasBama);
        Task<SliderTextHomeDTO> GetSliderHomeText();
        Task<string> GetSliderHomeImage();
        Task<string> AddImageCkEditor(IFormFile imageCkeditor);
    }
}
