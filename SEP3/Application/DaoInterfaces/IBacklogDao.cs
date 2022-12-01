using Domain.DTOs;
using Domain.Models;

namespace Application.DaoInterfaces;

public interface IBacklogDao
{
    Task<Backlog> CreateAsync(Backlog backlog);
     Task<IEnumerable<Backlog>> GetAsync(SearchBacklogParametersDto searchParameters);
    Task UpdateAsync(Backlog backlog);
    Task<Backlog?> GetByNameAsync(string backlogName);
    Task DeleteAsync(string backlogName);
    
}