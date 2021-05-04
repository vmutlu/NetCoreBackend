using Apsiyon.Business.Abstract;
using Apsiyon.Business.Constants;
using Apsiyon.Business.ValidationRules.FluentValidation;
using Apsiyon.Core.Aspects.Autofac.Validation;
using Apsiyon.Core.Utilities.Results;
using Apsiyon.DataAccess.Abstract;
using Apsiyon.Entities.Concrete;
using System.Collections.Generic;

namespace Apsiyon.Business.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {
            _productRepository.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }

        public IResult Delete(Product product)
        {
            _productRepository.Delete(product);
            return new SuccessResult(Messages.ProductDeleted);
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productRepository.Get(p => p.Id == productId));
        }

        public IDataResult<List<Product>> GetList()
        {
            return new SuccessDataResult<List<Product>>(_productRepository.GetList());
        }

        public IDataResult<List<Product>> GetListProductCategory(int categoryId)
        {
            return new SuccessDataResult<List<Product>>(_productRepository.GetList(p => p.CategoryId == categoryId));
        }

        public IResult Update(Product product)
        {
            _productRepository.Update(product);
            return new SuccessResult(Messages.ProductUpdated);
        }
    }
}
