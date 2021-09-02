using Ardalis.Specification;

namespace Crm.Core.CrmAggregate.Specifications
{
    public class CompanyByIdWithVacanciesSpec : Specification<Company>, ISingleResultSpecification
    {
        public CompanyByIdWithVacanciesSpec(int id)
        {
            Query
                .Where(c => c.Id == id)
                .Include(c => c.Vacancies);
        }
    }
}
