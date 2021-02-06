using DataAccess.Abstract;
using DataAccess.DTOs;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductDal : IProductDal
    {
        List<Product> _products;
        public InMemoryProductDal()
        {
            _products = new List<Product>
            {
            new Product { ProductId =1, CategoryId =1,ProductName="Kamera",UnitPrice=500,UnitsInStock =3 },
            new Product { ProductId =2, CategoryId =1,ProductName="Telefon",UnitPrice=1500,UnitsInStock =2 },
            new Product { ProductId =3, CategoryId =1,ProductName="Klavye",UnitPrice=150,UnitsInStock =65 },
            new Product { ProductId =4, CategoryId =1,ProductName="Fare",UnitPrice=85,UnitsInStock =1 },
            new Product { ProductId =5, CategoryId =2,ProductName="Bardak",UnitPrice=15,UnitsInStock =15 },
            new Product { ProductId =6, CategoryId =2,ProductName="Tabak",UnitPrice=3,UnitsInStock =98 },
            new Product { ProductId =7, CategoryId =2,ProductName="Kaşık",UnitPrice=3,UnitsInStock =98 },
            new Product { ProductId =8, CategoryId =3,ProductName="Kalem",UnitPrice=5,UnitsInStock =35 },
            new Product { ProductId =9, CategoryId =3,ProductName="Defter",UnitPrice=7,UnitsInStock =67 },
            new Product { ProductId =8, CategoryId =3,ProductName="Kitap",UnitPrice=13,UnitsInStock =43 },
            };
        }
        public void Add(Product product)
        {
            _products.Add(product);
        }

        //Linq = Language Integrated Query
        //Lambda
        public void Delete(Product product)
        {
            //Linq olmadan Listeden product silmek için aşağıdaki kodlar kullanılır.
            //Product productToDelete;
            //foreach (var item in _products)
            //{
            //    if (product.ProductId == item.ProductId)
            //    {
            //        productToDelete = item;
            //        _products.Remove(productToDelete);
            //    }
            //}

            //product ile gelen ürün ID'sini linq ile buldurarak, productToDelete değişkenine atıyoruz.
            //ID üzerinde sordu yaptığımız için bir adet product dönüş sağlayacaktır.
            Product productToDelete = _products.SingleOrDefault(p => p.ProductId == product.ProductId);
            _products.Remove(productToDelete);
        }
        public void Update(Product product)
        {
            Product productToUpdate = _products.SingleOrDefault(p => p.ProductId == product.ProductId);
            productToUpdate.ProductName = product.ProductName;
            productToUpdate.CategoryId = product.CategoryId;
            productToUpdate.UnitPrice = product.UnitPrice;
            productToUpdate.UnitsInStock = product.UnitsInStock;
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            var query = filter.Compile();
            return (Product)_products.SingleOrDefault(query.Invoke);
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            //return filter == null ?
            //                    _products.ToList() :
            //                    _products.Where(filter).ToList();
            if (filter == null)
            {
                return _products;
            }
            else
            {
                var query = filter.Compile();
                return _products.Where(query.Invoke).ToList();
            }
        }

        public List<ProductDetailDto> GetProductDetails()
        {
            throw new NotImplementedException();
        }
    }
}
