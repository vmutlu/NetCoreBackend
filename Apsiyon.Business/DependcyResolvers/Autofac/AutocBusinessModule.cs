﻿using Apsiyon.Business.Abstract;
using Apsiyon.Business.Concrete;
using Apsiyon.Core.Utilities.Security.Jwt;
using Apsiyon.DataAccess.Abstract;
using Apsiyon.DataAccess.Concrete.EntityFramework;
using Autofac;

namespace Apsiyon.Business.DependcyResolvers.Autofac
{
    public class AutocBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductService>().As<IProductService>();
            builder.RegisterType<EfProductRepository>().As<IProductRepository>();

            builder.RegisterType<CategoryService>().As<ICategoryService>();
            builder.RegisterType<EfCategoryRepository>().As<ICategoryRepository>();

            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<EfUserRepository>().As<IUserRepository>();

            builder.RegisterType<AuthService>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();
        }
    }
}
