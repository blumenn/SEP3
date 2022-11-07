using Shared.DTOs;
using Shared.Models;

namespace HTTPClients.ClientInterfaces;

public interface IPostService
{
    Task CreateAsync(PostCreationDTO dto);
    Task<IEnumerable<Post>> GetAsync(int? authorId, string? authorName, string? titleContains, string? bodyContains);
    Task<Post> GetByIdAsync(int id);
}