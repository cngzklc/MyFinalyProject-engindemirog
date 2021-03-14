using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using Core.Utilities.Messages;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType)  //Bana(attribute) validator type ver. Örneğin ProductValidator.
                                                    //Örneğin add metodunu ProductValidator'daki kurallara göre doğrula.
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType)) // Gönderilen validatorType bir IValidator değilse exception fırlat diyor.
            {
                throw new System.Exception("Bu bir doğrulama sınıfı değil!");
            }

            _validatorType = validatorType; //validatorType doğru ise _validatorType'a eşitle.
        }
        protected override void OnBefore(IInvocation invocation) //MetodInterception'dan inheritance ettiğimiz için, MetodInterception classındaki
                                                                // OnBefore methodunu override ediyoruz. OnBefore=metodun önündeki kuralları çalıştır demek.
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType); //Reflection. ProductValidation'un bir instance'ını oluşturuyor.
            var entityType = _validatorType.BaseType.GetGenericArguments()[0]; // ProductValidator'un Base Type'ını (AbstractValidator) bul ve onun Generic argümanlarından(Product) ilkini bul.
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType); // Metodun parametrelerine (Add(Product product)) bak, entityType (Product)Validator'un tipine eşit olanları bul diyor.
            foreach (var entity in entities)        // Bulundan entities'lerin hepsini gez ve ValidationTool'u kullanarak, validate et diyoruz.
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
