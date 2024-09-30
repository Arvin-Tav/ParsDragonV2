using Learning.Domain.DTO.Banner;
using Learning.Domain.Models.Banner;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Application.Interfaces
{
    public interface IBannerService
    {
        Task<FilterBannerDTO> FilterBanner(FilterBannerDTO filter);
        Task AddBanner(Banner banner);
        Task<List<Banner>> GetBanners(int takeCount = 8);
        Task<DeleteBannerResult> DeleteBanner(int bannerId);
        Task<Banner> GetBannerById(int bannerId);
        Task<BannerResult> CreateBanner(CreateBannerDTO banner, IFormFile imageBanner);
        Task<BannerResult> EditeBaneer(EditBannerDTO banner, IFormFile imageBanner);
        Task<EditBannerDTO> GetBannerForEdit(int bannerId);
    }
}
