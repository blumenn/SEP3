using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace FileData.DAOs;

public class UserFileDao : IUserDao
{
    private readonly FileContext context;

    public UserFileDao(FileContext context)
    {
        this.context = context;
    }

    public Task<User> CreateAsync(User user)
    {
        int userId = 1;
        if (context.Users.Any())
        {
            userId = context.Users.Max(u => u.UserId);
            userId++;
        }

        user.UserId = userId;

        context.Users.Add(user);
        context.SaveChanges();

        return Task.FromResult(user);
    }

 

    public Task<User?> GetByUsernameAsync(string? userName)
    {
        User? existing = context.Users!.FirstOrDefault(u =>
                u.UserName != null && u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase)
            );
            return Task.FromResult(existing);
        
       
    }
    
    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters)
    {
        IEnumerable<User> users = context.Users.AsEnumerable();
            if (searchParameters.UsernameContains != null)
            {
                users = context.Users.Where(u => u.UserName != null && u.UserName.Contains(searchParameters.UsernameContains, StringComparison.OrdinalIgnoreCase));
            }
            return Task.FromResult(users);
    }

    public Task<User?> GetByIdAsync(int id)
    {
        if (context.Users != null)
        {
            User? existing = context.Users.FirstOrDefault(u =>
                u.UserId == id
            );
            return Task.FromResult(existing);
        }

        return null;
    }
}