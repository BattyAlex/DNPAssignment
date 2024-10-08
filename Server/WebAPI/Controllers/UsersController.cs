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
            return Created($"/Users/{created.Id}", created);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IResult> GetSingleUser([FromRoute] int id)
    {
        try
        {
            User result = await userRepo.GetSingleUserAsync(id);
            UserDTO dto = new()
            {
                Id = result.Id,
                Username = result.Name
            };
            return Results.Ok(dto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Results.NotFound(e);
        }
    }

    [HttpDelete]
    public async Task<IResult> DeleteUser([FromRoute] int id)
    {
        try
        {
            await userRepo.DeleteUserAsync(id);
            return Results.NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPut]
    public async Task<ActionResult<UserDTO>> UpdateUser([FromBody] UpdateUserDTO request)
    {
        try
        {
            User user = userRepo.GetSingleUserAsync(request.Id).Result;
            user.Name = request.Username;
            user.Password = request.Password;
            //User user = new(request.Username, request.Password);
            //user.Id = request.Id;
            await userRepo.UpdateUserAsync(user);
            UserDTO dto = new()
            {
                Id = user.Id,
                Username = user.Name
            };
            return Created($"/Users/{user.Id}", dto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpGet]
    public IResult GetManyUsers([FromQuery] int id, [FromQuery] string? nameContains)
    {
        try
        {
            List<User> users = userRepo.GetManyUsersAsync().ToList();
            if (!string.IsNullOrEmpty(nameContains))
            {
                users = users.Where(u => u.Name.Contains(nameContains)).ToList();
            }
            return Results.Ok(users);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
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