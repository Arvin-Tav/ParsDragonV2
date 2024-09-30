using Learning.Domain.DTO.Visit;
using Learning.Domain.Models.Visit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Application.Interfaces
{
    public interface IVisitService
    {
        Task<List<Visit>> GetAllVisits();
        Task AddVisit(Visit visit);
        Task<VisitSiteDTO> GetAllVisitSilte();
    }
}
