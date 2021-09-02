using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Crm.Web.Endpoints.VacancyEndpoints
{
    public class CreateVacancyRequest
    {
        [FromRoute(Name = "companyId")] 
        public int CompanyId { get; set; }

        [FromBody]
        public CreateVacancyRequestBody Body { get; set; }

        public class CreateVacancyRequestBody
        {
            public string Title { get; set; }

            public string Description { get; set; }

            public bool Expired { get; set; }
        }
    }
}
