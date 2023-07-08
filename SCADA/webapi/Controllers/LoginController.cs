using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Enum;
using webapi.model;

namespace webapi.Controllers;

[ApiController]
[Route("scada/login")]
public class LoginController : ControllerBase
{
    
    private readonly ILogger<LoginController> _logger;

    public LoginController(ILogger<LoginController> logger)
    {
        _logger = logger;
    }

    [HttpPost()]
    public IActionResult Login(LoginModel model)
    {
        Console.WriteLine($"Username: {model.Username}");
        Console.WriteLine($"Password: {model.Password}");
        DbSet<User> users = new ScadaDBContext().Users;
        User user = users.FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);

        if (user == null)
        {
            return Unauthorized();
        }

        var userType = user.Type == UserType.ADMIN ? "admin" : "user";

        var response = new { UserType = userType };
        return Ok(response);
    }
}

public class LoginModel
{
    public string Username { get; set; }
    public string Password { get; set; }
}
