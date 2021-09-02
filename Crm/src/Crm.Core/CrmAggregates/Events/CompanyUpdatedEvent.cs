using Crm.SharedKernel;

namespace Crm.Core.CrmAggregate.Events
{
    public class CompanyUpdatedEvent : BaseDomainEvent
    {
        public Company Company { get; set; }

        public CompanyUpdatedEvent(Company company)
        {
            Company = company;
        }
    }
}
