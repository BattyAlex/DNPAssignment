using DataTransferObjects;
using Entities;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public AuthController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequest)
    {
        if (string.IsNullOrEmpty(loginRequest.username) || string.IsNullOrEmpty(loginRequest.password))
        {
            return Unauthorized("Username and password are required.");
        }
      var user = _userRepository
            .GetManyUsersAsync()
            .SingleOrDefault(u => u.Name.Equals(loginRequest.username));
        
        if (user == null)
        {
            return Unauthorized("User does not exist.");
        }
        if (user.Password != loginRequest.password) 
        {
            return Unauthorized("Invalid password.");
        }
        return Ok("Login successful!");
    }
}
    