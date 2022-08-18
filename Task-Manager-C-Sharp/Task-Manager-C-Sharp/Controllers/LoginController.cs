using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task_Manager_C_Sharp.Controllers.Dtos;
using Task_Manager_C_Sharp.Models;
using Task_Manager_C_Sharp.Services;

namespace Task_Manager_C_Sharp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : BaseController
    {

        private readonly ILogger<LoginController> _logger;


        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }


        //Login do usuario
        [HttpPost]
        [AllowAnonymous]
        public IActionResult LoginValidation([FromBody] LoginRequestDto request)
        {
            try
            {
                if (request == null ||
                    string.IsNullOrEmpty(request.Login) || string.IsNullOrWhiteSpace(request.Login) ||
                    string.IsNullOrEmpty(request.Password) || string.IsNullOrWhiteSpace(request.Password))
                {
                    return BadRequest(new ErrorResponseDto()
                    {
                        Status = StatusCodes.Status400BadRequest,
                        ErrorMessage = "Parametros de entrada invalidos"
                    });
                }

                var testUser = new User()
                {
                    Id = 1,
                    UserName = "Matheus Henrique Silva",
                    Email = "admin@admin.com",
                    Password = "Admin1234@"
                };

                var token = TokenService.CreateToken(testUser);


                return Ok(new LoginResponseDto()
                {
                    Email = testUser.Email,
                    UserName = testUser.UserName,
                    Token = token
                });
            }
            catch(Exception ex)
            {
                _logger.LogError($"Ocorreu erro ao efetuar login: {ex.Message}", request);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDto()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    ErrorMessage = "Ocorreu um erro ao efetuar login, tente novamente"
                });
            }
        }
    }
}
