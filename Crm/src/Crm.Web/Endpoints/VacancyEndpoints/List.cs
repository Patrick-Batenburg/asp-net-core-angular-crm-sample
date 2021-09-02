using Ardalis.ApiEndpoints;
using Crm.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Crm.Web.Endpoints.VacancyEndpoints
{
    public class List : BaseAsyncEndpoint
        .WithRequest<VacancyListRequest>
        .WithResponse<VacancyListResponse>
    {
        private readonly IVacancySearchService _searchService;

        public List(IVacancySearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet(VacancyListRequest.Route)]
        [SwaggerOperation(
            Summary = "Gets a list of all Vacancies of a Company",
            Description = "Gets a list of all Vacancies of a Company",
            OperationId = "Company.Vacancy.List",
            Tags = new[] { "Companies" })
        ]
        public override async Task<ActionResult<VacancyListResponse>> HandleAsync([FromRoute] VacancyListRequest request, CancellationToken cancellationToken)
        {
            var response = new VacancyListResponse();
            var result = await _searchService.GetVacanciesByIdAsync(request.CompanyId);
            var vacancy = result.Value.FirstOrDefault();

            if (result.Status == Ardalis.Result.ResultStatus.Ok)
            {
                response.Vacancies = new List<VacancyRecord>(
                        result.Value.Select(
                            v => new VacancyRecord(v.Id,
                            v.Title,
                            v.Description,
                            v.Expired)));
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
