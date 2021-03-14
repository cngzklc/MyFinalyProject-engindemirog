using Castle.DynamicProxy;
using Core.Aspects.Autofac.Performance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Core.Utilities.Interceptors
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>
                (true).ToList();
            var methodAttributes = type.GetMethod(method.Name)
                .GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
            classAttributes.AddRange(methodAttributes);
            //classAttributes.Add(new ExceptionLogAspect(typeof(FileLogger))); /*Bu kısım Log'lama ile alakalı. Loglama class'ımızı oluşturduğumuzda kullanılacak*/
            classAttributes.Add(new PerformanceAspect(2)); // metodlar çalıştığında 2 saniyeyi geçerse beni uyar. 

            return classAttributes.OrderBy(x => x.Priority).ToArray();
        }
    }
}
