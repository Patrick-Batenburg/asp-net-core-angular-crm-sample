using Crm.SharedKernel;

namespace Crm.Core.CrmAggregate.Events
{
    public class VacancyDeletedEvent : BaseDomainEvent
    {
        public Vacancy Vacancy { get; set; }

        public Company Company { get; set; }

        public VacancyDeletedEvent(Company company, Vacancy vacancy)
        {
            Company = company;
            Vacancy = vacancy;
        }
    }
}
