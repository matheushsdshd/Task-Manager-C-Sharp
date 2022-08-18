namespace Task_Manager_C_Sharp.Controllers.Dtos
{
    public class ErrorResponseDto
    {
        public int Status { get; set; }
        public string ErrorMessage { get; set; }
        public List<string> ErrorMessages { get; set; }
    }
}
