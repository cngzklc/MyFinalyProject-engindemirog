using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection; //.GetService<ICacheManager>() hata veriyorsa bu using eklenecek

namespace Core.Aspects.Autofac.Caching
{

    //15. derste eklendi
    public class CacheAspect : MethodInterception
    {
        private int _duration;
        private ICacheManager _cacheManager;

        public CacheAspect(int duration = 60)
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        public override void Intercept(IInvocation invocation)
        {
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");
            var arguments = invocation.Arguments.ToList();
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";
            //x?.ToString() => soru işareti(?) parametre string'e çevrilebiliyorsa demek.
            // x?.ToString() ?? "<Null>" => çift soru işareti(??) ise varsa x?.ToString(), yoksa "<Null>" u ekle demek
            //arguments.Select => parametreleri listeye çevirir.
            //string.Join("," => ise virgül ile yanyana getirir.
            if (_cacheManager.IsAdd(key))
            {
                //invocation.ReturnValue => Metodun return değerini cache'deki datadan getir demek
                invocation.ReturnValue = _cacheManager.Get(key);
                return;
            }
            invocation.Proceed();
            _cacheManager.Add(key, invocation.ReturnValue, _duration);
        }
    }
}
