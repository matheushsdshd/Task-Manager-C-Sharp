using Microsoft.AspNetCore.Mvc;
using System;
using Task_Manager_C_Sharp.Controllers.Dtos;
using Task_Manager_C_Sharp.Models;
using Task_Manager_C_Sharp.Repository;

namespace Task_Manager_C_Sharp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssignmentController : BaseController
    {

        private readonly ILogger<AssignmentController> _logger;
        private readonly IAssignmentRepository _assignmentRepository;

        public AssignmentController(ILogger<AssignmentController> logger, IUserRepository userRepository, IAssignmentRepository assignmentRepository) : base(userRepository)
        {
            _logger = logger;
            _assignmentRepository = assignmentRepository;
        }

        [HttpPost]
        public IActionResult CreateAssignment([FromBody] Assignment assignment)
        {
            try {

                var user = ReadToken();
                List<string> errorMessages = new List<string>();
                if (assignment == null || user == null)
                {
                    errorMessages.Add("Favor informar a tarefa ou usuario!");
                }
                else {
                    if (string.IsNullOrEmpty(assignment.Name) || string.IsNullOrWhiteSpace(assignment.Name) || assignment.Name.Count() < 4)
                    {
                        errorMessages.Add("Favor informar um nome válido!");
                    }
                    if (assignment.EstimatedDate == DateTime.MinValue || assignment.EstimatedDate < DateTime.Now.Date)
                    {
                        errorMessages.Add("Data Prevista não pode ser anterior a atual!");
                    }
                }

                if (errorMessages.Count() > 0)
                {
                    return BadRequest(new ErrorResponseDto()
                    {
                        Status = StatusCodes.Status400BadRequest,
                        ErrorMessages = errorMessages
                    });
                }

                assignment.UserId = user.Id;
                assignment.ConclusionDate = null;

                _assignmentRepository.CreateAssignment(assignment);

                return Ok(new { msg = "Tarefa criada com sucesso!" });

            }
            catch (Exception ex)
            {
                _logger.LogError("Ocorreu ao criar uma tarefa", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDto()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    ErrorMessage = "Ocorreu erro ao criar uma tarefa!"
                });
            }
        }
    }
}
