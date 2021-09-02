using System.Collections.Generic;

namespace Crm.Web.Endpoints.CompanyEndpoints
{
    public class GetCompanyByIdResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string ZipCode { get; set; }

        public List<VacancyRecord> Vacancies { get; set; } = new List<VacancyRecord>();
    }
}