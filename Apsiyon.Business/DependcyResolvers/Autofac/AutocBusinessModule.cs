using Apsiyon.Business.Abstract;
using Apsiyon.Business.Concrete;
using Apsiyon.DataAccess.Abstract;
using Apsiyon.DataAccess.Concrete.EntityFramework;
using Apsiyon.Utilities.Interceptors;
using Apsiyon.Utilities.Security.Jwt;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Microsoft.AspNetCore.Http;

namespace Apsiyon.Business.DependcyResolvers.Autofac
{
    public class AutocBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EfProductRepository>().As<IProductRepository>();
            builder.RegisterType<ProductService>().As<IProductService>();

            builder.RegisterType<EfCategoryRepository>().As<ICategoryRepository>();
            builder.RegisterType<CategoryService>().As<ICategoryService>();

            builder.RegisterType<EfUserRepository>().As<IUserRepository>();
            builder.RegisterType<UserService>().As<IUserService>();

            builder.RegisterType<EfUserOperationClaimRepository>().As<IUserOperationClaimRepository>();
            builder.RegisterType<UserOperationClaimService>().As<IUserOperationClaimService>();

            builder.RegisterType<EfOperationClaimRepository>().As<IOperationClaimRepository>();
            builder.RegisterType<OperationClaimService>().As<IOperationClaimService>();

            builder.RegisterType<AuthService>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}
