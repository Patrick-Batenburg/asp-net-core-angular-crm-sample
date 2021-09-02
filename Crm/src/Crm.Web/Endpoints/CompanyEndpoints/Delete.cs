using Ardalis.ApiEndpoints;
using Crm.Core.CrmAggregate;
using Crm.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using System.Threading.Tasks;

namespace Crm.Web.Endpoints.CompanyEndpoints
{
    public class Delete : BaseAsyncEndpoint
        .WithRequest<DeleteCompanyRequest>
        .WithoutResponse
    {
        private readonly IRepository<Company> _repository;

        public Delete(IRepository<Company> repository)
        {
            _repository = repository;
        }

        [HttpDelete(DeleteCompanyRequest.Route)]
        [SwaggerOperation(
            Summary = "Deletes a Company",
            Description = "Deletes a Company",
            OperationId = "Company.Delete",
            Tags = new[] { "Companies" })
        ]
        public override async Task<ActionResult> HandleAsync([FromRoute] DeleteCompanyRequest request,
            CancellationToken cancellationToken)
        {
            var aggregateToDelete = await _repository.GetByIdAsync(request.CompanyId, cancellationToken);

            if (aggregateToDelete == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(aggregateToDelete, cancellationToken);

            return NoContent();
        }
    }
}
