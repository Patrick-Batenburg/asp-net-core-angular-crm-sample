using Ardalis.Specification;
using Crm.Core.CrmAggregate;
using System.Linq;

namespace Crm.Core.CrmAggregates.Specifications
{
    public class ExpiredVacancySpec : Specification<Vacancy>
    {
        public ExpiredVacancySpec()
        {
            Query.Where(v => !v.Expired);
        }
    }
}
