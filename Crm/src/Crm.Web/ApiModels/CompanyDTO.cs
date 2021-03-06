using Crm.Core.CrmAggregate;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Crm.Web.ApiModels
{
    // ApiModel DTOs are used by ApiController classes and are typically kept in a side-by-side folder
    public class CompanyDTO : CreateCompanyDTO
    {
        public int Id { get; set; }
        public List<VacancyDTO> Vacancies = new();

        public static CompanyDTO FromCompany(Company company)
        {
            return new CompanyDTO()
            {
                Id = company.Id,
                Name = company.Name,
                Street = company.Address.Street,
                City = company.Address.City,
                State = company.Address.State,
                Country = company.Address.Country,
                ZipCode = company.Address.ZipCode,
                Vacancies = company.Vacancies.Select(v => VacancyDTO.FromVacancy(v)).ToList()
            };
        }
    }

    // Creation DTOs should not include an ID if the ID will be generated by the back end
    public class CreateCompanyDTO
    {
        [Required]
        public string Name { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
    }
}
