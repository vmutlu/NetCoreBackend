using Apsiyon.Aspects.Autofac.UsersAspect;
using Apsiyon.Business.Abstract;
using Apsiyon.DataAccess.Abstract;
using Apsiyon.Entities.Concrete;
using Apsiyon.Extensions;
using Apsiyon.Extensions.MapHelper;
using Apsiyon.Services.Abstract;
using Apsiyon.Utilities.Results;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Apsiyon.Business.Concrete
{
    public class OperationClaimService : IOperationClaimService
    {
        private readonly IOperationClaimRepository _operationClaimRepository;
        private readonly IUserService _userService;
        private readonly IPaginationUriService _uriService;

        public OperationClaimService(IOperationClaimRepository operationClaimRepository, IUserService userService, IPaginationUriService uriService)
        {
            _operationClaimRepository = operationClaimRepository;
            _userService = userService;
            _uriService = uriService;
        }

        [SecuredOperation("admin,user")]
        public async Task<IResult> AddAsync(OperationClaim operation)
        {
            await _operationClaimRepository.AddAsync(operation).ConfigureAwait(false);
            return new Result(success: true, message: "Ekleme işlemi başarılı");
        }

        [SecuredOperation("admin")]
        public async Task<IResult> DeleteAsync(int id)
        {
            var deletedOperation = await _operationClaimRepository.GetAsync(i => i.Id == id);
            await _operationClaimRepository.DeleteAsync(deletedOperation).ConfigureAwait(false);
            return new Result(success: true, message: "Silme işlemi başarılı");
        }

        [SecuredOperation("admin,user")]
        public async Task<IDataResult<PaginationDataResult<OperationClaim>>> GetAllAsync(PaginationQuery paginationQuery=null)
        {
            var users = await _userService.GetUsersAsync().ConfigureAwait(false);
            var response = (from oc in await _operationClaimRepository.GetAllAsync(null, paginationQuery, o => o.UserOperationClaims).ConfigureAwait(false)
                            from u in oc.UserOperationClaims
                            from us in users
                            where u.UserId == us.Id
                            select new OperationClaim()
                            {
                                Id = oc.Id,
                                Name = oc.Name,
                                UserOperationClaims = oc.UserOperationClaims != null ? (from uoc in oc.UserOperationClaims
                                                                                        select new UserOperationClaim()
                                                                                        {
                                                                                            User = new User()
                                                                                            {
                                                                                                Id = us.Id,
                                                                                                FirstName = us.FirstName,
                                                                                                LastName = us.LastName,
                                                                                                Email = us.Email,
                                                                                                Status = us.Status
                                                                                            }
                                                                                        }).ToList() : null
                            }).ToList();


            var responsePagination = response.AsQueryable().CreatePaginationResult(HttpStatusCode.OK, paginationQuery, response.Count, _uriService);

            return new SuccessDataResult<PaginationDataResult<OperationClaim>>(responsePagination);
        }

        [SecuredOperation("admin,user")]
        public async Task<IDataResult<OperationClaim>> GetByIdAsync(int id)
        {
            var operationClaims = await _operationClaimRepository.GetAsync(c => c.Id == id, t => t.UserOperationClaims).ConfigureAwait(false);

            if (operationClaims is null)
                return new ErrorDataResult<OperationClaim>(null, $"{id} id'sine sahip Claim bulunamadı.");

            var operation = new OperationClaim()
            {
                Id = operationClaims.Id,
                Name = operationClaims.Name,
                UserOperationClaims = operationClaims.UserOperationClaims != null ? new List<UserOperationClaim>()
                                {
                                    new UserOperationClaim()
                                    {
                                        Id = operationClaims.UserOperationClaims.GetListMapped(op => op.Id),
                                        OperationClaimId = operationClaims.UserOperationClaims.GetListMapped(op => op.OperationClaimId),
                                        UserId = operationClaims.UserOperationClaims.GetListMapped(op => op.UserId),
                                        User = operationClaims.UserOperationClaims.GetListMapped(op => op.User)
                                    }
                                } : null
            };

            return new SuccessDataResult<OperationClaim>(operation);
        }

        [SecuredOperation("admin")]
        public async Task<IResult> UpdateAsync(OperationClaim operation)
        {
            await _operationClaimRepository.UpdateAsync(operation).ConfigureAwait(false);
            return new Result(success: true, message: "Güncelleme işlemi başarılı");
        }
    }
}
