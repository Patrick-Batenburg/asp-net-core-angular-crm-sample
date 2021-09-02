using Crm.SharedKernel;

namespace Crm.Core.CrmAggregate.Events
{
    public class VacancyUpdatedEvent : BaseDomainEvent
    {
        public Vacancy Vacancy { get; set; }

        public Company Company { get; set; }

        public VacancyUpdatedEvent(Company company, Vacancy vacancy)
        {
            Company = company;
            Vacancy = vacancy;
        }
    }
}
