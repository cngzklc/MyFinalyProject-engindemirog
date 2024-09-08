﻿using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.Constants;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.DTOs;
using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        ICategoryService _categoryService;
        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }
        [CacheRemoveAspect("IProductService.Get")] // Ürün eklendiği zaman tüm IPoductService türündeki Get metodu içeren cache'leri temizle diyoruz.
        //[SecuredOperation("product.add,product.list,admin")]
        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {
            /*Refactor edilmiş kod*/
            IResult result = BusinessRules.Run
                (
                CheckIfProductCountOfCategoryCorrect(product.CategoryId),
                CheckIfProductNameExists(product.ProductName),
                CheckIfCategoryLimitExceded()
                );
            if (result != null)
            {
                return result;
            }
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);

            #region Kötü kod
            //ValidationTool.Validate(new ProductValidator(), product);
            //if (CheckIfProductCountOfCategoryCorrect(product.CategoryId).Success)
            //{
            //    if (CheckIfProductNameExists(product.ProductName).Success)
            //    {
            //        _productDal.Add(product);
            //        return new SuccessResult(Messages.ProductAdded);
            //    }
            //    else
            //    {
            //        return new ErrorResult();
            //    }
            //}
            //else
            //{
            //    return new ErrorResult();
            //}
            #endregion

        }

        //[PerformanceAspect(5)] //Bu metodunun çalışması 5 saniyeyi geçerse beni uyar. Ama bu işlem sadece GetAll metodunda çalışır.
        //PerformanceAspect'i Core/Utilities/Interceptors/AspectInterceptorSelector.cs içerisine koyarsak tüm sistemi takip eder.
        [CacheAspect]
        [SecuredOperation("product.list,admin")]
        public IDataResult<List<Product>> GetAll()
        {
            //İş kodları
            //Yetkisi var mı? gibi kuralları geçtikten sonra aşağıdaki listeyi döndürecektir.
            //if (DateTime.Now.Hour == 03)
            //{
            //    return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            //}
            Thread.Sleep(3000);
            return new DataResult<List<Product>>(_productDal.GetAll(), true, Messages.ProductsListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id));
        }

        [CacheAspect]
        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            //iki fiyat aralığındaki ürünleri getirmek için.
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            if (DateTime.Now.Hour == 00)
            {
                return new ErrorDataResult<List<ProductDetailDto>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }

        [CacheRemoveAspect("IProductService.Get")] // Ürün güncellendiği zaman tüm IPoductService türündeki Get metodu içeren cache'leri temizle diyoruz.
        public IResult Update(Product product)
        {
            throw new NotImplementedException();
        }

        IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            var result = _productDal.GetAll(c => c.CategoryId == categoryId).Count;
            if (result > 50)
            {
                return new ErrorResult(Messages.ProductCountofCategoryError);

            }
            else
            {
                return new SuccessResult();
            }
        }
        IResult CheckIfProductNameExists(string productName)
        {
            var result = _productDal.GetAll(c => c.ProductName == productName).Any();
            if (result)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);

            }
            else
            {
                return new SuccessResult();
            }
        }
        IResult CheckIfCategoryLimitExceded()
        {
            var result = _categoryService.GetAll().Data.Count();
            if (result>15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);

            }
            else
            {
                return new SuccessResult();
            }
        }

    }
}
