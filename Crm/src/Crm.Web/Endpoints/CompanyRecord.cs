using System.Collections.Generic;

namespace Crm.Web.Endpoints
{
    public record CompanyRecord(int Id, string Name, string Street, string City, string State, string Country, string ZipCode, List<VacancyRecord> Vacancies);
}
