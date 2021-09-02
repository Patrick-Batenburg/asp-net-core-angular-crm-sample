using System.ComponentModel.DataAnnotations;

namespace Crm.Web.Endpoints.CompanyEndpoints
{
    public class UpdateCompanyRequest
    {
        public const string Route = "/Companies";

        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string ZipCode { get; set; }
    }
}