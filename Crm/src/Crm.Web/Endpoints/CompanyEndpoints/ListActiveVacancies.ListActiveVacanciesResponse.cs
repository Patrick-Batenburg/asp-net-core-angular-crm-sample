using Crm.Core.CrmAggregate;
using System.Collections.Generic;

namespace Crm.Web.Endpoints.CompanyEndpoints
{
    public class ListActiveVacanciesResponse
    {
        public List<CompanyRecord> Companies { get; set; } = new();
    }
}
