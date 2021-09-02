using Ardalis.ApiEndpoints;
using Crm.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Crm.Web.Endpoints.VacancyEndpoints
{
    public class GetById : BaseAsyncEndpoint
        .WithRequest<GetVacancyByIdRequest>
        .WithResponse<GetVacancyByIdResponse>
    {
        private readonly IVacancySearchService _searchService;

        public GetById(IVacancySearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet(GetVacancyByIdRequest.Route)]
        [SwaggerOperation(
            Summary = "Gets a single Vacancy",
            Description = "Gets a single Vacancy by Id",
            OperationId = "Company.Vacancy.GetById",
            Tags = new[] { "Companies" })
        ]
        public override async Task<ActionResult<GetVacancyByIdResponse>> HandleAsync([FromRoute] GetVacancyByIdRequest request,
            CancellationToken cancellationToken)
        {
            var response = new GetVacancyByIdResponse();
            var result = await _searchService.GetVacancyByIdAsync(request.CompanyId, request.VacancyId);

            if (result.Value == null)
            {
                return NotFound();
            }

            var vacancy = result.Value.FirstOrDefault();

            if (result.Status == Ardalis.Result.ResultStatus.Ok)
            {
                response.Id = vacancy.Id;
                response.Title = vacancy.Title;
                response.Description = vacancy.Description;
                response.Expired = response.Expired;
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
