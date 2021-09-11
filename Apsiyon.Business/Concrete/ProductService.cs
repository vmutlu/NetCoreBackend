using Apsiyon.Aspects.Autofac.Caching;
using Apsiyon.Aspects.Autofac.Logging;
using Apsiyon.Aspects.Autofac.Performans;
using Apsiyon.Aspects.Autofac.Transaction;
using Apsiyon.Business.Abstract;
using Apsiyon.Business.Constants;
using Apsiyon.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Apsiyon.DataAccess.Abstract;
using Apsiyon.Entities.Concrete;
using Apsiyon.Utilities.Results;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        private async Task<IResult> CheckIfProductNameExists(string productName)
        {
            if (await _productRepository.GetAsync(p => p.ProductName == productName) != null)
                return new ErrorResult(Messages.ErrorProductAdded);

            return new SuccessResult("Ürün eklensin");
        }

        //[SecuredOperation("admin,user")]
        //[ValidationAspect(typeof(ProductValidator))]
        [TransactionScopeAspect]
        [CacheRemoveAspect("IProductService.Get")]
        public async Task<IResult> Add(Product product)
        {
            //IResult result = BusinessRules.Run(CheckIfProductNameExists(product.ProductName));
            //if (result == null)
            //    return result;

          await  _productRepository.AddAsync(product);
            return new SuccessResult(Messages.ProductAdded);
        }

        public async Task<IResult> Delete(Product product)
        {
           await _productRepository.DeleteAsync(product);
            return new SuccessResult(Messages.ProductDeleted);
        }

        [LogAspect(typeof(DatabaseLogger))]
        public async Task<IDataResult<Product>> GetById(int productId)
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

        [PerformanceAspect(5)]
        [CacheAspect(1)]
        public async Task<IDataResult<List<Product>>> GetList()
        {
            var response = (from p in await _productRepository.GetAllAsync(null, null, p => p.CategoryWithProducts)
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

        public async Task<IDataResult<List<Product>>> GetListProductCategory(int categoryId)
        {
            var response = await _productRepository.GetAllAsync(p => p.Id == categoryId, null);
            return new SuccessDataResult<List<Product>>(response.ToList());
        }

        public async Task<IResult> Update(Product product)
        {
            await _productRepository.UpdateAsync(product);
            return new SuccessResult(Messages.ProductUpdated);
        }
    }
}
