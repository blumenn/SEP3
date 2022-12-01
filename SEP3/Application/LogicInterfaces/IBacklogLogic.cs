using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IBacklogLogic
{
   Task<Backlog> CreateAsync(BacklogCreationDto dto);
   public Task<IEnumerable<Backlog>> GetAsync(SearchBacklogParametersDto searchParameters);
   Task UpdateAsync(BacklogUpdateDto dto);
   Task DeleteAsync(string? name);
   Task<BacklogBasicDto> GetByNameAsync(string? backlogName);
}