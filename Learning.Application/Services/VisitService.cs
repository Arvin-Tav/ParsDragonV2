using Learning.Application.Interfaces;
using Learning.Domain.DTO.Visit;
using Learning.Domain.Interfaces;
using Learning.Domain.Models.Visit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Application.Services
{
    public class VisitService : IVisitService
    {
        #region constructor
        private readonly IVisitRepository _visitRepository;
        public VisitService(IVisitRepository visitRepository)
        {
            _visitRepository = visitRepository;
        }
        #endregion
        public async Task AddVisit(Visit visit)
        {
            await _visitRepository.AddVisit(visit);
            await _visitRepository.Save();
        }
        public async Task<VisitSiteDTO> GetAllVisitSilte()
        {
            DateTime dtNow = DateTime.Now.Date;
            DateTime yesterday = dtNow.AddDays(-1).Date;
            List<Visit> selectAllVisit = await GetAllVisits();
            VisitSiteDTO visit = new VisitSiteDTO();
            visit.VisitSum = selectAllVisit.Count();
            visit.VisitToday = selectAllVisit.Count(v => v.Date.Date == dtNow);
            visit.VisitYesterday = selectAllVisit.Count(v => v.Date.Date == yesterday);
            return visit;
        }


        public Task<List<Visit>> GetAllVisits()
        {
            return _visitRepository.GetAllVisits();
        }


    }
}
