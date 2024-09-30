using Learning.Domain.Models.Visit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.Interfaces
{
    public interface IVisitRepository
    {
        Task<List<Visit>> GetAllVisits();
        Task AddVisit(Visit visit);
        Task Save();
    }
}
