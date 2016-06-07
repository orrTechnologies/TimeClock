using System.Web.Http;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using Timeclock.Services;
using TimeClock.Data;
using TimeClock.Web.Services;

namespace Timeclock.Api
{
    public static class SimpleInjectorConfig
    {
        public static void RegisterDependencies()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();

            // Register your types, for instance using the scoped lifestyle:
            container.Register<TimeClockContext, TimeClockContext>(Lifestyle.Scoped);
            container.Register<IEmployeeService, EmployeeService>(Lifestyle.Scoped);
            container.Register<ITimeService, TimeService>(Lifestyle.Scoped);
            container.Register<IReportService, ReportService>(Lifestyle.Scoped);

            // This is an extension method from the integration package.
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
}