using Apsiyon.Aspects.Autofac.UsersAspect;
using Apsiyon.Business.Abstract;
using Apsiyon.Business.Constants;
using Apsiyon.DataAccess.Abstract;
using Apsiyon.Entities;
using Apsiyon.Entities.Concrete;
using Apsiyon.Utilities.Results;
using System.Linq;
using System.Threading.Tasks;

namespace Apsiyon.Business.Concrete
{
    public class UserOperationClaimService : IUserOperationClaimService
    {
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;

        public UserOperationClaimService(IUserOperationClaimRepository userOperationClaimRepository) => (_userOperationClaimRepository) = (userOperationClaimRepository);

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
        public async Task<PagingResult<UserOperationClaim>> GetAllAsync(GeneralFilter generalFilter = null)
        {
            if (generalFilter.Page <= 0 || generalFilter.PropertyName == null)
                return new PagingResult<UserOperationClaim>(null, 0, false, Messages.EmptyObject);

            var query = await _userOperationClaimRepository.GetAllForPagingAsync(generalFilter.Page, generalFilter.PropertyName, generalFilter.Asc, null, c => c.User, o => o.OperationClaim).ConfigureAwait(false);
            var response = (from uoc in query.Data
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

            return new PagingResult<UserOperationClaim>(response, query.TotalItemCount, query.Success, query.Message);
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
