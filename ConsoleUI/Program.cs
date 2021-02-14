using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //ProductDetail();
            //ProductTest();
            //CategoryTest();
            //CategoryGetById();
            //AddProduct();
            ProductGetAll();
        }

        private static void ProductGetAll()
        {
            ProductManager productManager = new ProductManager(new EfProductDal());
            var resault = productManager.GetAll();
            if (resault.Success)
            {
                foreach (var product in resault.Data)
                {
                    Console.WriteLine(product.ProductName);
                }
            }
            else
            {
                Console.WriteLine(resault.Message);
            }
        }

        private static void AddProduct()
        {
            EfProductDal efProductDal = new EfProductDal();
            efProductDal.Add(new Product() { CategoryId = 1, ProductName = "Ayakkabı", UnitPrice = 150, UnitsInStock = 5 });
        }

        private static void CategoryTest()
        {
            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
            foreach (var category in categoryManager.GetAll())
            {
                Console.WriteLine(category.CategoryName);
            }
        }
        private static void CategoryGetById()
        {
            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
            Console.WriteLine("ID = {0}", categoryManager.GetById(152).CategoryName);
        }
        private static void ProductTest()
        {
            ProductManager productManager = new ProductManager(new EfProductDal());
            var result = productManager.GetProductDetails();
            if (result.Success == true)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine(item.ProductName);
                    Console.WriteLine(result.Message);
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }
        }
        private static void ProductDetail()
        {
            ProductManager productManager = new ProductManager(new EfProductDal());
            var result = productManager.GetProductDetails();
            if (result.Success == true)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine("{0}, {1}",item.ProductName, item.CategoryName);
                    
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }

        }
    }
}
