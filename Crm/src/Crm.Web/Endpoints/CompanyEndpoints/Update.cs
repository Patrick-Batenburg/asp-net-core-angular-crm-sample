using Ardalis.ApiEndpoints;
using Crm.Core.CrmAggregate;
using Crm.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Crm.Web.Endpoints.CompanyEndpoints
{
    public class Update : BaseAsyncEndpoint
        .WithRequest<UpdateCompanyRequest>
        .WithResponse<UpdateCompanyResponse>
    {
        private readonly IRepository<Company> _repository;

        public Update(IRepository<Company> repository)
        {
            _repository = repository;
        }

        [HttpPut(UpdateCompanyRequest.Route)]
        [SwaggerOperation(
            Summary = "Updates a Company",
            Description = "Updates a Company",
            OperationId = "Company.Update",
            Tags = new[] { "Companies" })
        ]
        public override async Task<ActionResult<UpdateCompanyResponse>> HandleAsync(UpdateCompanyRequest request,
            CancellationToken cancellationToken)
        {
            var existingCompany = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (existingCompany == null)
            {
                return NotFound();
            }

            existingCompany.UpdateName(request.Name);
            existingCompany.UpdateAddress(request.Street, request.City, request.State, request.Country, request.ZipCode);

            await _repository.UpdateAsync(existingCompany, cancellationToken);

            var response = new UpdateCompanyResponse()
            {
                Company = new CompanyRecord(existingCompany.Id, existingCompany.Name, existingCompany.Address.Street, 
                existingCompany.Address.City, existingCompany.Address.State, existingCompany.Address.Country, existingCompany.Address.ZipCode,
                    existingCompany.Vacancies.Select(v => new VacancyRecord(v.Id, v.Title, v.Description, v.Expired)).ToList())
            };

            return Ok(response);
        }
    }
}
