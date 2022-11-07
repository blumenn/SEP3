using System.Net.Http.Json;
using System.Text.Json;
using HTTPClients.ClientInterfaces;
using Shared.DTOs;
using Shared.Models;

namespace HTTPClients.Implementations;

public class PostHTTPClient : IPostService
{
    
    private readonly HttpClient client;

    public PostHTTPClient(HttpClient client)
    {
        this.client = client;
    }
    
    public async Task CreateAsync(PostCreationDTO dto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/posts",dto);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }
    
    public async Task<IEnumerable<Post>> GetAsync(int? authorId, string? authorName, string? titleContains, string? bodyContains)
    {
        HttpResponseMessage response = await client.GetAsync("/posts");
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
            throw new Exception(result);

        ICollection<Post> posts = JsonSerializer.Deserialize<ICollection<Post>>(result,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        return posts;
    }

    public async Task<Post> GetByIdAsync(int id)
    {
        HttpResponseMessage response = await client.GetAsync($"posts/{id}");
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
            throw new Exception(result);

        Post post = JsonSerializer.Deserialize<Post>(result, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        return post;
    }
}