using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Enum;
using webapi.model;
using webapi.Repositories;

namespace webapi.Controllers;

[ApiController]
[Route("scada/login")]
public class LoginController : ControllerBase
{

    private readonly IUserRepository _userRepository;

    public LoginController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpPost()]
    public IActionResult Login(LoginModel model)
    {

        User user = _userRepository.GetUserByCredentials(model.Username, model.Password);

        if (user == null)
            return Unauthorized();

        var userType = user.Type == UserType.ADMIN ? "admin" : "user";
        var response = new { UserType = userType };
        return Ok(response);
    }
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}


