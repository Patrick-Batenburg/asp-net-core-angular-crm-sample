using Ardalis.ApiEndpoints;
using Crm.Core.CrmAggregate;
using Crm.Core.Interfaces;
using Crm.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Crm.Web.Endpoints.VacancyEndpoints
{
    public class Delete : BaseAsyncEndpoint
        .WithRequest<DeleteVacancyRequest>
        .WithoutResponse
    {
        private readonly IRepository<Company> _repository;
        private readonly IVacancySearchService _searchService;

        public Delete(IRepository<Company> repository, IVacancySearchService searchService)
        {
            _repository = repository;
            _searchService = searchService;
        }

        [HttpDelete(DeleteVacancyRequest.Route)]
        [SwaggerOperation(
            Summary = "Deletes a Vacancy",
            Description = "Deletes a Vacancy",
            OperationId = "Company.Vacancy.Delete",
            Tags = new[] { "Companies" })
        ]
        public override async Task<ActionResult> HandleAsync([FromRoute] DeleteVacancyRequest request,
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
                company.DeleteVacancy(vacancy);
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

            return NoContent();
        }
    }
}
