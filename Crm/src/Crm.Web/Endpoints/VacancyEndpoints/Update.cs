using Ardalis.ApiEndpoints;
using Crm.Core.CrmAggregate;
using Crm.Core.Interfaces;
using Crm.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Crm.Web.Endpoints.VacancyEndpoints
{
    public class Update : BaseAsyncEndpoint
        .WithRequest<UpdateVacancyRequest>
        .WithResponse<UpdateVacancyResponse>
    {
        private readonly IRepository<Company> _repository;
        private readonly IVacancySearchService _searchService;

        public Update(IRepository<Company> repository, IVacancySearchService searchService)
        {
            _repository = repository;
            _searchService = searchService;
        }


        [HttpPut(UpdateVacancyRequest.Route)]
        [SwaggerOperation(
            Summary = "Updates a Vacancy",
            Description = "Updates a Vacancy",
            OperationId = "Company.Vacancy.Update",
            Tags = new[] { "Companies" })
        ]
        public override async Task<ActionResult<UpdateVacancyResponse>> HandleAsync([FromRoute] UpdateVacancyRequest request,
            CancellationToken cancellationToken)
        {
            var company = await _repository.GetByIdAsync(request.CompanyId, cancellationToken);
            var result = await _searchService.GetVacancyByIdAsync(request.CompanyId, request.VacancyId);

            if (result.Value == null)
            {
                return NotFound();
            }

            var vacancy = result.Value.FirstOrDefault();

            if (result.Status == Ardalis.Result.ResultStatus.Ok)
            {
                vacancy.Title = request.Body.Title;
                vacancy.Description = request.Body.Description;
                vacancy.Expired = request.Body.Expired;
                await _repository.UpdateAsync(company);
            }
            else if (result.Status == Ardalis.Result.ResultStatus.Invalid)
            {
                return BadRequest(result.ValidationErrors);
            }
            else if (result.Status == Ardalis.Result.ResultStatus.NotFound)
            {
                return NotFound();
            }

            var response = new UpdateVacancyResponse()
            {
                Id = vacancy.Id,
                Title = vacancy.Title,
                Description = vacancy.Description,
                Expired = vacancy.Expired
            };

            return Ok(response);
        }
    }
}
