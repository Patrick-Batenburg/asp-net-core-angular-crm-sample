using Crm.Core.CrmAggregate;
using Crm.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Crm.Web
{
    public static class SeedData
    {
        public static readonly Company TestCompany1 = new("Test Company", "Steet 1", "City 1", "State 1", "Country 1", "ZipCode 1");
        public static readonly Vacancy Vacancy1 = new()
        {
            Title = "Get Sample Working",
            Description = "Try to get the sample to build."
        };
        public static readonly Vacancy Vacancy2 = new()
        {
            Title = "Review Solution",
            Description = "Review the different projects in the solution and how they relate to one another."
        };
        public static readonly Vacancy Vacancy3 = new()
        {
            Title = "Run and Review Tests",
            Description = "Make sure all the tests run and review what they are doing."
        };

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var dbContext = new AppDbContext(serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>(), null);

            // Look for any TODO items.
            if (dbContext.Vacancies.Any())
            {
                return;   // DB has been seeded
            }

            PopulateTestData(dbContext);
        }
        public static void PopulateTestData(AppDbContext dbContext)
        {
            foreach (var item in dbContext.Vacancies)
            {
                dbContext.Remove(item);
            }
            dbContext.SaveChanges();

            TestCompany1.AddVacancy(Vacancy1);
            TestCompany1.AddVacancy(Vacancy2);
            TestCompany1.AddVacancy(Vacancy3);
            dbContext.Companies.Add(TestCompany1);

            dbContext.SaveChanges();
        }
    }
}
