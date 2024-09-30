using Learning.Application.Extentions;
using Learning.Application.Interfaces;
using Learning.Application.Utils;
using Learning.Domain.DTO.Banner;
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
    public class BannerService : IBannerService
    {
        #region constructor
        private readonly IBannerRepository _bannerRepository;
        public BannerService(IBannerRepository bannerRepository)
        {
            _bannerRepository = bannerRepository;
        }

        #endregion

        #region banner
        public async Task<BannerResult> CreateBanner(CreateBannerDTO banner, IFormFile imageBanner)
        {
            if (imageBanner == null) return BannerResult.NotImage;
            if (imageBanner != null && !imageBanner.IsImage()) return BannerResult.ErrorImage;
            banner.BannerImageName = TextFixer.GenerateUniqCodeString(banner.Name) + Path.GetExtension(imageBanner.FileName);
            bool result = imageBanner
                .AddImageToServer(banner.BannerImageName, PathExtensions.BannerImageOrginServer, null, null, PathExtensions.BannerImageThumbServer);
            if (!result) return BannerResult.ErrorImage;

            await AddBanner(new Banner
            {
                BannerImageName = banner.BannerImageName,
                Link = banner.Link,
                Name = banner.Name,
            });
            await _bannerRepository.Save();
            return BannerResult.Success;
        }

        public async Task<DeleteBannerResult> DeleteBanner(int bannerId)
        {
            var banner = await GetBannerById(bannerId);
            if (banner == null) return DeleteBannerResult.NotFound;
            banner.IsDelete = true;
            banner.UpdateDate = DateTime.Now;

            _bannerRepository.UpdateBanner(banner);
            await _bannerRepository.Save();
            return DeleteBannerResult.Success;
        }



        public async Task<BannerResult> EditeBaneer(EditBannerDTO banner, IFormFile imageBanner)
        {

            var getBanner = await _bannerRepository.GetBannerById(banner.BannerId);
            if (getBanner == null) return BannerResult.Error;
            if (imageBanner != null && !imageBanner.IsImage()) return BannerResult.ErrorImage;
            if (imageBanner != null)
            {
                banner.BannerImageName = TextFixer.GenerateUniqCodeString(banner.Name) + Path.GetExtension(imageBanner.FileName);
                bool result = imageBanner
                .AddImageToServer(banner.BannerImageName, PathExtensions.BannerImageOrginServer, null, null, PathExtensions.BannerImageThumbServer, getBanner.BannerImageName);
                if (!result) return BannerResult.ErrorImage;
            }
            getBanner.BannerImageName = banner.BannerImageName;
            getBanner.Link = banner.Link;
            getBanner.Name = banner.Name;
            getBanner.UpdateDate = DateTime.Now;

            _bannerRepository.UpdateBanner(getBanner);
            await _bannerRepository.Save();
            return BannerResult.Success;
        }

        public async Task<FilterBannerDTO> FilterBanner(FilterBannerDTO filter)
        {
            return await _bannerRepository.FilterBanners(filter);
        }
        
        public async Task<List<Banner>> GetBanners(int takeCount = 8)
        {
            return await _bannerRepository.GetBanners(takeCount);
        }

        public async Task<Banner> GetBannerById(int bannerId)
        {
            return await _bannerRepository.GetBannerById(bannerId);
        }

        public async Task<EditBannerDTO> GetBannerForEdit(int bannerId)
        {
            var banner = await GetBannerById(bannerId);
            if (banner == null) return null;
            return new EditBannerDTO
            {
                BannerId = banner.BannerId,
                BannerImageName = banner.BannerImageName,
                Link = banner.Link,
                Name = banner.Name,
            };
        }

        public async Task AddBanner(Banner banner)
        {
           await _bannerRepository.AddBaner(banner);
        }

        #endregion



    }
}
