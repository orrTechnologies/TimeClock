using System.Reflection;
using System.Web.Mvc;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using TimeClock.Data;
using TimeClock.Web.Services;

namespace TimeClock.Web
{
    internal static class SimpleInject
    {
        internal static void Application_Start()
        {
            // Create the container as usual.
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            // Register your types, for instance:
            container.Register<TimeClockContext, TimeClockContext>(Lifestyle.Scoped);
            container.Register<IEmployeeService, EmployeeService>(Lifestyle.Scoped);
            container.Register<ITimeService, TimeService>(Lifestyle.Scoped);
            // This is an extension method from the integration package.
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            // This is an extension method from the integration package as well.
            container.RegisterMvcIntegratedFilterProvider();

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}