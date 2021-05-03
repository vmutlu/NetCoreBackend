using Apsiyon.Business.Abstract;
using Apsiyon.Business.Constants;
using Apsiyon.Core.Utilities.Results;
using Apsiyon.DataAccess.Abstract;
using Apsiyon.Entities.Concrete;
using System.Collections.Generic;

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
            return new SuccessDataResult<Category>(_categoryRepository.Get(c => c.Id == categoryId));
        }

        public IDataResult<List<Category>> GetList()
        {
            return new SuccessDataResult<List<Category>>(_categoryRepository.GetList());
        }

        public IResult Update(Category category)
        {
            _categoryRepository.Update(category);
            return new SuccessResult(Messages.CategoryUpdated);
        }
    }
}
