using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Crm.Web.Endpoints.VacancyEndpoints
{
    public class UpdateVacancyRequest
    {
        public const string Route = "/Companies/{CompanyId:int}/Vacancies/{VacancyId:int}";

        public static string BuildRoute(int companyId, int vacancyId) =>
            Route.Replace("{CompanyId:int}", companyId.ToString()).Replace("{VacancyId:int}", vacancyId.ToString());

        [FromRoute]
        public int CompanyId { get; set; }

        [FromRoute]
        public int VacancyId { get; set; }

        [FromBody]
        public UpdateVacancyRequestBody Body { get; set; }

        public class UpdateVacancyRequestBody
        {
            public string Title { get; set; }

            public string Description { get; set; }

            public bool Expired { get; set; }
        }
    }
}