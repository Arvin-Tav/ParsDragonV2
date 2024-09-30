using Learning.Domain.DTO.Banner;
using Learning.Domain.Models.Banner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.Interfaces
{
    public interface IBannerRepository
    {
        Task<FilterBannerDTO> FilterBanners(FilterBannerDTO filter);
        Task<List<Banner>> GetBanners(int takeCount);
        Task AddBaner(Banner banner);
        void UpdateBanner(Banner banner);
        Task<Banner> GetBannerById(int bannerId);
        Task Save();
    }
}
