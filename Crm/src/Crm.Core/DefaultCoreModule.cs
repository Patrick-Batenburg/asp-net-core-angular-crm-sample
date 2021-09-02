using Autofac;
using Crm.Core.Interfaces;
using Crm.Core.Services;

namespace Crm.Core
{
    public class DefaultCoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<VacanciesSearchService>()
                .As<IVacancySearchService>().InstancePerLifetimeScope();
        }
    }
}
