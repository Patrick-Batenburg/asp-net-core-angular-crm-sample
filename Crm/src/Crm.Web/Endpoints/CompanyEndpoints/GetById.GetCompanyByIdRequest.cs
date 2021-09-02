namespace Crm.Web.Endpoints.CompanyEndpoints
{
    public class GetCompanyByIdRequest
    {
        public const string Route = "/Companies/{CompanyId:int}";

        public static string BuildRoute(int companyId) => Route.Replace("{CompanyId:int}", companyId.ToString());

        public int CompanyId { get; set; }
    }
}
