namespace Crm.Web.Endpoints.VacancyEndpoints
{
    public class VacancyListRequest
    {
        public const string Route = "/Companies/{CompanyId:int}/Vacancies";

        public static string BuildRoute(int companyId) => Route.Replace("{CompanyId:int}", companyId.ToString());

        public int CompanyId { get; set; }
    }
}
