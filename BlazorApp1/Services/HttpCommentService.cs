using System.Text.Json;
using DataTransferObjects;

namespace BlazorApp1.Services;

public class HttpCommentService : ICommentService
{
    private readonly HttpClient _httpClient ;

    public HttpCommentService(HttpClient httpClient)
    {
       
        this._httpClient = httpClient;
    }

    public async Task<CommentDTO> AddCommentAsync(CreateCommentDto request)
    {
        HttpResponseMessage httpResponse =
            await _httpClient.PostAsJsonAsync("comments", request);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(httpResponse.StatusCode.ToString());
        }

        return JsonSerializer.Deserialize<CommentDTO>(response,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
    }

    public async Task UpdateCommentAsync(int id,
        ReplaceCommentDTO request)
    {
        HttpResponseMessage response =
            await _httpClient.PutAsJsonAsync($"comments/{id}", request);
        string responseString = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseString);
        }
    }

    public async Task DeleteCommentAsync(int id)
    {
        HttpResponseMessage response =
            await _httpClient.DeleteAsync($"comments/{id}");
        string responseString = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseString);
        }
    }

    public async Task<CommentDTO> GetCommentByIdAsync(int id)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"comments/{id}");
        string responseString = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseString);
        }

        return JsonSerializer.Deserialize<CommentDTO>(responseString,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
    }

    public async Task<List<CommentDTO>> GetAllCommentsAsync()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("comments");
        string responseString = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseString);
        }

        return JsonSerializer.Deserialize<List<CommentDTO>>(responseString,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
    }
}