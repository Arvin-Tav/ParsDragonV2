using Learning.Domain.DTO.Banner;
using Learning.Domain.DTO.Paging;
using Learning.Domain.Interfaces;
using Learning.Domain.Models.Banner;
using Learning.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Infra.Data.Repositories
{
    public class BannerRepository : IBannerRepository
    {
        #region constructor
        private readonly LearningContext _context;
        public BannerRepository(LearningContext context)
        {
            _context = context;
        }
        #endregion

        #region banner
        public async Task<FilterBannerDTO> FilterBanners(FilterBannerDTO filter)
        {
            IQueryable<Banner> query = _context.Banners.AsQueryable();

            #region state
            switch (filter.FilterBannerOrder)
            {
                case FilterBannerOrder.CreateDate_DES:
                    query = query.OrderByDescending(i => i.CreateDate);
                    break;
                case FilterBannerOrder.CreateDate_ASC:
                    query = query.OrderBy(i => i.CreateDate);
                    break;
            }
            #endregion
            #region filter
            if(!string.IsNullOrEmpty(filter.Search))
                query = query.Where(i=>EF.Functions.Like(i.Name,$"%{filter.Search}%"));
            #endregion




            #region paging

            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.HowManyShowPageAfterAndBefore);
            var allEntities = await query.Paging(pager).ToListAsync();

            #endregion
            return filter.SetPaging(pager).SetBanners(allEntities);

        }
        public async Task AddBaner(Banner banner)
        {
            await _context.Banners.AddAsync(banner);
        }
        
        public async Task<List<Banner>> GetBanners(int takeCount)
        {
           return await _context.Banners.Take(takeCount).ToListAsync();
        }

        public void UpdateBanner(Banner banner)
        {
            _context.Banners.Update(banner);
        }

        public async Task<Banner> GetBannerById(int bannerId)
        {
            return await _context.Banners.AsQueryable().FirstOrDefaultAsync(i=>i.BannerId==bannerId);
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
        #endregion


    }

}
