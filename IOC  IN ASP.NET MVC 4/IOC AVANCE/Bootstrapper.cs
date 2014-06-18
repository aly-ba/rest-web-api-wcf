using System;
using System.Web.Configuration;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using MvcIoC.Models;
using Unity.Mvc4;

namespace MvcIoC
{
    public static class Bootstrapper
    {
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();    

            RegisterTypes(container);

            return container;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterTypes(
                AllClasses.FromLoadedAssemblies(),
                WithMappings.FromMatchingInterface,
                WithName.Default);

            //container.RegisterType<IProteinRepository, ProteinRepository>(new InjectionConstructor("test data source"));
            //container.RegisterInstance(typeof (IProteinRepository), new ProteinRepository("test data source 123"));

            container.RegisterType<IProteinRepository, ProteinRepository>("StandardRepository",
                                                                          new InjectionConstructor("test"));
            container.RegisterType<IProteinRepository, DebugProteinRepository>("DebugRepository");

            var repositoryType = WebConfigurationManager.AppSettings["RepositoryType"];
            container.RegisterType<IProteinTrackingService, ProteinTrackingService>(
                new InjectionConstructor(container.Resolve<IProteinRepository>(repositoryType)));

            //container.RegisterType<IProteinTrackingService, ProteinTrackingService>();
            //container.RegisterType<IProteinRepository, ProteinRepository>();
        }
    }

    public class DebugProteinRepository : IProteinRepository
    {
        public ProteinData GetData(DateTime date)
        {
            return new ProteinData();
        }

        public void SetTotal(DateTime date, int value)
        {
            //
        }

        public void SetGoal(DateTime date, int value)
        {
            //
        }
    }
}