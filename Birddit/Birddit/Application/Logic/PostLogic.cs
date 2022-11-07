using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared.DTOs;
using Shared.Models;

namespace Application.Logic;

public class PostLogic : IPostLogic
{
    private readonly IPostDAO postDao;
    private readonly IUserDAO userDao;

    public PostLogic(IPostDAO postDao, IUserDAO userDao)
    {
        this.postDao = postDao;
        this.userDao = userDao;
    }

    public async Task<Post> CreateAsync(PostCreationDTO dto)
    {
        User? user = await userDao.GetByIdAsync(dto.OwnerId);
        if (user == null)
        {
            throw new Exception($"User with id {dto.OwnerId} was not found.");
        }

        ValidatePost(dto);
        Post post = new Post(user, dto.Title, dto.Body);
        Post created = await postDao.CreateAsync(post);
        return created;
    }

    public Task<IEnumerable<Post>> GetAsync(SearchPostParameterDTO searchParameters)
    {
        return postDao.GetAsync(searchParameters);
    }

    public async Task UpdateAsync(PostUpdateDTO post)
    {
        Post? existing = await postDao.GetByIdAsync(post.Id);

        if (existing == null)
        {
            throw new Exception($"Todo with ID {post.Id} not found!");
        }

        User? user = null;
        if (post.OwnerId != null)
        {
            user = await userDao.GetByIdAsync((int)post.OwnerId);
            if (user == null)
            {
                throw new Exception($"User with id {post.OwnerId} was not found.");
            }
        }

        if (post.IsCompleted != null && existing.IsCompleted && !(bool)post.IsCompleted)
        {
            throw new Exception("Cannot un-complete a completed Todo");
        }

        User userToUse = user ?? existing.Owner;
        string titleToUse = post.Title ?? existing.Title;
        string bodyToUse = post.Body ?? existing.Body;
        bool completedToUse = post.IsCompleted ?? existing.IsCompleted;
    
        Post updated = new (userToUse, titleToUse, bodyToUse)
        {
            IsCompleted = completedToUse,
            Id = existing.Id,
        };

        ValidatePost(updated);

        await postDao.UpdateAsync(updated);
    }

    private void ValidatePost(Post dto)
    {
        if (string.IsNullOrEmpty(dto.Title)) throw new Exception("Title cannot be empty.");
        // other validation stuff
    }

    private void ValidatePost(PostCreationDTO dto)
    {
        if (string.IsNullOrEmpty(dto.Title)) throw new Exception("Title cannot be empty.");
        // other validation stuff
    }
    
    public async Task DeleteAsync(int id)
    {
        Post? todo = await postDao.GetByIdAsync(id);
        if (todo == null)
        {
            throw new Exception($"Todo with ID {id} was not found!");
        }

        if (!todo.IsCompleted)
        {
            throw new Exception("Cannot delete un-completed Todo!");
        }

        await postDao.DeleteAsync(id);
    }
    
    
    
}