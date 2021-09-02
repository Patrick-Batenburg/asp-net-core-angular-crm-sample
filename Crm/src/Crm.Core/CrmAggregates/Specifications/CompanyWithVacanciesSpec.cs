using Ardalis.Specification;
using System.Linq;

namespace Crm.Core.CrmAggregate.Specifications
{
    public class CompanyWithVacanciesSpec : Specification<Company>, ISingleResultSpecification
    {
        public CompanyWithVacanciesSpec()
        {
            Query.Include(c => c.Vacancies);
        }
    }
}
