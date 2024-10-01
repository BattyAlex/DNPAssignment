using DataTransferObjects;
using Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;


[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository userRepo;

    public UsersController(IUserRepository userRepo)
    {
        this.userRepo = userRepo;
    }

    [HttpPost]
    public async Task<ActionResult<UserDTO>> AddUser([FromBody] CreateUserDTO request)
    {
        try
        {
            if (VerifyUserNameAvailable(request.Username))
            {
                return Unauthorized();
            }

            User user = new(request.Username, request.Password);
            User created = await userRepo.AddUserAsync(user);
            UserDTO dto = new()
            {
                Id = created.Id,
                Username = created.Name
            };
            return Created($"/Users/{dto.Id}", created);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    

    private bool VerifyUserNameAvailable(String userName)
    {
        List<User> users = userRepo.GetManyUsersAsync().ToList();
        foreach (User user in users)
        {
            if (user.Name == userName)
            {
                return true;
            }
        }

        return false;
    }
}