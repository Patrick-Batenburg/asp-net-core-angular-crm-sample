using Ardalis.ApiEndpoints;
using Crm.Core.CrmAggregate;
using Crm.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using System.Threading.Tasks;

namespace Crm.Web.Endpoints.VacancyEndpoints
{
    public class Create : BaseAsyncEndpoint
        .WithRequest<CreateVacancyRequest>
        .WithResponse<CreateVacancyResponse>
    {
        private readonly IRepository<Company> _repository;

        public Create(IRepository<Company> repository)
        {
            _repository = repository;
        }

        [HttpPost("/Companies/{companyId}/Vacancies")]
        [SwaggerOperation(
            Summary = "Creates a new Vacancy",
            Description = "Creates a new Vacancy",
            OperationId = "Company.Vacancy.Create",
            Tags = new[] { "Companies" })
        ]
        public override async Task<ActionResult<CreateVacancyResponse>> HandleAsync([FromRoute] CreateVacancyRequest request,
            CancellationToken cancellationToken)
        {
            var company = await _repository.GetByIdAsync(request.CompanyId);

            if(company == null)
            {
                return NotFound();
            }

            var newVacancy = new Vacancy()
            {
                Title = request.Body.Title,
                Description = request.Body.Description,
                Expired = request.Body.Expired
            };

            company.AddVacancy(newVacancy);

            await _repository.UpdateAsync(company, cancellationToken);

            var response = new CreateVacancyResponse()
            {
                Id = newVacancy.Id,
                Title = newVacancy.Title,
                Description = newVacancy.Description,
                Expired = newVacancy.Expired
            };

            return Ok(response);
        }

    }
}
