using Crm.SharedKernel;

namespace Crm.Core.CrmAggregate
{
    public class Vacancy : BaseEntity
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public bool Expired { get; set; } = false;
    }
}
