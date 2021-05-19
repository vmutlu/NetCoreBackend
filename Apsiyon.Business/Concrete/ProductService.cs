using Apsiyon.Business.Abstract;
using Apsiyon.Business.Constants;
using Apsiyon.Business.ValidationRules.FluentValidation;
using Apsiyon.Core.Aspects.Autofac.Caching;
using Apsiyon.Core.Aspects.Autofac.Logging;
using Apsiyon.Core.Aspects.Autofac.Performans;
using Apsiyon.Core.Aspects.Autofac.Transaction;
using Apsiyon.Core.Aspects.Autofac.UsersAspect;
using Apsiyon.Core.Aspects.Autofac.Validation;
using Apsiyon.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Apsiyon.Core.Extensions.MapHelper;
using Apsiyon.Core.Utilities.Business;
using Apsiyon.Core.Utilities.Results;
using Apsiyon.DataAccess.Abstract;
using Apsiyon.Entities.Concrete;
using System.Collections.Generic;
using System.Linq;

namespace Apsiyon.Business.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryService _categoryService;
        public ProductService(IProductRepository productRepository, ICategoryService categoryService)
        {
            _productRepository = productRepository;
            _categoryService = categoryService;
        }

        private IResult CheckIfProductNameExists(string productName)
        {
            if (_productRepository.Get(p => p.ProductName == productName) != null)
                return new ErrorResult(Messages.ErrorProductAdded);

            return new SuccessResult("Ürün eklensin");
        }

        //[SecuredOperation("admin,user")]
        //[ValidationAspect(typeof(ProductValidator))]
        [TransactionScopeAspect]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Add(Product product)
        {
            //IResult result = BusinessRules.Run(CheckIfProductNameExists(product.ProductName));
            //if (result == null)
            //    return result;

            _productRepository.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }

        public IResult Delete(Product product)
        {
            _productRepository.Delete(product);
            return new SuccessResult(Messages.ProductDeleted);
        }

        [LogAspect(typeof(DatabaseLogger))]
        public IDataResult<Product> GetById(int productId)
        {
            var product = _productRepository.Get(p => p.Id == productId, t => t.CategoryWithProducts);
            if (product is null)
                return new ErrorDataResult<Product>(productId + Messages.NotFoundProduct);

            Product response = new()
            {
                Id = product.Id,
                ProductName = product.ProductName,
                QuantityPerUnit = product.QuantityPerUnit,
                UnitPrice = product.UnitPrice,
                UnitsInStock = product.UnitsInStock,
                CategoryWithProducts = product.CategoryWithProducts != null ? from pc in product.CategoryWithProducts
                                                                              select new CategoryWithProduct
                                                                              {
                                                                                  CategoryId = pc.CategoryId,
                                                                                  Id = pc.Id,
                                                                                  Category = new Category()
                                                                                  {
                                                                                      Id = pc.CategoryId
                                                                                  }
                                                                              } : null
            };
            return new SuccessDataResult<Product>(response);
        }

        [PerformanceAspect(5)]
        [CacheAspect(1)]
        public IDataResult<List<Product>> GetList()
        {
            var response = (from p in _productRepository.GetList(null, p => p.CategoryWithProducts)
                            select new Product()
                            {
                                Id = p.Id,
                                ProductName = p.ProductName,
                                QuantityPerUnit = p.QuantityPerUnit,
                                UnitPrice = p.UnitPrice,
                                UnitsInStock = p.UnitsInStock,
                                CategoryWithProducts = p.CategoryWithProducts != null ? from pc in p.CategoryWithProducts
                                                                                        select new CategoryWithProduct
                                                                                        {
                                                                                            CategoryId = pc.CategoryId,
                                                                                            Id = pc.Id,
                                                                                            Category = new Category()
                                                                                            {
                                                                                                Id = pc.CategoryId
                                                                                            }
                                                                                        } : null
                            });
            return new SuccessDataResult<List<Product>>(response.ToList());
        }

        public IDataResult<List<Product>> GetListProductCategory(int categoryId)
        {
            return new SuccessDataResult<List<Product>>(_productRepository.GetList(p => p.Id == categoryId));
        }

        public IResult Update(Product product)
        {
            _productRepository.Update(product);
            return new SuccessResult(Messages.ProductUpdated);
        }
    }
}
