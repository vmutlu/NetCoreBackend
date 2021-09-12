using Apsiyon.Business.Abstract;
using Apsiyon.DataAccess.Abstract;
using Apsiyon.Entities.Concrete;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apsiyon.Business.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository) => (_userRepository) = (userRepository);
        public async Task Add(User user)
        {
            await _userRepository.AddAsync(user);
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _userRepository.GetAsync(u => u.Email == email);
        }

        public async Task<List<OperationClaim>> GetClaims(User user)
        {
            return _userRepository.GetClaims(user);
        }
        public async Task<List<User>> GetUsersAsync()
        {
            var users = (from uop in await _userRepository.GetAllAsync(null, null, u => u.UserOperationClaims).ConfigureAwait(false)
                         from uoc in uop.UserOperationClaims
                         where uoc.UserId == uop.Id
                         select new User()
                         {
                             Id = uop.Id,
                             FirstName = uop.FirstName,
                             LastName = uop.LastName,
                             Email = uop.Email,
                             Status = uop.Status,
                             UserOperationClaims = uop.UserOperationClaims != null ? (from uoc in uop.UserOperationClaims
                                                                                      select new UserOperationClaim()
                                                                                      {
                                                                                          Id = uoc.Id,
                                                                                          UserId = uoc.UserId
                                                                                      }).ToList() : null
                         }).ToList();

            return users;
        }
    }
}
