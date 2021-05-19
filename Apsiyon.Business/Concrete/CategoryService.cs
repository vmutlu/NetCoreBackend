using Apsiyon.Business.Abstract;
using Apsiyon.Business.Constants;
using Apsiyon.Core.Utilities.Results;
using Apsiyon.DataAccess.Abstract;
using Apsiyon.Entities.Concrete;
using System.Collections.Generic;
using System.Linq;

namespace Apsiyon.Business.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public IResult Add(Category category)
        {
            _categoryRepository.Add(category);
            return new SuccessResult(Messages.CategoryAdded);
        }

        public IResult Delete(Category category)
        {
            _categoryRepository.Delete(category);
            return new SuccessResult(Messages.CategoryDeleted);
        }

        public IDataResult<Category> GetById(int categoryId)
        {
            var category = _categoryRepository.Get(p => p.Id == categoryId, t => t.CategoryWithProducts);
            if (category is null)
                return new ErrorDataResult<Category>(categoryId + Messages.NotFoundCategory);

            Category response = new()
            {
                Id = category.Id,
                Name = category.Name,
                CategoryWithProducts = category.CategoryWithProducts != null ? from pc in category.CategoryWithProducts
                                                                               select new CategoryWithProduct
                                                                               {
                                                                                   ProductId = pc.ProductId,
                                                                                   Id = pc.Id,
                                                                                   Product = new Product()
                                                                                   {
                                                                                       Id = pc.ProductId
                                                                                   }
                                                                               } : null
            };
            return new SuccessDataResult<Category>(response);
        }

        public IDataResult<List<Category>> GetList()
        {
            var response = (from c in _categoryRepository.GetList(null, c => c.CategoryWithProducts)
                            select new Category()
                            {
                                Id = c.Id,
                                Name = c.Name,
                                CategoryWithProducts = c.CategoryWithProducts != null ? from cp in c.CategoryWithProducts
                                                                                        select new CategoryWithProduct
                                                                                        {
                                                                                            ProductId = cp.ProductId,
                                                                                            Id = cp.Id,
                                                                                            Product = new Product()
                                                                                            {
                                                                                                Id = cp.ProductId
                                                                                            }
                                                                                        } : null
                            });
            return new SuccessDataResult<List<Category>>(response.ToList());
        }

        public IResult Update(Category category)
        {
            _categoryRepository.Update(category);
            return new SuccessResult(Messages.CategoryUpdated);
        }
    }
}
