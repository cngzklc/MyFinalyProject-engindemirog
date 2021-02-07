using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        /*
        Generic Constrain : Generic kısıt
        "where T : class"                   => ile referans tipli nesneler üzerinde çalışabilir demek
        "where T : class, IEntity"          => IEntity olabilir ya da IEntity" implemente eden nesne olabilir demek.
        "where T : class, IEntity, new()"   => ile soyut bir nesne olmamalı ve IEntity'den implemente edilmiş referans tipli bir nesne olabilir demek.
         
        Expression :Lamda türünde filtreler koyarak liste elde etmeye yarayan metod.
        En alttaki tür belirtilelerek elde edilen listenin daha gelişmişidir. 
        Expression sayesinde "p=>p.CategoryId==2" gibi fonksiyonlar yazarak listeler elde edeceğiz.*/
        List<T> GetAll(Expression<Func<T, bool>> filter = null);
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        //List<T> GetAllByCategory(int categoryId);
    }
}
