using Learning.Domain.Interfaces;
using Learning.Domain.Models.Visit;
using Learning.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Infra.Data.Repositories
{
    public class VisitRepository : IVisitRepository
    {
        #region constructor
        private readonly LearningContext _context;
        public VisitRepository(LearningContext context)
        {
            _context = context;
        }

        #endregion
        public async Task AddVisit(Visit visit)
        {
           await _context.Visits.AddAsync(visit);
        }
        
        public async Task Save()
        {
           await _context.SaveChangesAsync();
        }

        public async Task<List<Visit>> GetAllVisits()
        {
            return await _context.Visits.ToListAsync();
        }

    }
}
