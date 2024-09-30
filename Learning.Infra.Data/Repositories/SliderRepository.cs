using Learning.Domain.DTO.Paging;
using Learning.Domain.DTO.Slider;
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
    public class SliderRepository : ISliderRepository
    {
        #region constructor
        private readonly LearningContext _context;
        public SliderRepository(LearningContext context)
        {
            _context = context;
        }

        #endregion
        public async Task AddSlider(Slider slider)
        {
            await _context.Sliders.AddAsync(slider);
        }

        public async Task<List<Slider>> GetSliders(int takeCount=8)
        {
            return await _context.Sliders.OrderByDescending(i => i.CreateDate).Take(takeCount).ToListAsync();
        }



        public async Task<FilterSliderDTO> FilterSlider(FilterSliderDTO filter)
        {
            IQueryable<Slider> query = _context.Sliders.AsQueryable()
                .Include(i => i.SliderGroup)
                .Include(i => i.SliderSubGroup);
            #region state
            switch (filter.FilterSliderOrder)
            {
                case FilterSliderOrder.CreateDate_DES:
                    query = query.OrderByDescending(i => i.CreateDate);
                    break;
                case FilterSliderOrder.CreateDate_ASC:
                    query = query.OrderBy(i => i.CreateDate);
                    break;
            }
            #endregion

            #region paging

            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.HowManyShowPageAfterAndBefore);
            var allEntities = await query.Paging(pager).ToListAsync();

            #endregion
            return filter.SetPaging(pager).SetSliders(allEntities);
        }

        public async Task<Slider> GetSliderById(int sliderId)
        {
            return await _context.Sliders.AsQueryable().FirstOrDefaultAsync(i => i.SliderId == sliderId);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public void UpdateSlider(Slider slider)
        {
            _context.Sliders.Update(slider);
        }

    }
}
