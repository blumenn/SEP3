using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class BacklogLogic: IBacklogLogic
{
    private readonly IBacklogDao backlogDao;
    private readonly IUserDao userDao;

    public BacklogLogic(IBacklogDao backlogDao, IUserDao userDao)
    {
        this.backlogDao = backlogDao;
        this.userDao = userDao;
    }
    
    public async Task<Backlog> CreateAsync(BacklogCreationDto dto)
    {
       
            User? user = await userDao.GetByUsernameAsync(dto.user.UserName);
            if (user?.UserName == null)
            {
                throw new Exception($"User with name {dto.user.UserName} was not found.");
            }

            Backlog backlog = new Backlog(dto.backlogName, dto.user);

            ValidateBacklog(backlog);

            Backlog? createdBacklog = await backlogDao.CreateAsync(backlog);
            return createdBacklog;

    }

    public Task<IEnumerable<Backlog>> GetAsync(SearchBacklogParametersDto searchParameters)
    {
        return backlogDao.GetAsync(searchParameters);
    }

    public async Task UpdateAsync(BacklogUpdateDto dto)
    {
        Backlog? existing = await backlogDao.GetByNameAsync(dto.Name);

        if (existing != null)
        {
            throw new Exception($"Todo with ID {dto.Id} not found!");
        }

        User? user = null;
        if (dto.Id != null)
        {
            user = await userDao.GetByIdAsync((int)dto.Id);
            if (user == null)
            {
                throw new Exception($"User with id {dto.Id} was not found.");
            }
        }

        if (dto.IsCompleted != null && existing.IsCompleted && !(bool)dto.IsCompleted)
        {
            throw new Exception("Cannot un-complete a completed Todo");
        }

        var userToUse = user ?? existing.ProductOwner;
        var titleToUse = dto.Name ?? existing.name;
        var completedToUse = dto.IsCompleted ?? existing.IsCompleted;
        
        Backlog updated = new (titleToUse, userToUse )
        {
            IsCompleted = completedToUse,
            name = existing.name,
        };

        ValidateBacklog(updated);

        await backlogDao.UpdateAsync(updated);
    }

    public async Task DeleteAsync(string? name)
    {
        Backlog? todo = await backlogDao.GetByNameAsync(name);
        if (todo == null)
        {
            throw new Exception($"Backlog with name {name} was not found!");
        }

        if (!todo.IsCompleted)
        {
            throw new Exception("Cannot delete un-completed Todo!");
        }

        await backlogDao.DeleteAsync(name);
    }
    
    public async Task<BacklogBasicDto> GetByNameAsync(string? backlogName)
    {
        var backlog = await backlogDao.GetByNameAsync(backlogName);
        if (backlog == null)
        {
            throw new Exception($"Todo with name {backlogName} not found");
        }

        return new BacklogBasicDto(backlog.name, backlog.ProductOwner, backlog.IsCompleted);
    }

    

    private void ValidateBacklog(Backlog backlog)
    {
        if (string.IsNullOrEmpty(backlog.name)) throw new Exception("Name cannot be empty.");
    }
}