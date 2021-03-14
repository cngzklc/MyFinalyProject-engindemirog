using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Caching
{
    //DependencyResolver/CoreModule service eklemesi yapıldı.
    public interface ICacheManager
    {
        //Cache'in amacı: Başka bir kullanıcının çağırdığı veriyi bir değişiklik yok ise o veriyi database'e gitmeden çekmek.
        T Get<T>(string key);
        object Get(string key);// bu şekilde olunca tip dönüşümü yapmak gerekiyor.
        void Add(string key, object value, int duration);
        bool IsAdd(string key);
        void Remove(string key);
        void RemoveByPattern(string pattern);
    }
}
