using Crm.Core.CrmAggregate;
using System.Collections.Generic;

namespace Crm.Web.Endpoints.VacancyEndpoints
{
    public class VacancyListResponse
    {
        public List<VacancyRecord> Vacancies { get; set; } = new();
    }
}
