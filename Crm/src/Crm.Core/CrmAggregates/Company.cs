using Ardalis.GuardClauses;
using Crm.Core.CrmAggregate.Events;
using Crm.Core.ValueObjects;
using Crm.SharedKernel;
using Crm.SharedKernel.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Crm.Core.CrmAggregate
{
    public class Company : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; }

        public Address Address { get; set; }

        private List<Vacancy> _vacancies = new();
        public IEnumerable<Vacancy> Vacancies => _vacancies.AsReadOnly();

        public Company(string name)
        {
            Name = Guard.Against.NullOrEmpty(name, nameof(name));
        }

        public Company(string name, string street, string city, string state, string country, string zipcode)
        {
            Name = Guard.Against.NullOrEmpty(name, nameof(name));
            Address = new Address(street, city, state, country, zipcode);
        }

        public void AddVacancy(Vacancy vacancy)
        {
            Guard.Against.Null(vacancy, nameof(vacancy));
            _vacancies.Add(vacancy);

            var vacancyAddedEvent = new VacancyAddedEvent(this, vacancy);
            Events.Add(vacancyAddedEvent);
        }

        public void UpdateVacancy(Vacancy updatedVacancy)
        {
            Guard.Against.Null(updatedVacancy, nameof(updatedVacancy));
            var vacancy = _vacancies.FirstOrDefault(v => v.Id == updatedVacancy.Id);

            vacancy.Title = updatedVacancy.Title;
            vacancy.Description = updatedVacancy.Description;
            vacancy.Expired = updatedVacancy.Expired;

            var vacancyUpdatedEvent = new VacancyUpdatedEvent(this, vacancy);
            Events.Add(vacancyUpdatedEvent);
        }

        public void DeleteVacancy(Vacancy vacancy)
        {
            Guard.Against.Null(vacancy, nameof(vacancy));
            _vacancies.Remove(vacancy);

            var vacancyDeletedEvent = new VacancyDeletedEvent(this, vacancy);
            Events.Add(vacancyDeletedEvent);
        }

        public void UpdateName(string name)
        {
            Name = Guard.Against.NullOrEmpty(name, nameof(name));

            var companyUpdatedEvent = new CompanyUpdatedEvent(this);
            Events.Add(companyUpdatedEvent);
        }

        public void UpdateAddress(string street, string city, string state, string country, string zipcode)
        {
            Address = new Address(street, city, state, country, zipcode);

            var companyUpdatedEvent = new CompanyUpdatedEvent(this);
            Events.Add(companyUpdatedEvent);
        }
    }
}
