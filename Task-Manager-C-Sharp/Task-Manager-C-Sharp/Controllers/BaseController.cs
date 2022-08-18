using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Task_Manager_C_Sharp.Controllers
{
    [Authorize]
    public class BaseController : ControllerBase
    {
    }
}