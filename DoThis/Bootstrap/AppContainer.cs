using Autofac;
using Beeffective.Data;
using Beeffective.Data.Entities;
using Beeffective.Services;
using Beeffective.ViewModels;
using Beeffective.Views;

namespace Beeffective.Bootstrap
{
    public class AppContainer
    {
        private readonly IContainer container;

        public AppContainer()
        {
            var builder = new ContainerBuilder();

            // views
            builder.RegisterType<HoneycombWindow>().AsSelf().SingleInstance();

            // view models
            builder.RegisterType<HoneycombViewModel>().AsSelf().SingleInstance();

            // services
            builder.RegisterType<HoneycombService>().AsSelf().SingleInstance();

            // repository
            builder.RegisterType<CellRepository>().As<IRepository<CellEntity>>().SingleInstance();

            // database
            builder.RegisterType<Database>().AsSelf().SingleInstance();

            container = builder.Build();
        }

        public T Resolve<T>() => container.Resolve<T>();
    }

}
