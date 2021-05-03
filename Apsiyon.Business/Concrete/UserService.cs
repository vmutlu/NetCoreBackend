using Apsiyon.Business.Abstract;
using Apsiyon.DataAccess.Abstract;
using Apsiyon.Core.Entities.Concrete;
using System.Collections.Generic;

namespace Apsiyon.Business.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public void Add(User user)
        {
            _userRepository.Add(user);
        }

        public User GetByEmail(string email)
        {
            return _userRepository.Get(u => u.Email == email);
        }

        public List<OperationClaim> GetClaims(User user)
        {
            return _userRepository.GetClaims(user);
        }
    }
}
