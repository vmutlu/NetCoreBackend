using Apsiyon.Business.Abstract;
using Apsiyon.DataAccess.Abstract;
using Apsiyon.Entities.Concrete;
using System.Collections.Generic;
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
    }
}
