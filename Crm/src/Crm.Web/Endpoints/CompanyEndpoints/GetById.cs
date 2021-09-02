using Ardalis.ApiEndpoints;
using Crm.Core.CrmAggregate;
using Crm.Core.CrmAggregate.Specifications;
using Crm.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Crm.Web.Endpoints.CompanyEndpoints
{
    public class GetById : BaseAsyncEndpoint
        .WithRequest<GetCompanyByIdRequest>
        .WithResponse<GetCompanyByIdResponse>
    {
        private readonly IRepository<Company> _repository;

        public GetById(IRepository<Company> repository)
        {
            _repository = repository;
        }

        [HttpGet(GetCompanyByIdRequest.Route)]
        [SwaggerOperation(
            Summary = "Gets a single Company",
            Description = "Gets a single Company by Id",
            OperationId = "Company.GetById",
            Tags = new[] { "Companies" })
        ]
        public override async Task<ActionResult<GetCompanyByIdResponse>> HandleAsync([FromRoute] GetCompanyByIdRequest request,
            CancellationToken cancellationToken)
        {
            var spec = new CompanyByIdWithVacanciesSpec(request.CompanyId);
            var company = await _repository.GetBySpecAsync(spec, cancellationToken);

            if (company == null)
            {
                return NotFound();
            }

            var response = new GetCompanyByIdResponse()
            {
                Id = company.Id,
                Name = company.Name,
                Street = company.Address.Street,
                City = company.Address.City,
                State = company.Address.State,
                Country = company.Address.Country,
                ZipCode = company.Address.ZipCode,
                Vacancies = company.Vacancies.Select(v => new VacancyRecord(v.Id, v.Title, v.Description, v.Expired)).ToList()
            };

            return Ok(response);
        }
    }
}
