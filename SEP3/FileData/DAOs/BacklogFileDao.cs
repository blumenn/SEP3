using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace FileData.DAOs;

public class BacklogFileDao: IBacklogDao
{
    private readonly FileContext context;

    public BacklogFileDao(FileContext context)
    {
        this.context = context;
    }

    public Task<Backlog> CreateAsync(Backlog backlog)
    {
        context.Backlogs.Add(backlog);
        context.SaveChanges();
        return Task.FromResult(backlog);
    }

    public Task<IEnumerable<Backlog>> GetAsync(SearchBacklogParametersDto searchParameters)
    {
        IEnumerable<Backlog> result = context.Backlogs.AsEnumerable();

        if (!string.IsNullOrEmpty(searchParameters.Name))
        {
            // we know username is unique, so just fetch the first
            result = context.Backlogs.Where(backlog =>
                backlog.ProductOwner?.UserName != null && backlog.ProductOwner.UserName.Equals(searchParameters.Name, StringComparison.OrdinalIgnoreCase));
        }

        if (searchParameters.Name != null)
        {
            result = result.Where(t => t.ProductOwner?.UserName == searchParameters.Name);
        }

        if (searchParameters.CompletedStatus != null)
        {
            result = result.Where(t => t.IsCompleted == searchParameters.CompletedStatus);
        }

        if (!string.IsNullOrEmpty(searchParameters.TitleContains))
        {
            result = result.Where(b =>
                b.name.Contains(searchParameters.TitleContains, StringComparison.OrdinalIgnoreCase));
        }

        return Task.FromResult(result);
    }

    public Task UpdateAsync(Backlog backlogToUpdate)
    {
        Backlog? existing = context.Backlogs.FirstOrDefault(backlog => backlog.name == backlogToUpdate.name);
        if (existing == null)
        {
            throw new Exception($"Backlog with name {backlogToUpdate.name} does not exist!");
        }

        context.Backlogs.Remove(existing);
        context.Backlogs.Add(backlogToUpdate);
        
        context.SaveChanges();
        
        return Task.CompletedTask;
    }
    

    public Task<Backlog?> GetByNameAsync(string? backlogName)
    {
        Backlog? existing = context.Backlogs.FirstOrDefault(b => b.name == backlogName);
        return Task.FromResult(existing);
    }

    public Task DeleteAsync(string? backlogName)
    {
        Backlog? existing = context.Backlogs.FirstOrDefault(backlog => backlog.name == backlogName);
        if (existing == null)
        {
            throw new Exception($"Backlog  with name {backlogName} does not exist!");
        }

        context.Backlogs.Remove(existing); 
        context.SaveChanges();

        return Task.CompletedTask;
    }
    
    
}