using Ardalis.Specification;
using System.Linq;

namespace Crm.Core.CrmAggregate.Specifications
{
    public class CompanyGetVacancyByIdSpec : Specification<Company>, ISingleResultSpecification
    {
        public CompanyGetVacancyByIdSpec(int companyId, int vacancyId)
        {
            Query
                .Where(c => c.Id == companyId)
                .Include(c => c.Vacancies.Where(v => v.Id == vacancyId));
        }
    }
}
