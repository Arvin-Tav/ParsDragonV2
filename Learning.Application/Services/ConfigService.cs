using Learning.Application.EntitiesExtensions;
using Learning.Application.Extentions;
using Learning.Application.Interfaces;
using Learning.Application.Utils;
using Learning.Domain.DTO.Config;
using Learning.Domain.DTO.Slider;
using Learning.Domain.Interfaces;
using Learning.Domain.Models.Config;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Application.Services
{
    public class ConfigService : IConfigService
    {
        #region constructor
        private readonly IConfigRepository _configRepository;
        public ConfigService(IConfigRepository configRepository)
        {
            _configRepository = configRepository;
        }

        #endregion

        public async Task<ConfigDTO> GetAllCongigs()
        {
            ConfigDTO config = new ConfigDTO();
            config.Address = await GetConfigValueByKey("Address");
            config.AkseSafeyeAslyName = await GetConfigValueByKey("AkseSafeyeAsly");
            config.AksTamasBamaName = await GetConfigValueByKey("AksTamasBama");
            config.DarBareyeMa = await GetConfigValueByKey("DarBareyeMa");
            config.Email = await GetConfigValueByKey("Email");
            config.RahnemyaKharid = await GetConfigValueByKey("RahnemyaKharid");
            config.Telegram = await GetConfigValueByKey("Telegram");
            config.TellHome = await GetConfigValueByKey("TellHome");
            config.TellMobile = await GetConfigValueByKey("TellMobile");
            config.Whatsapp = await GetConfigValueByKey("Whatsapp");
            config.TextHome1 = await GetConfigValueByKey("TextHome1");
            config.TextHome2 = await GetConfigValueByKey("TextHome2");
            config.GavaninKharid = await GetConfigValueByKey("GavaninKharid");
            config.HamkaryBama = await GetConfigValueByKey("HamkaryBama");
            config.Inista = await GetConfigValueByKey("Inista");
            config.Consulting = await GetConfigValueByKey("Consulting");
            return config;
        }

        public async Task<Config> GetConfigByKey(string key)
        {
            return await _configRepository.GetConfigKey(key);
        }

        public async Task<SliderTextHomeDTO> GetSliderHomeText()
        {
            SliderTextHomeDTO slider = new SliderTextHomeDTO();
            slider.TextHome1 = await GetConfigValueByKey("TextHome1");
            slider.TextHome2 = await GetConfigValueByKey("TextHome2");
            return slider;
        }

        public async Task<string> GetSliderHomeImage()
        {
            string imagePath = await GetConfigValueByKey("AkseSafeyeAsly");
            if (string.IsNullOrEmpty(imagePath))
            {
                imagePath = "";
            }
            return imagePath.GetConfigImageAddress();
        }
        public async Task<string> AddImageCkEditor(IFormFile imageCkeditor)
        {
            var imageName = TextFixer.GenerateUniqCodeString() + Path.GetExtension(imageCkeditor.FileName);
            imageCkeditor.AddImageToServer(imageName, PathExtensions.CkEditorServer, null, null);
            return PathExtensions.CkEditor + imageName;
        }

        public async Task<ConfigReult> EditConfig(ConfigDTO config, IFormFile akseSafeyeAsly, IFormFile aksTamasBama)
        {
            if (akseSafeyeAsly != null)
            {
                string lastImagNameForDelete = config.AkseSafeyeAslyName;
                config.AkseSafeyeAslyName = TextFixer.GenerateUniqCodeString("صفحه اصلی") + Path.GetExtension(akseSafeyeAsly.FileName);
                bool result = akseSafeyeAsly
               .AddImageToServer(config.AkseSafeyeAslyName, PathExtensions.ConfigImageOrginServer, null, null, PathExtensions.BlogImageThumbServer, lastImagNameForDelete);
                if (!result) return ConfigReult.ErrorImage;
            }
            if (aksTamasBama != null)
            {
                string lastImagNameForDelete = config.AksTamasBamaName;
                config.AksTamasBamaName = TextFixer.GenerateUniqCodeString("صفحه اصلی") + Path.GetExtension(aksTamasBama.FileName);
                bool result = aksTamasBama
               .AddImageToServer(config.AksTamasBamaName, PathExtensions.ConfigImageOrginServer, null, null, PathExtensions.BlogImageThumbServer, lastImagNameForDelete);
                if (!result) return ConfigReult.ErrorImage;
            }
            await UpdateConfigForEdit("Address", config.Address);
            await UpdateConfigForEdit("AkseSafeyeAsly", config.AkseSafeyeAslyName);
            await UpdateConfigForEdit("AksTamasBama", config.AksTamasBamaName);
            await UpdateConfigForEdit("DarBareyeMa", config.DarBareyeMa);
            await UpdateConfigForEdit("Email", config.Email);
            await UpdateConfigForEdit("RahnemyaKharid", config.RahnemyaKharid);
            await UpdateConfigForEdit("Telegram", config.Telegram);
            await UpdateConfigForEdit("TellHome", config.TellHome);
            await UpdateConfigForEdit("TellMobile", config.TellMobile);
            await UpdateConfigForEdit("Whatsapp", config.Whatsapp);
            await UpdateConfigForEdit("TextHome1", config.TextHome1);
            await UpdateConfigForEdit("GavaninKharid", config.GavaninKharid);
            await UpdateConfigForEdit("Inista", config.Inista);
            await UpdateConfigForEdit("HamkaryBama", config.HamkaryBama);
            await UpdateConfigForEdit("TextHome2", config.TextHome2);
            await UpdateConfigForEdit("Consulting", config.Consulting);
            await _configRepository.Save();
            return ConfigReult.Success;
        }

        public async Task<string> GetConfigValueByKey(string key)
        {
          

            var value = await _configRepository.GetConfigKey(key);
            return value == null ? "" : value.Value;
        }

        public void UpdateConfig(Config config)
        {
            _configRepository.UpdateConfig(config);
        }

        public async Task UpdateConfigForEdit(string key, string value)
        {
            var config = await GetConfigByKey(key);
            config.Value = value;
            UpdateConfig(config);
        }


    }
}
