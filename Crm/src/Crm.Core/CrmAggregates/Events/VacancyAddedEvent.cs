using Crm.SharedKernel;

namespace Crm.Core.CrmAggregate.Events
{
    public class VacancyAddedEvent : BaseDomainEvent
    {
        public Vacancy Vacancy { get; set; }

        public Company Company { get; set; }

        public VacancyAddedEvent(Company company, Vacancy vacancy)
        {
            Company = company;
            Vacancy = vacancy;
        }
    }
}
