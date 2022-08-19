using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using Task_Manager_C_Sharp.Controllers.Dtos;
using Task_Manager_C_Sharp.Models;
using Task_Manager_C_Sharp.Repository;

namespace Task_Manager_C_Sharp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : BaseController
    {

        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, IUserRepository userRepository) : base(userRepository)
        {
            _logger = logger;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult createUser([FromBody] User user)
        {
            try
            {

                user.Email = user.Email.ToLower();
                var errorMessages = new List<string>();

                Regex validateUserNameRegex = new Regex(@"^(?=[a-zA-Z._]{8,20}$)(?!.*[_.]{2})[^_.].*[^_.]$");

                if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrWhiteSpace(user.UserName) || !validateUserNameRegex.IsMatch(user.UserName))
                {
                    errorMessages.Add("Nome invalido!");
                }


                Regex hasNumberRegex = new Regex(@"[0-9]+");
                Regex hasUpperCharRegex = new Regex(@"[A-Z]+");
                Regex hasMinimum8CharsRegex = new Regex(@".{8,}");

                if (string.IsNullOrEmpty(user.Password) || string.IsNullOrWhiteSpace(user.Password) || !hasNumberRegex.IsMatch(user.Password) || !hasUpperCharRegex.IsMatch(user.Password) || !hasMinimum8CharsRegex.IsMatch(user.Password))
                {
                    errorMessages.Add("Senha invalida!");
                }


                Regex validateEmailRegex = new Regex("^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$");
                if (string.IsNullOrEmpty(user.Email) || string.IsNullOrWhiteSpace(user.Email) || !validateEmailRegex.IsMatch(user.Email))
                { 
                    errorMessages.Add("Email invalido!");
                }

                if (_userRepository.UserEmailExists(user.Email))
                {
                    errorMessages.Add("Email já cadastrado!");
                }


                if(errorMessages.Count > 0)
                {
                    return BadRequest(new ErrorResponseDto()
                    {
                        Status = StatusCodes.Status400BadRequest,
                        ErrorMessages = errorMessages
                    });
                }

                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                _userRepository.Save(user);

                return Ok(new{ msg= "Usuario criado com sucesso!" });
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu um erro ao obter o usuario", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDto()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    ErrorMessage = "Ocorreu um erro ao criar o usuario, tente novamente!"
                });
            }
        }
    }
}
