﻿using Apsiyon.Aspects.Autofac.Caching;
using Apsiyon.Aspects.Autofac.Logging;
using Apsiyon.Aspects.Autofac.Transaction;
using Apsiyon.Aspects.Autofac.UsersAspect;
using Apsiyon.Aspects.Autofac.Validation;
using Apsiyon.Business.Abstract;
using Apsiyon.Business.Constants;
using Apsiyon.Business.ValidationRules.FluentValidation;
using Apsiyon.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Apsiyon.DataAccess.Abstract;
using Apsiyon.Entities.Concrete;
using Apsiyon.Extensions;
using Apsiyon.Services.Abstract;
using Apsiyon.Utilities.Business;
using Apsiyon.Utilities.Results;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Apsiyon.Business.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IPaginationUriService _uriService;
        public ProductService(IProductRepository productRepository, IPaginationUriService uriService)
        {
            _productRepository = productRepository;
            _uriService = uriService;
        }

        private IResult CheckIfProductNameExists(string productName)
        {
            if (_productRepository.GetAsync(p => p.ProductName == productName).Result != null)
                return new ErrorResult(Messages.ErrorProductAdded);

            return new SuccessResult("Ürün eklensin");
        }

        [SecuredOperation("admin,user")]
        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        [TransactionScopeAspect]
        public async Task<IResult> AddAsync(Product product)
        {
            IResult result = BusinessRules.Run(CheckIfProductNameExists(product.ProductName));

            if (result == null)
                return result;

            await  _productRepository.AddAsync(product);
            return new SuccessResult(Messages.ProductAdded);
        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("IProductService.GetAllAsync")]
        [TransactionScopeAspect]
        public async Task<IResult> DeleteAsync(int id)
        {
            var result = await _productRepository.GetAsync(i => i.Id == id);

            if (result is null)
                return new ErrorResult($"{id} id'sine sahip ürün bulunamadı");

            await _productRepository.DeleteAsync(result);
            return new SuccessResult(Messages.ProductDeleted);
        }

        [LogAspect(typeof(DatabaseLogger))]
        public async Task<IDataResult<Product>> GetByIdAsync(int productId)
        {
            var product = await _productRepository.GetAsync(p => p.Id == productId, t => t.CategoryWithProducts);
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


        [SecuredOperation("admin")]
        [CacheAspect]
        public async Task<IDataResult<PaginationDataResult<Product>>> GetAllAsync(PaginationQuery paginationQuery = null)
        {
            var response = (from p in await _productRepository.GetAllAsync(null, null, p => p.CategoryWithProducts).ConfigureAwait(false)
                            select new Product()
                            {
                                Id = p.Id,
                                ProductName = p.ProductName,
                                QuantityPerUnit = p.QuantityPerUnit,
                                UnitPrice = p.UnitPrice,
                                UnitsInStock = p.UnitsInStock,
                                CategoryWithProducts = p.CategoryWithProducts != null ? (from pc in p.CategoryWithProducts
                                                                                        select new CategoryWithProduct
                                                                                        {
                                                                                            CategoryId = pc.CategoryId,
                                                                                            Id = pc.Id,
                                                                                            Category = new Category()
                                                                                            {
                                                                                                Id = pc.CategoryId
                                                                                            }
                                                                                        }).ToList() : null
                            }).ToList();

            var responsePagination = response.AsQueryable().CreatePaginationResult(HttpStatusCode.OK, paginationQuery, response.Count, _uriService);
            return new SuccessDataResult<PaginationDataResult<Product>>(responsePagination, (HttpStatusCode)responsePagination.StatusCode);
        }

        [SecuredOperation("admin")]
        public async Task<IDataResult<List<Product>>> GetListProductCategory(int categoryId)
        {
            var response = await _productRepository.GetAllAsync(p => p.Id == categoryId, null);
            return new SuccessDataResult<List<Product>>(response);
        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("IProductService.GetAllAsync")]
        [TransactionScopeAspect]
        public async Task<IResult> UpdateAsync(Product product)
        {
            await _productRepository.UpdateAsync(product);
            return new SuccessResult(Messages.ProductUpdated);
        }
    }
}
