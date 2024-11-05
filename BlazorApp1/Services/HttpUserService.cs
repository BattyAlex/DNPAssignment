using System.Text;
using System.Text.Json;
using DataTransferObjects;

namespace BlazorApp1.Services;

public class HttpUserService:IUserService
{
    private readonly HttpClient _httpClient;

    public HttpUserService(HttpClient httpClient)
    {
        this._httpClient = httpClient;
    }

    public async Task<UserDTO> AddUserAsync(CreateUserDTO request)
    {
       HttpResponseMessage response = await _httpClient.PostAsJsonAsync("users", request);
       string responseString = await response.Content.ReadAsStringAsync();

       if (!response.IsSuccessStatusCode)
       {
           throw new Exception(responseString);
       }
      return JsonSerializer.Deserialize<UserDTO>(responseString, new JsonSerializerOptions
       {
           PropertyNameCaseInsensitive = true
       })!;
    }
    public async Task UpdateUserAsync(int id, UpdateUserDTO request)
    {
        HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"users/{id}", request);
        string responseString = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseString);
        }
    }
    public async Task DeleteUserAsync(int id)
    {
        HttpResponseMessage response = await _httpClient.DeleteAsync($"users/{id}");
        string responseString = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseString);
        }
    }
    public async Task<UserDTO> GetUserAsync(int id)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"users/{id}");
        string responseString = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseString);
        }

        return JsonSerializer.Deserialize<UserDTO>(responseString, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }
    public async Task<List<UserDTO>> GetUsersAsync()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("users");
        string responseString = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseString);
        }

        return JsonSerializer.Deserialize<List<UserDTO>>(responseString, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }
}