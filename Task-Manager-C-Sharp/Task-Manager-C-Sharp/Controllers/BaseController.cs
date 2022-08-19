using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Task_Manager_C_Sharp.Models;
using Task_Manager_C_Sharp.Repository;

namespace Task_Manager_C_Sharp.Controllers
{
    [Authorize]
    public class BaseController : ControllerBase
    {
        protected readonly IUserRepository _userRepository;
        public BaseController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        protected User ReadToken()
        {
            var UserIdStr = User.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(u => u.Value).FirstOrDefault();

            if (!string.IsNullOrEmpty(UserIdStr))
            {
                return _userRepository.GetUserbyId(int.Parse(UserIdStr));
            }

            throw new UnauthorizedAccessException("Token não informado ou invalido");
        }
    }
}