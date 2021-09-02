using Ardalis.ApiEndpoints;
using Crm.Core.CrmAggregate;
using Crm.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using System.Threading.Tasks;

namespace Crm.Web.Endpoints.CompanyEndpoints
{
    public class Create : BaseAsyncEndpoint
        .WithRequest<CreateCompanyRequest>
        .WithResponse<CreateCompanyResponse>
    {
        private readonly IRepository<Company> _repository;

        public Create(IRepository<Company> repository)
        {
            _repository = repository;
        }

        [HttpPost("/Companies")]
        [SwaggerOperation(
            Summary = "Creates a new Company",
            Description = "Creates a new Company",
            OperationId = "Company.Create",
            Tags = new[] { "Companies" })
        ]
        public override async Task<ActionResult<CreateCompanyResponse>> HandleAsync(CreateCompanyRequest request,
            CancellationToken cancellationToken)
        {
            var newCompany = new Company(request.Name, request.Street, request.City, request.State, request.Country, request.ZipCode);
            var createdCompany = await _repository.AddAsync(newCompany, cancellationToken);

            var response = new CreateCompanyResponse()
            {
                Id = createdCompany.Id,
                Name = createdCompany.Name,
                Street = createdCompany.Address.Street,
                City = createdCompany.Address.City,
                State = createdCompany.Address.State,
                Country = createdCompany.Address.Country,
                ZipCode = createdCompany.Address.ZipCode
            };

            return Ok(response);
        }
    }
}
