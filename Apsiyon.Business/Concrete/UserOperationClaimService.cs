using Apsiyon.Aspects.Autofac.UsersAspect;
using Apsiyon.Business.Abstract;
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
    public class UserOperationClaimService : IUserOperationClaimService
    {
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;
        private readonly IPaginationUriService _uriService;

        public UserOperationClaimService(IUserOperationClaimRepository userOperationClaimRepository, IPaginationUriService uriService) { _userOperationClaimRepository = userOperationClaimRepository;  _uriService = uriService; }

        public async Task<IResult> AddAsync(UserOperationClaim operation)
        {
            await _userOperationClaimRepository.AddAsync(operation).ConfigureAwait(false);
            return new Result(success: true, message: "Ekleme işlemi başarılı");
        }

        [SecuredOperation("admin")]
        public async Task<IResult> DeleteAsync(int id)
        {
            var deleting = await _userOperationClaimRepository.GetAsync(uo => uo.Id == id).ConfigureAwait(false);
            if (deleting is null)
                return new ErrorResult($"{id} id'sine sahip user claim bulunamadı");

            await _userOperationClaimRepository.DeleteAsync(deleting).ConfigureAwait(false);
            return new Result(success: true, message: "User claim silme işlemi başarılı");
        }

        [SecuredOperation("admin")]
        public async Task<IDataResult<PaginationDataResult<UserOperationClaim>>> GetAllAsync(PaginationQuery paginationQuery = null)
        {
            var response = (from uoc in await _userOperationClaimRepository.GetAllAsync(null, paginationQuery, o => o.User, o => o.OperationClaim).ConfigureAwait(false)
                            select new UserOperationClaim()
                            {
                                Id = uoc.Id,
                                UserId = uoc.UserId,
                                OperationClaimId = uoc.OperationClaimId,
                                User = uoc.User != null ? new User()
                                {
                                    FirstName = uoc.User.FirstName,
                                    LastName = uoc.User.LastName,
                                    Email = uoc.User.Email,
                                    Status = uoc.User.Status
                                } : null,
                                OperationClaim = uoc.OperationClaim != null ? new OperationClaim()
                                {
                                    Name = uoc.OperationClaim.Name
                                } : null
                            }).ToList();

            var responsePagination = response.AsQueryable().CreatePaginationResult(HttpStatusCode.OK, paginationQuery, response.Count, _uriService);
            return new SuccessDataResult<PaginationDataResult<UserOperationClaim>>(responsePagination);
        }

        [SecuredOperation("admin")]
        public async Task<IDataResult<UserOperationClaim>> GetByIdAsync(int id)
        {
            var operationClaims = await _userOperationClaimRepository.GetAsync(c => c.Id == id, t => t.User, t => t.OperationClaim).ConfigureAwait(false);

            if (operationClaims is null)
                return new ErrorDataResult<UserOperationClaim>($"{id} id'sine sahip user claim bulunamadı");

            var operation = new UserOperationClaim()
            {
                Id = operationClaims.Id,
                UserId = operationClaims.UserId,
                OperationClaimId = operationClaims.OperationClaimId,
                User = operationClaims.User != null ? new User()
                {
                    LastName = operationClaims.User.LastName,
                    FirstName = operationClaims.User.FirstName,
                    Email = operationClaims.User.Email,
                    Status = operationClaims.User.Status
                } : null,
                OperationClaim = operationClaims.OperationClaim != null ? new OperationClaim()
                {
                    Name = operationClaims.OperationClaim.Name
                } : null
            };

            return new SuccessDataResult<UserOperationClaim>(operation);
        }

        [SecuredOperation("admin")]
        public async Task<IResult> UpdateAsync(UserOperationClaim operation)
        {
            await _userOperationClaimRepository.UpdateAsync(operation).ConfigureAwait(false);
            return new Result(success: true, "User claim güncelleme işlemi başarılı");
        }
    }
}
