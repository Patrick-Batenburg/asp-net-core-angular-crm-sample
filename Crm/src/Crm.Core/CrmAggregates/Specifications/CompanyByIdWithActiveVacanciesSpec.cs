using Ardalis.Specification;
using System.Linq;

namespace Crm.Core.CrmAggregate.Specifications
{
    public class CompanyByIdWithActiveVacanciesSpec : Specification<Company>, ISingleResultSpecification
    {
        public CompanyByIdWithActiveVacanciesSpec(int id)
        {
            Query
                .Where(c => c.Id == id)
                .Include(c => c.Vacancies.Where(v => !v.Expired));
        }
    }
}
