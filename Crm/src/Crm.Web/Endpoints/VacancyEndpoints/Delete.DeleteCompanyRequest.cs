namespace Crm.Web.Endpoints.VacancyEndpoints
{
    public class DeleteVacancyRequest
    {
        public const string Route = "/Companies/{CompanyId:int}/Vacancies/{VacancyId:int}";

        public static string BuildRoute(int companyId, int vacancyId) => 
            Route.Replace("{CompanyId:int}", companyId.ToString()).Replace("{VacancyId:int}", vacancyId.ToString());

        public int CompanyId { get; set; }

        public int VacancyId { get; set; }
    }
}
