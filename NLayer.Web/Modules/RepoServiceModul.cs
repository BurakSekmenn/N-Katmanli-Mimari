using System.Reflection;
using Autofac;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Repository.Context;
using NLayer.Repository.Repositories;
using NLayer.Repository.UnitOfWorks;
using NLayer.Service.Mapping;
using NLayer.Service.Services;
using Module = Autofac.Module;

namespace NLayer.Web.Modules
{
    public class RepoServiceModul:Module
    {
        /// <summary>
        ///  biz burada autofac modulunu kullanarak servis ve repositoryleri register edeceğiz
        ///  program.cs dosyasında yaptığımız işlemleri burada yapacağız
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {


            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>)).InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWorks>().As<IUnitOfWorks>();


            var ApiAssembly = Assembly.GetExecutingAssembly();

            var RepositoryAssembly = Assembly.GetAssembly(typeof(AppDbContext));

            var serviceAssembly = Assembly.GetAssembly(typeof(MapProfile));





            builder.RegisterAssemblyTypes(ApiAssembly, RepositoryAssembly, serviceAssembly)
                .Where(x => x.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(ApiAssembly, RepositoryAssembly, serviceAssembly)
                .Where(x => x.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            //builder.RegisterType<ProductServiceWithCaching>().As<IProductServices>();

            // InstancePerLifetimeScope => Scope
            // InstancePerDependency => Transient


        }

    }
}
