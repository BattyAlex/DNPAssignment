using System.Text.Json;
using DataTransferObjects;

namespace BlazorApp1.Services;

public class HttpPostService : IPostService
{
    private readonly HttpClient _httpClient;

    public HttpPostService(HttpClient httpClient)
    {
        this._httpClient = httpClient;
    }

    public async Task<CompletePostDTO> AddPostAsync(CreatePostDTO request)
    {
        HttpResponseMessage response =
            await _httpClient.PostAsJsonAsync("Posts", request);
        string responseString = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseString);
        }

        return JsonSerializer.Deserialize<CompletePostDTO>(responseString,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
    }

    public async Task UpdatePostAsync(int id, UpdatePostDTO request)
    {
        HttpResponseMessage response =
            await _httpClient.PutAsJsonAsync($"posts/{id}", request);
        string responseString = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseString);
        }
    }

    public async Task DeletePostAsync(int id)
    {
        HttpResponseMessage response =
            await _httpClient.DeleteAsync($"posts/{id}");
        string responseString = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseString);
        }
    }

    public async Task<CompletePostDTO> GetPostAsync(int id)
    {
        HttpResponseMessage
            response = await _httpClient.GetAsync($"posts/{id}");
        string responseString = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseString);
        }

        return JsonSerializer.Deserialize<CompletePostDTO>(responseString,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
    }

    public async Task<List<CompletePostDTO>> GetPostsAsync()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("posts");
        string responseString = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseString);
        }

        return JsonSerializer.Deserialize<List<CompletePostDTO>>(responseString,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
    }
}