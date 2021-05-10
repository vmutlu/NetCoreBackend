﻿using Apsiyon.Core.Extensions;
using Apsiyon.Core.Utilities.Interceptors;
using Apsiyon.Core.Utilities.IoC;
using Apsiyon.Core.Utilities.Messages;
using Castle.DynamicProxy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Apsiyon.Core.Aspects.Autofac.UsersAspect
{
    public class SecuredOperation : MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;

        public SecuredOperation(string roles)
        {
            _roles = roles.Split(',');
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            foreach (var role in _roles)
                if (roleClaims.Contains(role))
                    return;

            throw new System.Exception(AuthMessages.AuthorizationDenied);
        }
    }
}
