using Apsiyon.Business.Abstract;
using Apsiyon.Business.Constants;
using Apsiyon.DataAccess.Abstract;
using Apsiyon.Entities.Concrete;
using Apsiyon.Utilities.Results;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apsiyon.Business.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<IResult> Add(Category category)
        {
            await _categoryRepository.AddAsync(category);
            return new SuccessResult(Messages.CategoryAdded);
        }

        public async Task<IResult> Delete(Category category)
        {
            await _categoryRepository.DeleteAsync(category);
            return new SuccessResult(Messages.CategoryDeleted);
        }

        public async Task<IDataResult<Category>> GetById(int categoryId)
        {
            var category = await _categoryRepository.GetAsync(p => p.Id == categoryId, t => t.CategoryWithProducts);
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

        public async Task<IDataResult<List<Category>>> GetList()
        {
            var response = (from c in await _categoryRepository.GetAllAsync(null,null, c => c.CategoryWithProducts)
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

        public async Task<IResult> Update(Category category)
        {
            await _categoryRepository.UpdateAsync(category);
            return new SuccessResult(Messages.CategoryUpdated);
        }
    }
}
