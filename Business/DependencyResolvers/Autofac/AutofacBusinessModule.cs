﻿using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.Concrete.EntityFramework;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //Burada yapılan işlem; WepAPI/Startup/ConfigureServices(IServiceCollection services) metodu içerisinde oluşturulan service'lerin yerinedir.
            //ConfigureServices veya buradaki(Load) tanımlamalar, ProductManager vb instance'ların her kullanıcı tarafından new'lenmesini engelleyerek performans artışı sağlıyor.
            //Autofac = Interception görevi görüyor. Interception: Metodun başında ya da sonund çalışan kod parçacıkları
            builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance();
            builder.RegisterType<EfProductDal>().As<IProductDal>().SingleInstance();

            builder.RegisterType<CategoryManager>().As<ICategoryService>().SingleInstance();
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>().SingleInstance();

            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly(); // Çalışan uygulama içerisinde 

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()//implemente edilmiş interfacce'leri bul
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector() //onlar için AspectIntercetorSelector class'ını çağır diyor.
                }).SingleInstance();
        }
    }
}
