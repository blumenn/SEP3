using Domain.DTOs;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IBackLogService
{
    Task CreateAsync(BacklogCreationDto dto);
    Task<ICollection<Backlog>> GetAsync(User? productOwner, bool? isCompleted, string? titleContains);
    Task UpdateAsync(BacklogUpdateDto dto);
    Task DeleteAsync(string name);
}