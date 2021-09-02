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
    public class List : BaseAsyncEndpoint
        .WithoutRequest
        .WithResponse<CompanyListResponse>
    {
        private readonly IReadRepository<Company> _repository;

        public List(IReadRepository<Company> repository)
        {
            _repository = repository;
        }

        [HttpGet("/Companies")]
        [SwaggerOperation(
            Summary = "Gets a list of all Companies",
            Description = "Gets a list of all Companies",
            OperationId = "Company.List",
            Tags = new[] { "Companies" })
        ]
        public override async Task<ActionResult<CompanyListResponse>> HandleAsync(CancellationToken cancellationToken)
        {
            var spec = new CompanyWithVacanciesSpec();

            var response = new CompanyListResponse
            {
                Companies = (await _repository.ListAsync(spec, cancellationToken))
                .Select(c => new CompanyRecord(c.Id, c.Name, c.Address.Street, c.Address.City, c.Address.State, c.Address.Country, c.Address.ZipCode, 
                    c.Vacancies.Select(v => new VacancyRecord(v.Id, v.Title, v.Description, v.Expired)).ToList()))
                    .ToList()
            };

            return Ok(response);
        }
    }
}
