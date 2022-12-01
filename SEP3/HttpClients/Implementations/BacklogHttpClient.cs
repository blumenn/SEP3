using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Domain.DTOs;
using Domain.Models;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class BacklogHttpClient : IBackLogService
{
    private readonly HttpClient client;

    public BacklogHttpClient(HttpClient client)
    {
        this.client = client;
    }

    public async Task CreateAsync(BacklogCreationDto dto)
    {
        HttpResponseMessage responseMessage = await client.PostAsJsonAsync("/backlog", dto);
        if (!responseMessage.IsSuccessStatusCode)
        {
            string content = await responseMessage.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task<ICollection<Backlog>> GetAsync(User? productOwner, bool? isCompleted, string? titleContains)
    {
        string query = ConstructQuery(productOwner, isCompleted, titleContains);

        HttpResponseMessage response = await client.GetAsync("/backlog" + query);
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ICollection<Backlog> backlogs = JsonSerializer.Deserialize<ICollection<Backlog>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return backlogs;
    }

    public async Task UpdateAsync(BacklogUpdateDto dto)
    {
        string dtoAsJson = JsonSerializer.Serialize(dto);
        StringContent body = new StringContent(dtoAsJson, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PatchAsync("/backlog", body);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task DeleteAsync(string name)
    {
        HttpResponseMessage response = await client.DeleteAsync($"backlog/{name}");
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }
    
    private static string ConstructQuery(User? productOwner, bool? isCompleted, string? titleContains)
    {
        string query = "";
        if (!string.IsNullOrEmpty(productOwner.UserName))
        {
            query += $"?username={productOwner.UserName}";
        }
        
        if (isCompleted != null)
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"completedstatus={isCompleted}";
        }

        if (!string.IsNullOrEmpty(titleContains))
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"titlecontains={titleContains}";
        }

        return query;
    }
}