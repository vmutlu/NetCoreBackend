using Apsiyon.Aspects.Autofac.Caching;
using Apsiyon.Aspects.Autofac.Transaction;
using Apsiyon.Aspects.Autofac.UsersAspect;
using Apsiyon.Business.Abstract;
using Apsiyon.Business.Constants;
using Apsiyon.DataAccess.Abstract;
using Apsiyon.Entities.Concrete;
using Apsiyon.Extensions;
using Apsiyon.Services.Abstract;
using Apsiyon.Utilities.Results;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Apsiyon.Business.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IPaginationUriService _uriService;
        public CategoryService(ICategoryRepository categoryRepository, IPaginationUriService uriService)
        {
            _categoryRepository = categoryRepository;
            _uriService = uriService;
        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("ICategoryService.GetAllAsync")]
        [TransactionScopeAspect]
        public async Task<IResult> AddAsync(Category category)
        {
            await _categoryRepository.AddAsync(category);
            return new SuccessResult(Messages.CategoryAdded);
        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("ICategoryService.GetAllAsync")]
        [TransactionScopeAspect]
        public async Task<IResult> DeleteAsync(int id)
        {
            var result = await _categoryRepository.GetAsync(i => i.Id == id);

            if (result is null)
                return new ErrorResult($"{id} id'sine sahip kategori bulunamadı");

            await _categoryRepository.DeleteAsync(result);
            return new SuccessResult(Messages.CategoryDeleted);
        }

        [SecuredOperation("admin,user")]
        [CacheAspect]
        public async Task<IDataResult<Category>> GetByIdAsync(int categoryId)
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

        [SecuredOperation("admin,user")]
        [CacheAspect]
        public async Task<IDataResult<PaginationDataResult<Category>>> GetAllAsync(PaginationQuery paginationQuery = null)
        {
            var response = (from c in await _categoryRepository.GetAllAsync(null, null, c => c.CategoryWithProducts).ConfigureAwait(false)
                            select new Category()
                            {
                                Id = c.Id,
                                Name = c.Name,
                                CategoryWithProducts = c.CategoryWithProducts != null ? (from cp in c.CategoryWithProducts
                                                                                         select new CategoryWithProduct
                                                                                         {
                                                                                             ProductId = cp.ProductId,
                                                                                             Id = cp.Id,
                                                                                             Product = new Product()
                                                                                             {
                                                                                                 Id = cp.ProductId
                                                                                             }
                                                                                         }).ToList() : null
                            }).ToList();

            var responsePagination = response.AsQueryable().CreatePaginationResult(HttpStatusCode.OK, paginationQuery, response.Count, _uriService);
            return new SuccessDataResult<PaginationDataResult<Category>>(responsePagination, (HttpStatusCode)responsePagination.StatusCode);
        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("ICategoryService.GetAllAsync")]
        [TransactionScopeAspect]
        public async Task<IResult> UpdateAsync(Category category)
        {
            await _categoryRepository.UpdateAsync(category);
            return new SuccessResult(Messages.CategoryUpdated);
        }
    }
}
