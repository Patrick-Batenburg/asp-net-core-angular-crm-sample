using Ardalis.Result;
using Crm.Core.Interfaces;
using Crm.Core.CrmAggregate;
using Crm.Core.CrmAggregate.Specifications;
using Crm.SharedKernel.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crm.Core.CrmAggregates.Specifications;

namespace Crm.Core.Services
{
    public class VacanciesSearchService : IVacancySearchService
    {
        private readonly IRepository<Company> _repository;

        public VacanciesSearchService(IRepository<Company> repository)
        {
            _repository = repository;
        }

        public async Task<Result<List<Vacancy>>> GetActiveVacanciesByIdAsync(int companyId)
        {
            var spec = new CompanyByIdWithActiveVacanciesSpec(companyId);
            var company = await _repository.GetBySpecAsync(spec);

            if (company == null)
            {
                return Result<List<Vacancy>>.NotFound();
            }

            if (!company.Vacancies.Any())
            {
                return Result<List<Vacancy>>.NotFound();
            }

            var epiredVacancySpec = new ExpiredVacancySpec();
            var vacancies = epiredVacancySpec.Evaluate(company.Vacancies).ToList();

            return new Result<List<Vacancy>>(vacancies);
        }

        public async Task<Result<List<Vacancy>>> GetVacanciesByIdAsync(int companyId)
        {
            var spec = new CompanyByIdWithVacanciesSpec(companyId);
            var company = await _repository.GetBySpecAsync(spec);

            if (company == null)
            {
                return Result<List<Vacancy>>.NotFound();
            }

            var vacancies = company.Vacancies.ToList();

            if (!vacancies.Any())
            {
                return Result<List<Vacancy>>.NotFound();
            }

            return new Result<List<Vacancy>>(vacancies);
        }

        public async Task<Result<List<Vacancy>>> GetVacancyByIdAsync(int companyId, int vacancyId)
        {
            var spec = new CompanyGetVacancyByIdSpec(companyId, vacancyId);
            var company = await _repository.GetBySpecAsync(spec);

            if (company == null)
            {
                return Result<List<Vacancy>>.NotFound();
            }

            var vacancies = company.Vacancies.ToList();

            if (!vacancies.Any())
            {
                return Result<List<Vacancy>>.NotFound();
            }

            return new Result<List<Vacancy>>(vacancies);
        }

        public async Task<Result<List<Company>>> GetCompaniesWithActiveVacanciesAsync()
        {
            var companies = await _repository.ListAsync();

            foreach (var company in companies.ToList())
            {
                var spec = new CompanyByIdWithActiveVacanciesSpec(company.Id);
                await _repository.GetBySpecAsync(spec);

                if (!company.Vacancies.Any())
                {
                    companies.Remove(company);
                }
            }

            return new Result<List<Company>>(companies);
        }
    }
}
