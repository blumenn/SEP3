using Shared.DTOs;
using Shared.Models;

namespace Application.DaoInterfaces;

public interface IPostDAO
{
    Task<Post> CreateAsync(Post post);
    Task<IEnumerable<Post>> GetAsync(SearchPostParameterDTO searchParameters);
    Task UpdateAsync(Post post);
    Task<Post> GetByIdAsync(int postId);
    Task DeleteAsync(int id);
    
}