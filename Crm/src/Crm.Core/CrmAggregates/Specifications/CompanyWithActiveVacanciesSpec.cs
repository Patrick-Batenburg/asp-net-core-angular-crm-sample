using Ardalis.Specification;
using System.Linq;

namespace Crm.Core.CrmAggregate.Specifications
{
    public class CompanyWithActiveVacanciesSpec : Specification<Company>, ISingleResultSpecification
    {
        public CompanyWithActiveVacanciesSpec()
        {
            Query.Include(c => c.Vacancies.Where(v => !v.Expired));
        }
    }
}
