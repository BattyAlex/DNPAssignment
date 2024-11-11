
using DataTransferObjects;
using Entities;

using DataTransferObjects;
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
      User? user = _userRepository
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
        UserDTO dto = new()
        {
            Id = user.Id,
            Username = user.Name
        };
        return Ok(dto);
    }

    [HttpPost("createuser")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO createUser)
    {
        if (string.IsNullOrEmpty(createUser.Username) || string.IsNullOrEmpty(createUser.Password))
        {
            return Unauthorized("Username and password are required.");
        }
        User? user = _userRepository
            .GetManyUsersAsync()
            .SingleOrDefault(u => u.Name.Equals(createUser.Username));
        if (user == null)
        {
            user = await _userRepository.AddUserAsync(new User(createUser.Username, createUser.Password));
            UserDTO dto = new()
            {
                Id = user.Id,
                Username = user.Name
            };
            
            return Ok(dto);
        }
        return Unauthorized("Username already exists.");
    }
}

    