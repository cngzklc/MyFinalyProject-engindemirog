using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    //EntityFramework : Microsoft'un ORM (Object relational mapping) destekli bir ürün. linq destekli çalışır.
    //Veri tabanındaki tabloyu, sanki class'mış gibi ilişkilendirip, bağ kurup veri tabanı işlemi yapma süreci
    public class EfCategoryDal :EfEntityRepositoryBase<Category, NorthwindContext>, ICategoryDal
    {

    }
}
