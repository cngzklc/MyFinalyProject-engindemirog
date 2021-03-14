using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Core.Aspects.Autofac.Transaction
{
    /*Uygulamalarda tutarlılığı korumak için yapılan yöntem. 
     * Örneğin benim hesabımda 100 lira var, arkdaşıma 10 lira gönderirken , benim hesabımdan 10 lira azalacak, arkadaşımın hesabında 10 lira artacak.
     * Fakat benim hesabımdan para çıkarken benim hesabı güncelledi fakat arkadaşımın hesabında güncelleme yaparken sistem hata verdi. 
     * Bu durumda para iadesi yapılması yani işlemi geri alması gerekiyor.*/
    public class TransactionScopeAspect : MethodInterception
    {
        public override void Intercept(IInvocation invocation)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    invocation.Proceed();
                    transactionScope.Complete();
                }
                catch (System.Exception)
                {
                    transactionScope.Dispose();
                    throw;
                }
            }
        }
    }
}
