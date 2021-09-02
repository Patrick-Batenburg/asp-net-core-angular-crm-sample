using Ardalis.ApiEndpoints;
using Crm.Core.CrmAggregate;
using Crm.Core.Interfaces;
using Crm.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Crm.Web.Endpoints.CompanyEndpoints
{
    public class ListActiveVacancies : BaseAsyncEndpoint
        .WithoutRequest
        .WithResponse<ListActiveVacanciesResponse>
    {
        private readonly IReadRepository<Company> _repository;
        private readonly IVacancySearchService _searchService;

        public ListActiveVacancies(IReadRepository<Company> repository, IVacancySearchService searchService)
        {
            _repository = repository;
            _searchService = searchService;
        }

        [HttpGet("/Companies/ActiveVacancies")]
        [SwaggerOperation(
            Summary = "Gets a list of all companies active vacancies",
            Description = "Gets a list of all companies active vacancies",
            OperationId = "Company.ListActiveVacancies",
            Tags = new[] { "Companies" })
        ]
        public override async Task<ActionResult<ListActiveVacanciesResponse>> HandleAsync(CancellationToken cancellationToken)
        {
            var response = new ListActiveVacanciesResponse();
            var result = await _searchService.GetCompaniesWithActiveVacanciesAsync();

            if (result.Status == Ardalis.Result.ResultStatus.Ok)
            {
                response.Companies = result.Value
                .Select(c => new CompanyRecord(c.Id, c.Name, c.Address.Street, c.Address.City, c.Address.State, c.Address.Country, c.Address.ZipCode,
                    c.Vacancies.Select(v => new VacancyRecord(v.Id, v.Title, v.Description, v.Expired)).ToList()))
                    .ToList();
            }
            else if (result.Status == Ardalis.Result.ResultStatus.Invalid)
            {
                return BadRequest(result.ValidationErrors);
            }
            else if (result.Status == Ardalis.Result.ResultStatus.NotFound)
            {
                return NotFound();
            }

            return Ok(response);
        }
    }
}
