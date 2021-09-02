namespace Crm.Web.Endpoints.VacancyEndpoints
{
    public class GetVacancyByIdResponse
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool Expired { get; set; }
    }
}