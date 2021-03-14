using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;  //.GetService<ICacheManager>() hata veriyorsa bu using eklenecek

namespace Core.Aspects.Autofac.Caching
{
    //15. derste aşağıdaki adresten eklendi.
    //https://github.com/engindemirog/NetCoreBackend/blob/master/Core/Aspects/Autofac/Caching/CacheRemoveAspect.cs
    public class CacheRemoveAspect : MethodInterception
    {
        private string _pattern;
        private ICacheManager _cacheManager;

        public CacheRemoveAspect(string pattern)
        {
            _pattern = pattern;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        protected override void OnSuccess(IInvocation invocation)
        {
            _cacheManager.RemoveByPattern(_pattern);
        }
    }
}
