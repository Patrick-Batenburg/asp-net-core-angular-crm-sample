using Ardalis.Result;
using Crm.Core.CrmAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Crm.Core.Interfaces
{
    public interface IVacancySearchService
    {
        Task<Result<List<Vacancy>>> GetActiveVacanciesByIdAsync(int companyId);

        Task<Result<List<Vacancy>>> GetVacanciesByIdAsync(int companyId);

        Task<Result<List<Company>>> GetCompaniesWithActiveVacanciesAsync();

        Task<Result<List<Vacancy>>> GetVacancyByIdAsync(int companyId, int vacancyId);
    }
}
